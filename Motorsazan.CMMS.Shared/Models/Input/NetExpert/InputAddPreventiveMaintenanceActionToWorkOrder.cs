using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputAddPreventiveMaintenanceActionToWorkOrder
    {
        public long WorkOrderId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public PMSchedulingInfoIdList[] PMSchedulingInfoIDList { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public EmployeeIdList[] ResposibleEmployeeIDList { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Time)]
        public string StartTime { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Time)]
        public string EndTime { get; set; }

        public int DurationInMinute { get; set; }

        public int UserId { get; set; }
    }
}