using MessagerieApp.Models.MasterData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
	public class ComputerModel : PageModel
    {
        public List<Computer> ComputersList { get; set; } = new List<Computer>();

        public void OnGet()
        {
            // Fetch computers from the database or service
            // Example:
            // ComputersList = _computerService.GetAllComputers();
        }

        public void OnPost(string brand, string cpu, string ram, string hardDisk, string screen)
        {
            // Handle creation of a new computer
            // Example:
            // var newComputer = new Computer { Brand = brand, CPU = cpu, RAM = ram, HardDisk = hardDisk, Screen = screen };
            // _computerService.AddComputer(newComputer);
        }
    }
}
