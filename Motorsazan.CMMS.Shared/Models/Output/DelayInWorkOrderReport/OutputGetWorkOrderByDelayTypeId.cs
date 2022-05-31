namespace Motorsazan.CMMS.Shared.Models.Output.DelayInWorkOrderReport
{
    public class OutputGetWorkOrderByDelayTypeId
    {
        public long WorkOrderId { get; set; }

        public string StopTypeTitle { get; set; }

        public string WorkOrderSerial { get; set; }

        public string PersianWorkOrderCreationDateTime { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string EmployeeName { get; set; }

        public int DurationInMinute { get; set; }

        public string DelayDescription { get; set; }

        public string PreRequestNO { get; set; }

        public string RequestNO { get; set; }

        public string WorkOrderStatus { get; set; }

    }
}
