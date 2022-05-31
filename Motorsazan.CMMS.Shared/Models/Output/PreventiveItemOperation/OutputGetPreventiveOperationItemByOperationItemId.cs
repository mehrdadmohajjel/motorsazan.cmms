namespace Motorsazan.CMMS.Shared.Models.Output.PreventiveItemOperation
{
    public class OutputGetPreventiveOperationItemByOperationItemId
    {
        public long OperationItemId { get; set; }

        public string OperationItemName { get; set; }

        public string OperationItemCode { get; set; }

        public string MaintenanceGroupName { get; set; }

        public long MaintenanceGroupId { get; set; }

        public long MiterMeasuringTypeId { get; set; }

        public int DoneDuration { get; set; }

        public int DonePeriod { get; set; }

        public int SourceWeek { get; set; }

        public int PeriodInWeek { get; set; }
    }
}