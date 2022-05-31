namespace Motorsazan.CMMS.Shared.Models.Output.OperationItem
{
    public class OutputGetPreventiveOperationItemListByMachineId
    {
        public long OperationItemId { get; set; }

        public string OperationItemName { get; set; }

        public string OperationItemCode { get; set; }

        public string MaintenanceGroupName { get; set; }
    }
}