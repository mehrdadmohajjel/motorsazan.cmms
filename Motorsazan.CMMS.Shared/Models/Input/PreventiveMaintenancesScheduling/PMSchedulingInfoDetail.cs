using System;

namespace Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesScheduling
{
    public class PMSchedulingInfoDetail
    {
        public long PMSchedulingInfoId { get; set; }

        public long MachineId { get; set; }

        public DateTime PreferDate { get; set; }

        public int PersonHour { get; set; }
    }
}
