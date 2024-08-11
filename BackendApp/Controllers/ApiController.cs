using BackendApp.Controllers.ViewModels;
using BackendApp.Infrastructure;
using BackendApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Controllers
{
    [Controller]
    public class ApiController : Controller
    {
        private readonly IDatabaseAccess _databaseAccess;

        public ApiController(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpGet("GetBusinessModels")]
        public async Task<ActionResult<List<BusinessModel>>> GetBusinessModels()
        {
            var businessModels = await _databaseAccess.GetBusinessModelsAsync();
            return Ok(businessModels);
        }

        [HttpPost("InsertBusinessModel")]
        public async Task<ActionResult> InsertBusinessModel(BusinessViewModel model)
        {
            var result = await _databaseAccess.InsertBusinessModelAsync(model);
            return result > 0 ? Ok() : StatusCode(500);
        }

        [HttpPut("UpdateBusinessModel")]
        public async Task<ActionResult> UpdateBusinessModel(BusinessModel model)
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
    }
}
