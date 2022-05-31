namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineLocation
    {
        public long MachineLocationID { get; set; }
        public long MachineID { get; set; }
        public int DepartmentId { get; set; }
        public long EmployeeId { get; set; }
        public string MahcineName { get; set; }
        public string OldMachineCode { get; set; }
        public string DepartmentTitle { get; set; }
        public string EmployeeName { get; set; }
        public string EID { get; set; }

    }
}
