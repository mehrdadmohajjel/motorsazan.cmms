using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder
{
    public class InputSetWorkOrderStatusToNotConfirmFinishByCustomer
    {
        public long WorKOrderId { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
