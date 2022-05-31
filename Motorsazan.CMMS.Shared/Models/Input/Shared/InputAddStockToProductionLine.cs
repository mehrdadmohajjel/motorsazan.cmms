namespace Motorsazan.CMMS.Shared.Models.Input.Shared
{
    public class InputAddStockToProductionLine
    {
        public long ProductionLineID { get; set; }

        public long StockID { get; set; }

        public int OperationUserID { get; set; }

        public bool IsPrimary { get; set; }
    }
}