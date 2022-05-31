namespace Motorsazan.CMMS.Shared.Models.Output.CostReportByDepartment
{
    public class OutputGetDepartmentCostReportByCondition
    {
        public long ActionOrDelayListId { get; set; }

        public long WorkOrderSerial { get; set; }

        public string DepartmentName { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string FaultCode { get; set; }

        public string FaultReason { get; set; }

        public string WorkOrderCreationDate { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string ExpertName { get; set; }

        public int EId { get; set; }

        public decimal Salary { get; set; }

        public decimal MaterialCost { get; set; }

        public decimal TotalCost { get; set; }
    }
}
