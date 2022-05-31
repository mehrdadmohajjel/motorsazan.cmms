using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesScheduling
{
    public class InputGetPreventiveMaintenanceSchedulingListByCondition
    {
        public int DepartmentId { get; set; }

        public long? MachineId { get; set; }

        public long MaintenanceGroupId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }
    }
}
