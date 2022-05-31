namespace Motorsazan.CMMS.Shared.Models.Output.NetExpert
{
    public class OutputGetPreventiveOperationItemListByWorkOrderId
    {
        public long OperationItemId { get; set; }

        public string OperationItemName { get; set; }

        public string OperationItemCode { get; set; }

        public long MaintenanceGroupId { get; set; }

        public int PersonHour { get; set; }

        public long PMSchedulingInfoId { get; set; }
    }
}