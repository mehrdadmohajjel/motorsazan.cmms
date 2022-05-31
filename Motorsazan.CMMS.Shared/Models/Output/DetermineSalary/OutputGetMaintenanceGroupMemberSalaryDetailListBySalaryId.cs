namespace Motorsazan.CMMS.Shared.Models.Output.DetermineSalary
{
    public class OutputGetMaintenanceGroupMemberSalaryDetailListBySalaryId
    {
        public long SalaryId { get; set; }

        public string RegisterUserName { get; set; }

        public decimal Salary { get; set; }

        public string ShamsiStartDate { get; set; }

        public string ShamsiCreationDate { get; set; }
    }
}