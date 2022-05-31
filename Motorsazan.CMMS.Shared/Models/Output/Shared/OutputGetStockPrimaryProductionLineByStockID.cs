namespace Motorsazan.CMMS.Shared.Models.Output.Shared
{
    public class OutputGetStockPrimaryProductionLineByStockID
    {
        public long StockProductionLineID { get; set; }

		public string	LineName{ get; set; }

		public int 	UnitCode{ get; set; }

		public int	DepartmentId{ get; set; }

		public string	CostCenter{ get; set; }

        public long StockID { get; set; }

		public bool IsPrimary { get; set; }
    }
}