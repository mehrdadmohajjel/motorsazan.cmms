using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;

namespace Motorsazan.CMMS.Shared.Models.Input.DetermineSalary
{
    public class InputEditMaintenanceGroupMemberSalaryBySalaryId
    {
        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public Tbl_SalaryDetailList[] SalaryDetailList { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        public int RegisterUserId { get; set; }
    }
}