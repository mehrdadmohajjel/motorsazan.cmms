namespace Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder
{
    public class OutputGetProductiveWorkOrderList
    {
        public long WorkOrderID { get; set; }
        public long WorkOrderStatusID { get; set; }
        public string WorkOrderStatusTitle { get; set; }
        public string WorkOrderStopTitle { get; set; }
        public string MaintenanceGroupTitle { get; set; }
        public string WorkOrderType { get; set; }
        public string WorkOrderSerial { get; set; }
    }
}
