namespace Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesScheduling
{
    public class OutputGetMainMachineListByMainDepartmentIdAndSubDepartmentId
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }
    }
}
