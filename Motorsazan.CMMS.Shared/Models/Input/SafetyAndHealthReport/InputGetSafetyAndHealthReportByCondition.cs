using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.SafetyAndHealthReport
{
    public class InputGetSafetyAndHealthReportByCondition
    {
        public long WorkOrderTypeId { get; set; }

        public long WorkOrderStatusTypeId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }
    }
}
