namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetPlcMachineToolsChangerChartReportByMachineId
    {
        public string PersianCreationDate { get; set; }

        public long MachineId { get; set; }

        public decimal ToolChangerCount { get; set; }
    }
}