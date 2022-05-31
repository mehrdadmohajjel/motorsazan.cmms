using System;

namespace Motorsazan.CMMS.Shared.Models.Output.Shared
{
    public class OutputGetStockProductionLineByID
    {
        public long StockProductionLineID { get; set; }

        public int DepartmentID { get; set; }

        public string DepartmentTitle { get; set; }

        public long StockID { get; set; }

        public string RackCode { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public string StockEnglishName { get; set; }

        public bool IsPrimary { get; set; }

        public int OperationUserID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}