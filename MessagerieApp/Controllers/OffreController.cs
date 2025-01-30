using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessagerieApp.Models;
using MessagerieApp.Services;
using MessagerieApp.Business;

namespace MessagerieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffreController : ControllerBase
    {
        private readonly OffreService _offreService;

        public OffreController(OffreService offreService)
        {
            _offreService = offreService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffre(int id)
        {
            var offre = await _offreService.GetOffreByIdAsync(id);
            return offre != null ? Ok(offre) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffres()
        {
            var offres = await _offreService.GetAllOffresAsync();
            return Ok(offres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffre([FromBody] Offre offre)
        {
            int id = await _offreService.CreateOffreAsync(offre);
            return CreatedAtAction(nameof(GetOffre), new { id }, offre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffre(int id, [FromBody] Offre offre)
        {
            if (id != offre.Id) return BadRequest();
            return await _offreService.UpdateOffreAsync(offre) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffre(int id)
        {
            return await _offreService.DeleteOffreAsync(id) ? NoContent() : NotFound();
        }
    }
}
