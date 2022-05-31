using System;

namespace Motorsazan.CMMS.Shared.Models.Output.DetermineSalary
{
    public class OutputGetAllEmployeeSalaryListByCondition
    {
        public int ID { get; set; }

        public long EID { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Salary { get; set; }

        public string EffectiveDate { get; set; }
    }
}
