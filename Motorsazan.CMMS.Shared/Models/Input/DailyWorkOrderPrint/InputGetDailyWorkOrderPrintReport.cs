using System;

namespace Motorsazan.CMMS.Shared.Models.Input.DailyWorkOrderPrint
{
    public class InputGetDailyWorkOrderPrintReport
    {
        public long StatusID { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
