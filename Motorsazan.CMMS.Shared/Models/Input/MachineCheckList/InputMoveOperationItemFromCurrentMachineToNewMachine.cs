namespace Motorsazan.CMMS.Shared.Models.Input.MachineCheckList
{
    public class InputMoveOperationItemFromCurrentMachineToNewMachine
    {
        public long OperationItemId { get; set; }

        public long NewMachineId { get; set; }
    }
}