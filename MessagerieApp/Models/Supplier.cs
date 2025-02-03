using MessagerieApp.Models;
using System.ComponentModel.DataAnnotations;

public class Supplier
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Le nom de la société est requis.")]
	[StringLength(200)]
	public string CompanyName { get; set; }

	public bool IsBlacklisted { get; set; } = false;

	[StringLength(1000)]
	public string? BlacklistReason { get; set; }

	public virtual ICollection<AppelOffres> Offres { get; set; }
}