namespace Motorsazan.CMMS.Shared.Models.Output.MachineCheckList
{
    public class OutputGetTopParentMachineAndChildrenByOperationItemId
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public long MachineParentId { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }
    }
}