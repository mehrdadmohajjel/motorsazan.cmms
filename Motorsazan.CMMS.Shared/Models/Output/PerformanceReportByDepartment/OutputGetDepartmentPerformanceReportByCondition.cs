namespace Motorsazan.CMMS.Shared.Models.Output.PerformanceReportByDepartment
{
    public class OutputGetDepartmentPerformanceReportByCondition
    {
        public long ActionOrDelayListId { get; set; }

        public long WorkOrderSerial { get; set; }

        public string StopTypeName { get; set; }

        public string WorkOrderTypeName { get; set; }

        public string DepartmentName { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string ExpertName { get; set; }

        public int EId { get; set; }

        public string PersianWorkOrderCreationDate { get; set; }

        public string PersianWorkOrderEndDate { get; set; }

        public string PersianActionEndDate { get; set; }

        public int DurationInMinute { get; set; }

    }
}
