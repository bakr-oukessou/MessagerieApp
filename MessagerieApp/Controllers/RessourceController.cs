using Microsoft.AspNetCore.Mvc;
using MessagerieApp.Models;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces;

namespace MessagerieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RessourceController : ControllerBase
    {
        private readonly IRessourceService _ressourceService;

        public RessourceController(IRessourceService ressourceService)
        {
            _ressourceService = ressourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRessources()
        {
            var ressources = await _ressourceService.GetAllRessourcesAsync();
            return Ok(ressources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRessourceById(int id)
        {
            var ressource = await _ressourceService.GetRessourceByIdAsync(id);
            if (ressource == null)
            {
                return NotFound();
            }
            return Ok(ressource);
        }

        [HttpPost]
        public async Task<IActionResult> AddRessource(Ressource ressource)
        {
            await _ressourceService.AddRessourceAsync(ressource);
            return CreatedAtAction(nameof(GetRessourceById), new { id = ressource.Id }, ressource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRessource(int id, Ressource ressource)
        {
            if (id != ressource.Id)
            {
                return BadRequest();
            }

            await _ressourceService.UpdateRessourceAsync(ressource);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRessource(int id)
        {
            await _ressourceService.DeleteRessourceAsync(id);
            return NoContent();
        }

        [HttpPost("{ressourceId}/assign-to-department/{departmentId}")]
        public async Task<IActionResult> AssignRessourceToDepartment(int ressourceId, int departmentId)
        {
            await _ressourceService.AssignRessourceToDepartmentAsync(ressourceId, departmentId);
            return NoContent();
        }

        [HttpPost("{ressourceId}/assign-to-user/{userId}")]
        public async Task<IActionResult> AssignRessourceToUser(int ressourceId, int userId)
        {
            await _ressourceService.AssignRessourceToUserAsync(ressourceId, userId);
            return NoContent();
        }

        [HttpPost("{ressourceId}/update-status/{status}")]
        public async Task<IActionResult> UpdateRessourceStatus(int ressourceId, string status)
        {
            await _ressourceService.UpdateRessourceStatusAsync(ressourceId, status);
            return NoContent();
        }
    }
}