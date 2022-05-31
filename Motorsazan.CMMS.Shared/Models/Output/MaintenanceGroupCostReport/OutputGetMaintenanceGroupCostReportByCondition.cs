using System;

namespace Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroupCostReport
{
   public class OutputGetMaintenanceGroupCostReportByCondition
    {
        public long ActionOrDelayListId { get; set; }

        public long WorkOrderSerial { get; set; }

        public string DepartmentName { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string FaultCode { get; set; }

        public string FaultReason { get; set; }

        public int DurationInMinute { get; set; }

        public string MaintenanceGroupName { get; set; }

        public decimal Salary { get; set; }

        public string ExpertName { get; set; }

        public int EId { get; set; }
    }
}