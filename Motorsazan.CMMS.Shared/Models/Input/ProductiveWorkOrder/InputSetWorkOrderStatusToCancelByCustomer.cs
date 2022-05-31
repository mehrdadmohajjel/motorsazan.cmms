using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder
{
    public class InputSetWorkOrderStatusToCancelByCustomer
    {
        public long WorKOrderId { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
