namespace Motorsazan.CMMS.Shared.Models.Output.Inspection
{
    public class OutputGetMainMachineListBySubDepartmentId
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }
    }
}