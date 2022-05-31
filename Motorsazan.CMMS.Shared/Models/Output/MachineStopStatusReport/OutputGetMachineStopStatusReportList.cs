namespace Motorsazan.CMMS.Shared.Models.Output.MachineStopStatusReport
{
    public class OutputGetMachineStopStatusReportList
    {
        public long MachineId { get; set; }

        public int StopCount { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string DepartmentName { get; set; }

        public string PersianEventDate { get; set; }
    }
}
