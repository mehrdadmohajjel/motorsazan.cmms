namespace Motorsazan.CMMS.Shared.Models.Output.OperationItem
{
    public class OutputGetOperationItemSparePartListByOperationItemId
    {
        public long OperationItemSparePartId { get; set; }

        public string StockName { get; set; }

        public string StockCode { get; set; }

        public string RackCode { get; set; }

        public decimal CurentCount { get; set; }

        public decimal KharidCount { get; set; }

    }
}