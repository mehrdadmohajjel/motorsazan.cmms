using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder
{
    public class InputAddProductiveWorkOrder
    {
        public long MachineId { get; set; }

        public long MaintenanceGroupId { get; set; }

        public long StopTypeId { get; set; }

        public int UserId { get; set; }

        public int DepartmentId { get; set; }

        public string Description { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Bit)]
        public bool HasScrap { get; set; }

        public long? RelativeWorkOrderId { get; set; }
    }
}
