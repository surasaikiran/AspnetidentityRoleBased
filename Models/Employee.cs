using System.ComponentModel.DataAnnotations;

namespace AspnetidentityRoleBased.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Department")]
        public Nullable<int> DepartmentId { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
        public virtual Department Department { get; set; }
        public virtual Sites Sites { get; set; }
        public bool IsDeleted { get; internal set; }
        public string City { get; internal set; }
    }
}
