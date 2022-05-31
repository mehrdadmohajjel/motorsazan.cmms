namespace Motorsazan.CMMS.Shared.Models.Output.DelayInWorkOrderReport
{
    public class OutputGetDelayInWorkOrderReportByCondition
    {
        public long DelayTypeId { get; set; }

        public int DurationInMinute { get; set; }

        public int ActionRepeatCount { get; set; }

        public string DelayTitle { get; set; }
    }
}
