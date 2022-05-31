namespace Motorsazan.CMMS.Shared.Models.Output.DailyWorkOrderPrint
{
    public class OutputGetDailyWorkOrderPrintReport
    {
        public long WorkOrderID { get; set; }
        public string WorkOrderSerial { get; set; }
        public string WorkOrderType { get; set; }
        public string EmployeeName { get; set; }
        public string SubDepartment { get; set; }
        public long StopTypeID { get; set; }
        public long MaintenanceGroupID { get; set; }
        public string MaintenanceGroupName { get; set; }
        public long WorkOrderStatusID { get; set; }
        public string WorkOrderStatusTitle { get; set; }
        public string WorkOrderStopTitle { get; set; }
    }
}
