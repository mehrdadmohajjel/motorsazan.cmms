namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetMachinePLCFileList
    {
        public long MachinePLCFileId { get; set; }

        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }
    }
}
