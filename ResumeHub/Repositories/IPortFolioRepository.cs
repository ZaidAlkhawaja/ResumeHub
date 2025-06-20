using ResumeHub.Models;
using System.Threading.Tasks;

namespace ResumeHub.Repositories
{
    public interface IPortFolioRepository
    {
        public void AddPortFolio(PortFolio portFolio);

        public Task UpdatePortFolio(PortFolio portFolio);

        public Task DeletePortFolio(int id);

        public Task<PortFolio> GetPortFolioById(int id);

        public List<PortFolio> GetPortFolioByEndUserId(string endUserId);

        Task<List<PortFolio>> GetPortfoliosCount(int count);

        Task<int> GetCountAsync(); 



    }
}
