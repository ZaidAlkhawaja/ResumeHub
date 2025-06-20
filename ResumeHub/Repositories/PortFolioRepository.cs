using ResumeHub.Data;
using ResumeHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

namespace ResumeHub.Repositories
{
    public class PortFolioRepository : IPortFolioRepository
    {
        private readonly ApplicationDbContext _Context;
        public PortFolioRepository(ApplicationDbContext context)
        {
            _Context = context; 
        }
        public void AddPortFolio(PortFolio portFolio)
        {
            _Context.Portfolios.Add(portFolio); 
            _Context.SaveChanges(); 

        }

        public async Task DeletePortFolio(int id)
        {
            var portFolio = _Context.Portfolios.Find(id); 
            {
                portFolio.IsDeleted = true; 
                _Context.Portfolios.Update(portFolio); 
               await _Context.SaveChangesAsync(); 
            }
           
        }

        public List<PortFolio> GetPortFolioByEndUserId(string endUserId)
        {
            return _Context.Portfolios.Where(x => x.EndUserId == endUserId).ToList(); 
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

        public async Task<List<PortFolio>> GetPortfoliosCount(int count)
        {
            // Fetch the latest 'count' portfolios that are not deleted, including related data for DataTable display
            return await _Context.Portfolios
                .OrderByDescending(p => p.PortFolioId)
                .Include(p => p.EndUser)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }




        public async Task<int> GetCountAsync()
        {
            return await _Context.Portfolios.CountAsync();
        }

    }


}
