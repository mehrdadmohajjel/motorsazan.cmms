using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupMttrAndMtbfReport
{
    public class InputGetMaintenanceGroupMttrAndMtbfReportByCondition
    {
        public int DepartmentId { get; set; }

        public int MaintenanceGroupId { get; set; }

        public int TimeType { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }
    }
}
