namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetToolsChangerDetailOnSpindle
    {
        public long MachinePLCFileDataId { get; set; }

        public long MachineId { get; set; }

        public decimal ToolChanger { get; set; }

        public string CreateDate { get; set; }

        public string CreateTime { get; set; }

        public int TotalTime { get; set; }

        public decimal I { get; set; }

        public decimal P { get; set; }

        public decimal E { get; set; }
    }
}
