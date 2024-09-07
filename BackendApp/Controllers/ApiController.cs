using BackendApp.Controllers.ViewModels;
using BackendApp.Infrastructure;
using BackendApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Controllers
{
    [Controller]
    [Authorize]
    public class ApiController : Controller
    {
        private readonly IDatabaseAccess _databaseAccess;

        public ApiController(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpGet("GetBusinessModels")]
        public async Task<ActionResult<List<BusinessModel>>> GetBusinessModels(int offset)
        {
            var businessModels = await _databaseAccess.GetBusinessModelsAsync(offset);
            return Ok(businessModels);
        }

        [HttpPost("InsertBusinessModel")]
        public async Task<ActionResult> InsertBusinessModel([FromBody] BusinessViewModel model)
        {
            var result = await _databaseAccess.InsertBusinessModelAsync(model);
            return result > 0 ? Ok() : StatusCode(500);
        }

        [HttpPut("UpdateBusinessModel")]
        public async Task<ActionResult> UpdateBusinessModel([FromBody] BusinessModel model)
        {
            var result = await _databaseAccess.UpdateBusinessModelAsync(model);
            return result > 0 ? Ok() : StatusCode(500);
        }

        [HttpDelete("DeleteBusinessModel/{id}")]
        public async Task<ActionResult> DeleteBusinessModel(int id)
        {
            var result = await _databaseAccess.DeleteBusinessModelAsync(id);
            return result > 0 ? Ok() : StatusCode(500);
        }

        [HttpGet("GetVisits")]
        public async Task<ActionResult<List<VisitModel>>> GetVisits(int offset)
        {
            var visits = await _databaseAccess.GetVisitsAsync(offset);
            return Ok(visits);
        }

        [HttpPost("InsertVisit")]
        public async Task<ActionResult> InsertVisit([FromBody]VisitViewModel model)
        {
            var result = await _databaseAccess.InsertVisitAsync(model);
            return result > 0 ? Ok() : StatusCode(500);
        }

        [HttpPut("UpdateVisit")]
        public async Task<ActionResult> UpdateVisit([FromBody] VisitModel model)
        {
            var result = await _databaseAccess.UpdateVisitAsync(model);
            return result > 0 ? Ok() : StatusCode(500);
        }

        [HttpDelete("DeleteVisit/{id}")]
        public async Task<ActionResult> DeleteVisit(int id)
        {
            var result = await _databaseAccess.DeleteVisitAsync(id);
            return result > 0 ? Ok() : StatusCode(500);
        }

        [HttpGet("GetVisitsByDateRange")]
        public async Task<ActionResult<List<VisitModel>>> GetVisitsByDateRange(DateTime dateFrom, DateTime dateTo, int offset)
        {
            var visits = await _databaseAccess.GetVisitsByDateRangeAsync(dateFrom, dateTo, offset);
            return Ok(visits);
        }
    }
}
