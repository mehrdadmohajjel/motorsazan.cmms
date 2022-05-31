namespace Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder
{
    public class OutputGetWorKOrderReferralListByWorkOrderId
    {
        public long WorkOrderReferralId { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string SenderName { get; set; }

        public string ReceiverName { get; set; }

        public string ShamsiDate { get; set; }

        public string Time { get; set; }

        public string ReferralDescription { get; set; }

        public int EmployeeId { get; set; }

        public long WorkOrderSerial { get; set; }
    }
}
