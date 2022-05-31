using System;

namespace Motorsazan.CMMS.Shared.Models.Input.AllWorkOrderStatusReport
{
    public class InputAllWorkOrderStatusReportListByCondition
    {
        public string MaintenanceGroupType { get; set; }

        public string WorkOrderType { get; set; }

        public string StopType { get; set; }

        public string MaintenanceGroup { get; set; }

        public string WorkOrderStatusType { get; set; }

        public string DepartmentList { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}