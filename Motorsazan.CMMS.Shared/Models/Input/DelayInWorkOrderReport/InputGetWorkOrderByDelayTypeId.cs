using System;

namespace Motorsazan.CMMS.Shared.Models.Input.DelayInWorkOrderReport
{
    public class InputGetWorkOrderByDelayTypeId
    {
        public long DelayTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
