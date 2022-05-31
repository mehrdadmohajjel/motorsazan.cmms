namespace Motorsazan.CMMS.Shared.Models.Output.DailyRepairsReports
{
    public class OutputGetMaintenanceDailyReportByCondition
    {
        public long WorkOrderId { get; set; }

        public long WorkOrderSerial { get; set; }

        public string WorkOrderType { get; set; }

        public string DepartmentTitle { get; set; }

        public string WorkOrderCreationDate { get; set; }

        public string Description { get; set; }

        public string StopTypeTitle { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string OperationItemCode { get; set; }

        public string FinishDate { get; set; }

        public int RequestNo { get; set; }

        public int PreRequestNo { get; set; }
    }
}