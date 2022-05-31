namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetPlcMachinePalletChangerCountChartReportByMachineId
    {
        public string PersianCreationDate { get; set; }

        public long MachineId { get; set; }

        public decimal PalletChangerCount { get; set; }
    }
}