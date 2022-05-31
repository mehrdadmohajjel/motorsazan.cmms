namespace Motorsazan.CMMS.Shared.Models.Output.NoneProductiveWorkOrder
{
    public class OutputGetNoneProductiveWorkOrderByCondition
    {
        public long WorkOrderId { get; set; }

        public string WorkOrderSerial { get; set; }

        public string StatusName { get; set; }

        public string StopTypeTitle { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string WorkOrderTypeTitle { get; set; }

        public string Description { get; set; }

        public long WorkOrderStatusTypeId { get; set; }

        public long WorkOrderTypeID { get; set; }
        public string ShamsiCreationDateTime { get; set; }
    }
}
