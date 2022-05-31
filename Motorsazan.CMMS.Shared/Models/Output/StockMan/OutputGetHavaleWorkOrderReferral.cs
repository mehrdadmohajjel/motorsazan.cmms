using System;

namespace Motorsazan.CMMS.Shared.Models.Output.StockMan
{
    public class OutputGetHavaleWorkOrderReferral
    {
        public long HavaleWorkOrderReferralId { get; set; }

        public long WorkOrderSerial { get; set; }

        public int HavaleNO { get; set; }

        public string StockName { get; set; }

        public decimal Quantity { get; set; }

        public string RackCode { get; set; }

        public decimal Price { get; set; }

    }
}
