namespace Motorsazan.CMMS.Shared.Models.Output.Shared
{
    public class OutputGetAllStockForProductionLineByDepartmentID
    {
        public string DepartmentInfo { get; set; }

        public int DepartmentID { get; set; }

        public long StockProductionLineID { get; set; }

        public string StockName { get; set; }

        public string RackCode { get; set; } 

        public string TechNO { get; set; }

        public string CreationDateTime { get; set; }

        public string UserName { get; set; }
    }
}