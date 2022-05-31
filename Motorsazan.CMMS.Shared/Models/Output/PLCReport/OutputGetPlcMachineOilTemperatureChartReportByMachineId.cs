namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetPlcMachineOilTemperatureChartReportByMachineId
    {
        public string PersianCreationDate { get; set; }

        public long MachineId { get; set; }

        public decimal MaxTemperature { get; set; }

        public decimal MinTemperature { get; set; }
    }
}