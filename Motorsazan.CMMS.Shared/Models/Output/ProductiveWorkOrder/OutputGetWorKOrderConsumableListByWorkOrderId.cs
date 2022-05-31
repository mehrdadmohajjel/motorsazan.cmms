namespace Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder
{
    public class OutputGetWorKOrderConsumableListByWorkOrderId
    {
        public long HavaleWorkOrderReferralId { get; set; }

        public string StockName { get; set; }

        public decimal Quantity { get; set; }

        public string ShamsiDate { get; set; }

        public string WorkOrderSerial { get; set; }

        public string RackCode { get; set; }

        public string HavaleNO { get; set; }

        public string WearhouseConfirmationStatus { get; set; }

        public long WorkOrderId { get; set; }
    }
}
