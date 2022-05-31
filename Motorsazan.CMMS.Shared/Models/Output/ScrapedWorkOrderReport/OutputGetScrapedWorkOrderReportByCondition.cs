namespace Motorsazan.CMMS.Shared.Models.Output.ScrapedWorkOrderReport
{
    public class OutputGetScrapedWorkOrderReportByCondition
    {
        public long WorkOrderId { get; set; }

        public string WorkOrderStatusTypeShowName { get; set; }

        public string WorkOrderSerial { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string ExpertDescription { get; set; }

        public string ScrapSolution { get; set; }

        public string IsScrapAccepted { get; set; }

        public string ScrapLicenseNumber { get; set; }

        public string MaintenanceGroupName { get; set; }

    }
}
