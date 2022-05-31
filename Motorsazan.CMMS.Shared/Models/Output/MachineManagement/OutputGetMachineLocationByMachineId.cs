namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineLocationByMachineId
    {
        public long MachineLocationId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string DepartmentName { get; set; }

        public string EmployeeName { get; set; }
    }
}