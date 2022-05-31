using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputSendWorkOrderToMaintenanceGroup
    {
        public long WorkOrderId { get; set; }

        public long MaintenanceGroupId { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string ReferralDescription { get; set; }

        public int UserId { get; set; }
    }
}