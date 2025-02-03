// Models/User.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessagerieApp.Models
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		[StringLength(256)]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(256)]
		public string Email { get; set; }

		[Required]
		public byte[] PasswordHash { get; set; }

		[Required]
		public byte[] PasswordSalt { get; set; }

		[Required]
		public UserRole Role { get; set; }

		public int? DepartmentId { get; set; }
		public virtual Departement? Department { get; set; }

		public int? SupplierId { get; set; }
		public virtual Supplier? Supplier { get; set; }

		[NotMapped] // Champ non stocké en base
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}