using System;

namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetPlcMachineSpindlePerformanceReportByMachineId
    {
        public long MachinePLCFileDataId { get; set; }

        public long MachineId { get; set; }

        public string PersianCreationDate { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public decimal PalletChanger { get; set; }

        public string CreateTime { get; set; }

        public int ToolChangerCount { get; set; }

        public int TimeToPalletChange { get; set; }

        public DateTime CreateDate { get; set; }
    }
}