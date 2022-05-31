namespace Motorsazan.CMMS.Shared.Models.Output.Shared
{
    public class OutputGetAllStockListWhichHasProductionLine
    {
        public long CodeID { get; set; }

        public string RackCode{ get; set; }

        public string StockCode{ get; set; }

        public string StockEnglishName{ get; set; }

        public string StockName{ get; set; }

        public string TechNO{ get; set; }
    }
}