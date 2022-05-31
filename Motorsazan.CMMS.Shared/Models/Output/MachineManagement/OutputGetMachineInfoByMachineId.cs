namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineInfoByMachineId
    {
        public long MachineId { get; set; }
        public string MachineName { get; set; }
        public string OldMachineCode { get; set; }
        public string MachineLocationTitle { get; set; }
    }
}