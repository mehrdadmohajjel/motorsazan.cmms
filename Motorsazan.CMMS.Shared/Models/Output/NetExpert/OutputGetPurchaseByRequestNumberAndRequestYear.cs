namespace Motorsazan.CMMS.Shared.Models.Output.NetExpert
{
    public class OutputGetPurchaseByRequestNumberAndRequestYear
    {
        public string StockName { get; set; }

        public string RackCode { get; set; } 

        public string TechNO { get; set; }

        public string StockCode { get; set; }

        public decimal RequestCount { get; set; }

        public decimal RemainingCount { get; set; }

        public string DeliverLocation { get; set; }

        public string RequestCode { get; set; }

        public string RequestYear { get; set; }

        public string StockClass { get; set; }

        public string FinalSerialNO { get; set; }


    }
}
