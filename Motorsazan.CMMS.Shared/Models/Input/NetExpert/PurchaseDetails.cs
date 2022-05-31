namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class PurchaseDetails
    {
        public string StockCode { get; set; }

        public decimal RequestCount { get; set; }

        public decimal RemainingCount { get; set; }

        public string DeliverLocation { get; set; }

        public string RequestCode { get; set; }

        public int RequestYear { get; set; }

        public string StockClass { get; set; }

        public string StockFinalSerialNO { get; set; }
    }
}
