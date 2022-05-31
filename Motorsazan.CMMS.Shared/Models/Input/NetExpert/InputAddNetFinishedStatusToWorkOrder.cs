using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputAddNetFinishedStatusToWorkOrder
    {
        public long WorkOrderId { get; set; }

        public int UserId { get; set; }

        public long? OperationItemId { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }
    }
}