namespace Motorsazan.CMMS.Shared.Models.Output.OCEReport
{
    public class OutputGetOCEReportByCondition
    {
        public int EmployeeId { get; set; }

        public int EId { get; set; }

        public string EmployeeName { get; set; }

        public string MaintenanceGroupName { get; set; }

        public decimal PresenceInCompany { get; set; }

        public decimal OverTime { get; set; }

        public decimal DailyPerformance { get; set; }

        public decimal NonePMWorkOrderTimeForEachEmployee { get; set; }

        public decimal PMWorkOrderTimeForEachEmployee { get; set; }

        public decimal TotalPerformanceForEachEmployee { get; set; }

        public decimal CoefficientUtility { get; set; }

        public decimal CoefficientPerformance { get; set; }

        public decimal CoefficientQuality { get; set; }

        public decimal OverallCraftEmployee { get; set; }
    }
}