namespace Motorsazan.CMMS.Shared.Models.Output.MachineStopStatusReport
{
    public class OutputGetWorkOrderListByMachineId
    {
        public long WorkOrderId { get; set; }

        public string WorkOrderStatusTypeName { get; set; }

        public string StopTypeTitle { get; set; }

        public string PersianCreationDate { get; set; }

        public string WorkOrderSerial { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string WorkOrderTypeTitle { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string Description { get; set; }

    }
}
