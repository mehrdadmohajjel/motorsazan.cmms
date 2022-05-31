using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NoneProductiveWorkOrder
{
    public class InputAddNoneProductiveWorkOrder
    {

        public int DepartmentId { get; set; }

        public long MaintenanceGroupId { get; set; }

        public long StopTypeId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public long? RelativeWorkOrderId { get; set; }

    }
}
