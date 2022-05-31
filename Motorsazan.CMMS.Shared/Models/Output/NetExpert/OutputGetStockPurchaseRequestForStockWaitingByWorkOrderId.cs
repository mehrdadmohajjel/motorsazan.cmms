namespace Motorsazan.CMMS.Shared.Models.Output.NetExpert
{
    public class OutputGetStockPurchaseRequestForStockWaitingByWorkOrderId
    {
        public long PurchaseRequestId { get; set; }

        public long WorkOrderSerial { get; set; }

        public long StockFinalSerialNo { get; set; }

        public string RackCode { get; set; }

        public string StockName { get; set; }

        public string TechNo { get; set; }
    }
}