using ResumeHub.Data;
using ResumeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace ResumeHub.Repositories
{
    public class PortFolioRepository : IPortFolioRepository
    {
        private readonly ApplicationDbContext _Context;
        public PortFolioRepository(ApplicationDbContext context)
        {
            _Context = context; // Initialize the repository with the database context.
        }
        public void AddPortFolio(PortFolio portFolio)
        {
            _Context.Portfolios.Add(portFolio); // Add the portfolio to the Portfolios DbSet in the context.
            _Context.SaveChanges(); // Save changes to the database.

        }

        public async Task DeletePortFolio(int id)
        {
            var portFolio = _Context.Portfolios.Find(id); // Find the portfolio by ID in the Portfolios DbSet.
            if (portFolio != null)
            {
                portFolio.IsDeleted = true; 
                _Context.Portfolios.Update(portFolio); 
               await _Context.SaveChangesAsync(); 
            }
           
        }

        public List<PortFolio> GetPortFolioByEndUserId(string endUserId)
        {
            return _Context.Portfolios.Where(x => x.EndUserId == endUserId).ToList(); // Retrieve portfolios by EndUserId.
        }

        public async Task <PortFolio> GetPortFolioById(int id)
        {
            return _Context.Portfolios
                  .Include(p => p.Services)
                  .Include(p => p.Projects)
                  .Include(p => p.Skills)
                  .FirstOrDefault(p => p.PortFolioId == id);// Find and return the portfolio by ID from the Portfolios DbSet.
        }

        public async Task UpdatePortFolio(PortFolio portFolio)
        {
            try

            {

                var existingPortfolio = await _Context.Portfolios

                    .Include(p => p.Services)

                    .Include(p => p.Projects)

                    .Include(p => p.Skills)

                    .FirstOrDefaultAsync(p => p.PortFolioId == portFolio.PortFolioId);

                if (existingPortfolio == null)

                {

                    throw new KeyNotFoundException($"Portfolio with ID  not found.");

                }

                // Remove related entities

                _Context.Services.RemoveRange(existingPortfolio.Services);

                _Context.Projects.RemoveRange(existingPortfolio.Projects);

                _Context.Skills.RemoveRange(existingPortfolio.Skills);

                // Remove the portfolio itself

                _Context.Portfolios.Remove(existingPortfolio);

                // Add the new portfolio

                _Context.Portfolios.Add(portFolio);

                await _Context.SaveChangesAsync();

            }

            catch (Exception ex)

            {

                throw new Exception("An error occurred while updating the portfolio.", ex);

            }


        }
    }
}
