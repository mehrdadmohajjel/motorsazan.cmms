using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineCheckList
{
    public class InputEditOperationItemByOperationItemId
    {
        public long OperationItemId { get; set; }

        public long MaintenanceGroupId { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string OperationItemCode { get; set; }

        [StoredProcedureParameter(Size = 500)]
        public string OperationItemName { get; set; }
    }
}