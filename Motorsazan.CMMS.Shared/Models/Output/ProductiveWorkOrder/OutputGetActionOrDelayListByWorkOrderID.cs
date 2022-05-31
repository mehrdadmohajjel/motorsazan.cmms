namespace Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder
{
    public class OutputGetActionOrDelayListByWorkOrderId
    {
        public long ActionOrDelayListId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string ShamsiStartDate { get; set; }

        public string ShamsiEndDate { get; set; }

        public string Description { get; set; }

        public int DurationInMinute { get; set; }

        public string ActionTypeName { get; set; }

        public int RequestNo { get; set; }

        public int PreRequestNo { get; set; }

        public string ShamsiDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

    }
}
