using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputSendWorkOrderToMaintenanceGroupMember
    {
        public long WorkOrderId { get; set; }

        public long MaintenanceGroupId { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }

        public int UserId { get; set; }

        public int ReceiverEmployeeId { get; set; }
    }
}