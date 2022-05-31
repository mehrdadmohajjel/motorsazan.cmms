namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetStockListByStoreCodeForSparePart
    {
        public long Id { get; set; }

        public string RackCode { get; set; }

        public string StockName { get; set; }

        public string StockCode { get; set; }

        public decimal Quantity { get; set; }
    }
}