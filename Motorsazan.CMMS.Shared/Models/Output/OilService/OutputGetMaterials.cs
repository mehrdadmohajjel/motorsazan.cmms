namespace Motorsazan.CMMS.Shared.Models.Output.OilService
{
    public class OutputGetMaterials
    {
        public long StockId { get; set; }

        public string StockName { get; set; }

        public string RackCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
