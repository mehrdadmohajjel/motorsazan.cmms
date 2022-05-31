namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetPlcMachineOilPressureChartReportByMachineId
    {
        public string PersianCreationDate { get; set; }

        public long MachineId { get; set; }

        public decimal MaxPressure { get; set; }

        public decimal MinPressure { get; set; }
    }
}