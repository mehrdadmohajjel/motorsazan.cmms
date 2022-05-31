
namespace Motorsazan.CMMS.Shared.Models.Input.MachineCheckList
{
    public class InputEditOperationItemModal
    {
        public long OperationItemId { get; set; }

        public string MaintenanceGroupName { get; set; }

        public long MaintenanceGroupId { get; set; }

        public string OperationItemCode { get; set; }

        public string OperationItemName { get; set; }
    }
}