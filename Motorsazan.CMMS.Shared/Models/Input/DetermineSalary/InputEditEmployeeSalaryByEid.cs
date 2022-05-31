using System;

namespace Motorsazan.CMMS.Shared.Models.Input.DetermineSalary
{
    public class InputEditEmployeeSalaryByEid
    {
        public long Eid { get; set; }

        public string Salary { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
