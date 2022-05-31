using Motorsazan.CMMS.Shared.Enums;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputGetOperationItemListByMaintenanceGroupIdAndMachineId
    {
        public long MaintenanceGroupId { get; set; }

        public long? MachineId { get; set; }

        public OperationItemType OperationItemTypeId { get; set; }
    }
}