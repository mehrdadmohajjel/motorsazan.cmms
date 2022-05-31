
namespace Motorsazan.CMMS.Shared.Models.Input.MachineCheckList
{
    public class InputCopyOneOperationItemToOtherMachine
    {
        public long OperationItemId { get; set; }

        public long CurrentMachineId { get; set; }

        public long NewMachineId { get; set; }
    }
}
