using System.ComponentModel.DataAnnotations;

namespace AspnetidentityRoleBased.Models
{
    public class EmployeeViewModelcs
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Department")]
        public Nullable<int> DepartmentId { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        public string City { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public string DepartmentName { get; set; }
        public bool Remember { get; set; }
        public string SiteName { get; set; }
    }
}
