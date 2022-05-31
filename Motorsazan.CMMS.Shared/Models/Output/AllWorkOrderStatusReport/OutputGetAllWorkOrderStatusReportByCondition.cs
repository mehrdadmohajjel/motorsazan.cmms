namespace Motorsazan.CMMS.Shared.Models.Output.AllWorkOrderStatusReport
{
    public class OutputGetAllWorkOrderStatusReportByCondition
    {
        public int ID { get; set; }

        public string WorkOrderSerial { get; set; }

        public string WorkOrderStatusTitle { get; set; }

        public string WorkOrderDate { get; set; }

        public string StopTypeTitle { get; set; }

        public string Requester { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string WorkOrderType { get; set; }

        public string OperationItem { get; set; }

        public string SubDepartment { get; set; }

        public string MachineName { get; set; }

        public string MachineCode { get; set; }
    }
}