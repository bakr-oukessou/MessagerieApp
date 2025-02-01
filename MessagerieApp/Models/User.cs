namespace MessagerieApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }  // Admin, DepartmentHead, Teacher, Supplier, MaintenanceStaff
        public int? DepartmentId { get; set; }
        public virtual Departement? Department { get; set; }
    }

}
