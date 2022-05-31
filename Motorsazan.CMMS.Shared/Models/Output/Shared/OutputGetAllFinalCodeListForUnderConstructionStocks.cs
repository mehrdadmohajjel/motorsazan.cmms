namespace Motorsazan.CMMS.Shared.Models.Output.Shared
{
    public class OutputGetAllFinalCodeListForUnderConstructionStocks
    {
        public long Id { get; set; }

        public string StockCode { get; set; }

        public string StockEnglishName { get; set; }

        public string StockName { get; set; }

        public string Class { get; set; }

        public string RackCode { get; set; }

        public string Unit { get; set; }

        public string TechNO { get; set; }

        public string Edition { get; set; }

        public bool IsActive { get; set; }
    }
}