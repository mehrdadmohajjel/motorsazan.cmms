namespace Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport
{
    public class OutputGetMTTRAndMTBFPerformanceReportByMachineId
    {
        public long ActionOrDelayListId { get; set; }

        public string WorkOrderSerial { get; set; }

        public string WorkOrderTypeTitle { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }
        
        public string ExpertName { get; set; }

        public string FaultCode { get; set; }
        
        public string FaultResean { get; set; }

        public string MaintenanceGroupName { get; set; }

        public int DurationInMinute { get; set; }

        public decimal Salary { get; set; }



    }
}
