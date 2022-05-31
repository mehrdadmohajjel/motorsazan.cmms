using System;

namespace Motorsazan.CMMS.Shared.Models.Output.DetermineSalary
{
    public class OutputGetMaintenanceGroupMemberSalaryDetailsListBySalaryId
    {
        public long SalaryId { get; set; }

        public int RegisterUserId { get; set; }

        public decimal Salary { get; set; }

        public string ShamsiStartDate { get; set; }

        public string ShamsiCreationDate { get; set; }
    }
}