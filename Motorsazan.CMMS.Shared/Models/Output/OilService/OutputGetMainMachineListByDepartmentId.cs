namespace Motorsazan.CMMS.Shared.Models.Output.OilService
{
    public class OutputGetMainMachineListByDepartmentId
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }
    }
}