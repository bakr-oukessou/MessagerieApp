using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class DepartementModel : PageModel
    {
        public List<Departement> DepartementsList { get; set; } = new List<Departement>();

        public void OnGet()
        {
            // Fetch departments from the database or service
            // Example:
            // DepartementsList = _departementService.GetAllDepartements();
        }

        public void OnPost(string departmentName)
        {
            // Handle creation of a new department
            // Example:
            // var newDepartement = new Departement { DepartmentName = departmentName };
            // _departementService.AddDepartement(newDepartement);
        }
    }
}
