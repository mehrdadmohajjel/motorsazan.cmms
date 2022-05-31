using System;

namespace Motorsazan.CMMS.Shared.Models.Output.CostReportByWorkOrder
{
    public class OutputGetCostReportByWorkOrderByCondition
    {
        public int ID { get; set; }

        public string WorkOrderSerial { get; set; }

        public string EmployeeName { get; set; }

        public string Department { get; set; }

        public string SubDepartment { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string FailureCode { get; set; }

        public string FailureRoot { get; set; }

        public string MaintenanceGroupName { get; set; }

        public DateTime WorkOrderDate { get; set; }

        public DateTime FinishDate { get; set; }

        public int ActionTime { get; set; }

        public int Salary { get; set; }
    }
}
