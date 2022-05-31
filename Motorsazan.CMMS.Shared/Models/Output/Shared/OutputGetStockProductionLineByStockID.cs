namespace Motorsazan.CMMS.Shared.Models.Output.Shared
{
    public class OutputGetStockProductionLineByStockID
    {
 
        public long StockProductionLineID { get; set; }

        public string LineName { get; set; }

        public string CostCenter { get; set; }

		public long StockID { get; set; }

        public bool IsPrimary { get; set; }

        public long OperationUserID { get; set; }

        public string CreationDateTime { get; set; }

        public string RackCode { get; set; }

        public string StockCode { get; set; }

        public string StockEnglishName { get; set; }

        public string StockName { get; set; }

        public string TechNO { get; set; }

        public string OperationUserName { get; set; }

        public int DepartmentId { get; set; }
    }
}