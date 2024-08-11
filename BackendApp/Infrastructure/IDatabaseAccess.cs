using BackendApp.Controllers.ViewModels;
using BackendApp.Models;

namespace BackendApp.Infrastructure
{
    public interface IDatabaseAccess
    {
        Task<List<BusinessModel>> GetBusinessModelsAsync();
        Task<int> InsertBusinessModelAsync(BusinessViewModel model);
        Task<int> UpdateBusinessModelAsync(BusinessModel model);
        Task<int> DeleteBusinessModelAsync(int id);
    }
}
