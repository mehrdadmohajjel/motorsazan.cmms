using System;

namespace Motorsazan.CMMS.Shared.Models.Input.PLCReport
{
    public class InputGetToolsChangerDetailOnSpindle
    {
        public long MachineId { get; set; }

        public int PalletChanger { get; set; }

        public DateTime CreateDate { get; set; }

        public long MachinePLCFileDataId { get; set; }
    }
}