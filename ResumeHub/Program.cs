using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResumeHub.Data;
using ResumeHub.Models;
using ResumeHub.Repositories;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ResumeHub.Interfaces;
using ResumeHub.Services;
using RizeUp.Services;
using Microsoft.Extensions.Options;
using ResumeHub.Services.PdfGeneration;




namespace ResumeHub;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IResumeRepository, ResumeRepository>(); 
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddScoped < IResumeRepository, ResumeRepository>();
        builder.Services.AddScoped<IPortFolioRepository, PortFolioRepository>();

        builder.Services.AddScoped<IResumePdfGenerator, ResumePdfGenerator>();

        builder.Services.AddScoped<IAiLetterService, AiLetterService>();

        builder.Services.AddScoped<IReviewRepo, ReviewRepo>();


        var key = builder.Configuration["OpenAI:ApiKey"];
        builder.Services.AddSingleton<Kernel>(sp =>
        {
            var kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddOpenAIChatCompletion("gpt-4", key);
            return kernelBuilder.Build();
         
        });

        builder.Services.AddSingleton<IResumeOpenAi, ResumeOpenAi>();
        
        builder.Services.AddSingleton<IPortfolioOpenAiService, PortfolioOpenAiService>();


        //this is for the email service
        builder.Services.Configure<EmailSettings>(
              builder.Configuration.GetSection("EmailSettings"));

        builder.Services.AddSingleton(sp =>
        sp.GetRequiredService<IOptions<EmailSettings>>().Value);
        builder.Services.AddControllersWithViews();

        

        builder.Services.AddDefaultIdentity<Person>(options => options.SignIn.RequireConfirmedAccount = true).
            AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            if (context.User?.Identity?.IsAuthenticated == true && context.Request.Path == "/")
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<Person>>();
                var user = await userManager.GetUserAsync(context.User);
                if (user != null && await userManager.IsInRoleAsync(user, "Admin"))
                {
                    context.Response.Redirect("/Admin/Index");
                    return;
                }

                context.Response.Redirect("/Home/Index");
                return;
            }

            await next();
        });
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
