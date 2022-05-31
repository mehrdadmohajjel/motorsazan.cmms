namespace Motorsazan.CMMS.Shared.Models.Output.Inspection
{
    public class OutputGetInspectionListByCondition
    {
        public long InspectionId { get; set; }

        public string InspectionTypeShowName { get; set; }

        public string DepartmentTitle { get; set; }

        public string MaintenanceGroupName { get; set; }

        public long WorkOrderSerial { get; set; }

        public string ShamsiCreationDateTime { get; set; }

        public long PersonHour { get; set; }

        public string Description { get; set; }

        public string WorkOrderType { get; set; }

        public long WorkOrderId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }
    }
}
