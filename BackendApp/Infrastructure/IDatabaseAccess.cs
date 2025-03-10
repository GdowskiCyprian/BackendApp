﻿using BackendApp.Controllers.ViewModels;
using BackendApp.Models;

namespace BackendApp.Infrastructure
{
    public interface IDatabaseAccess
    {
        Task<List<BusinessModel>> GetBusinessModelsAsync(int offset);
        Task<int> InsertBusinessModelAsync(BusinessViewModel model);
        Task<int> UpdateBusinessModelAsync(BusinessModel model);
        Task<int> DeleteBusinessModelAsync(int id);

        Task<List<VisitModel>> GetVisitsAsync(int offset);
        Task<int> InsertVisitAsync(VisitViewModel model);
        Task<int> UpdateVisitAsync(VisitModel model);
        Task<int> DeleteVisitAsync(int id);

        public Task<List<VisitModel>> GetVisitsByDateRangeAsync(DateTime dateFrom, DateTime dateTo, int offset);

    }
}