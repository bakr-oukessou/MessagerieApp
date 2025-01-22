namespace MessagerieApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }  // Admin, DepartmentHead, Teacher, Supplier, MaintenanceStaff
        public string? DepartementId { get; set; }
        public virtual Departement? Department { get; set; }
    }

}
