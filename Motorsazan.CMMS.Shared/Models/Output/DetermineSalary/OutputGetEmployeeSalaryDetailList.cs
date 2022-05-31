using System;

namespace Motorsazan.CMMS.Shared.Models.Output.DetermineSalary
{
    public class OutputGetEmployeeSalaryDetailList
    {
        public int ID { get; set; }

        public string Salary { get; set; }

        public string EffectiveDate { get; set; }

        public string RecordDate { get; set; }

        public string RegistrantUser { get; set; }
    }
}
