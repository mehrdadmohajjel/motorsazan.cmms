namespace Motorsazan.CMMS.Shared.Models.Output.SchedulerWorkOrderPrintReport
{
    public class OutputGetVisitFormPrintByWorkOrderId
    {
        public long WorkOrderId { get; set; }

        public string WorkOrderCreationDate { get; set; }

        public string WorkOrderCreationTime { get; set; }

        public string WorkOrderSerial { get; set; }

        public string StopTypeTitle { get; set; }

        public string OperationCode { get; set; }

        public string OldMachineCode { get; set; }

        public string DepartmentName { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string MachineName { get; set; }

        public string WorkOrderRegistrar { get; set; }

        public string FaultCode { get; set; }

        public string Description { get; set; }
    }
}
