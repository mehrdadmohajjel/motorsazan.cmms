using System;

namespace Motorsazan.CMMS.Shared.Models.Input.WarehouseDailyReports
{
    public class InputGetDailyWearhouseReportByCondition
    {
        public int ReportType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}