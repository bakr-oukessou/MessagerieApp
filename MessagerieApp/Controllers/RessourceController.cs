using MessagerieApp.Models;
using MessagerieApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Controllers
{
    public class RessourceController : Controller
    {
        private readonly IGenericRepository<Ressource> _ressourceRepository;

        public RessourceController(IGenericRepository<Ressource> ressourceRepository)
        {
            _ressourceRepository = ressourceRepository;
        }

        public async Task<IActionResult> Index()    
        {
            var resources = await _ressourceRepository.GetAllAsync();
            return View(resources);
        }
    }
}
