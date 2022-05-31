namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputAssignMachineToEmployee
    {
        public int EmployeeId { get; set; }

        public long MachineId { get; set; }

        public int DepartmentId { get; set; }
    }
}