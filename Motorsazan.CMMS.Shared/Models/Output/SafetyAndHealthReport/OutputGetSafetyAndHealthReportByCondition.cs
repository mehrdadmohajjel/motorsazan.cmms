namespace Motorsazan.CMMS.Shared.Models.Output.SafetyAndHealthReport
{
    public class OutputGetSafetyAndHealthReportByCondition
    {
        public long WorkOrderId { get; set; }

        public string WorkOrderSerial { get; set; }

        public string WorkOrderStatusTypeShowName { get; set; }

        public string PersianCreationDate { get; set; }

        public string RequesterUser { get; set; }

        public int EID { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string WorkOrderType { get; set; }

        public string OldMachineCode { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }




    }
}
