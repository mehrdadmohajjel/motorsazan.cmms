using System;

namespace Motorsazan.CMMS.Shared.Models.Output.DetermineSalary
{
    public class OutputGetMaintenanceGroupMemberSalaryListByCondition
    {
        public long SalaryId { get; set; }

        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string ShamsiStartDate { get; set; }
    }
}