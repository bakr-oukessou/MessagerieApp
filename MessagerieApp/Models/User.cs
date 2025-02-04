﻿// Models/User.cs
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
    public UserRole Role { get; set; }

    public int? DepartmentId { get; set; }
    public virtual Departement? Department { get; set; }

    public int? SupplierId { get; set; }
    public virtual Fournisseur? Supplier { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(256)]
    public string Password { get; set; }  // Stockage du mot de passe en clair
}
}