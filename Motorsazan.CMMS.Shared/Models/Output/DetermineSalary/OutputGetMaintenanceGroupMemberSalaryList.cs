namespace Motorsazan.CMMS.Shared.Models.Output.DetermineSalary
{
    public class OutputGetMaintenanceGroupMemberSalaryList
    {
        public long SalaryId { get; set; }

        public int EId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string ShamsiStartDate { get; set; }

        public string ShamsiCreationDate { get; set; }

        public string RegisterUserName { get; set; }
    }
}
