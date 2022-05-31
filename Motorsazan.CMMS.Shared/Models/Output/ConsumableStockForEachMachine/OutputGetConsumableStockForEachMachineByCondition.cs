namespace Motorsazan.CMMS.Shared.Models.Output.ConsumableStockForEachMachineByCondition
{
    public class OutputGetConsumableStockForEachMachineByCondition
    {
        public long HavaleWorkOrderReferralId { get; set; }

        public long MachineId { get; set; }

        public long StockId { get; set; }

        public string StockName { get; set; }
         
        public string StockCode { get; set; }

        public string RackCode { get; set; }

        public string TechNO { get; set; }

        public int StockCount { get; set; }

        public string DepartmentName { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

    }
}
