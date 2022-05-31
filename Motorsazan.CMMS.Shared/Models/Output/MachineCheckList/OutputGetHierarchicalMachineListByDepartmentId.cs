namespace Motorsazan.CMMS.Shared.Models.Output.MachineCheckList
{
    public class OutputGetHierarchicalMachineListByDepartmentId
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public long MachineParentId { get; set; }

        public long MachineTopParentId { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }
    }
}