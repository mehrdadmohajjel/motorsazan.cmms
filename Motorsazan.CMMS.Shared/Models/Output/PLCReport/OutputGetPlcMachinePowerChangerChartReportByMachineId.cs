namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetPlcMachinePowerChangerChartReportByMachineId
    {
        public string PersianCreationDate { get; set; }

        public long MachineId { get; set; }

        public decimal MaxPower { get; set; }

        public decimal MinPower { get; set; }
    }
}



