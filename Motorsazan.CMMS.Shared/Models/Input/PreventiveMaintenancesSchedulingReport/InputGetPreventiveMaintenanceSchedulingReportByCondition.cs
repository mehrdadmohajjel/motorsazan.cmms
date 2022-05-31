using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesSchedulingReport
{
    public class InputGetPreventiveMaintenanceSchedulingReportByCondition
    {
        public int ParentDepartmentId { get; set; }
        public int ChildDepartmentId { get; set; }

        public long MaintenanceGroupId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }
    }
}
