using System;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputEditMachineBuiltInfo
    {
        public long MachineId { get; set; }
        public DateTime ImportDate { get; set; }
        public string CountryName { get; set; }
        public string BuiltYear { get; set; }
    }
}