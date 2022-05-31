using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputAddCancelStatusToWorkOrder
    {
        public long WorkOrderId { get; set; }

        public int UserId { get; set; }
        
        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }
    }
}