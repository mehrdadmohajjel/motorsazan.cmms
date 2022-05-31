using System;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineCheckList
{
    public class InputGetOperationItemListByMachineId
    {
        public long OperationItemTypeId { get; set; }

        public long MachineId { get; set; }
    }
}
