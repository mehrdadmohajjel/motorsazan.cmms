namespace Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport
{
    public class OutputGetOeeChartReportOnMTTRAndMTBFByMachineId
    {
        public decimal Availability { get; set; }

        public decimal Performance { get; set; }

        public decimal Quality { get; set; }

        public decimal OEE { get; set; }

        public string PersianMonthName { get; set; }
    }
}
