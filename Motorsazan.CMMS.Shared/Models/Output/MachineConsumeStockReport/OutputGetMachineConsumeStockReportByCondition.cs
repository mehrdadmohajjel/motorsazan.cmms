namespace Motorsazan.CMMS.Shared.Models.Output.MachineConsumeStockReport
{
    public class OutputGetMachineConsumeStockReportByCondition
    {
        public long MachineId { get; set; }
        public string MachineName { get; set; }
        public string OldMachineCode { get; set; }
        public string OperationCode { get; set; }
        public string DepartmentName { get; set; }
        public int ConsumedCount { get; set; }
    }
}
