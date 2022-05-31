using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.OCEReport
{
    public class InputGetOCEReportByCondition
    {
        public long MaintenanceGroupId { get; set; }

        public long MaintenanceGroupMemberId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }
    }
}