using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.DetermineSalary
{
    public class InputGetMaintenanceGroupMemberSalaryListByCondition
    {
        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime SpecialDate { get; set; }
    }
}