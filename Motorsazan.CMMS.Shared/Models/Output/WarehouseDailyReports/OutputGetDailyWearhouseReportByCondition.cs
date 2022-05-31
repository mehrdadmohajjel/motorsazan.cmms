namespace Motorsazan.CMMS.Shared.Models.Output.WarehouseDailyReports
{
    public class OutputGetDailyWearhouseReportByCondition
    {
        public long HavaleWorkOrderReferralId { get; set; }

        public string WorkOrderStatusTypeShowName { get; set; }

        public long WorkOrderSerial { get; set; }

        public string WorkOrderType { get; set; }

        public string PersianHavalehCreationDate { get; set; }

        public string StockName { get; set; }

        public string RackCode { get; set; }

        public decimal Quantity { get; set; }

        public int HavaleNO { get; set; }

        public string PersianWorkOrderCreationDate { get; set; }

        public string WorkOrderCreationTime { get; set; }

        public string StopTypeTitle { get; set; }

        public string MaintenanceGroupName { get; set; }

        public long WorkOrderId { get; set; }
    }
}
