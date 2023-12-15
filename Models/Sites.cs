namespace AspnetidentityRoleBased.Models
{
    public class Sites
    {
        public int SitesId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string SiteName { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
