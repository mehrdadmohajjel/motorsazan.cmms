using System;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.OperationItem
{
    public class InputAddFaultOperationItem
    {
        [StoredProcedureParameter(Size = 1000)]
        public string OperationItemName { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string OperationItemCode { get; set; }

        public long MachineId { get; set; }

        public long MaintenanceGroupId { get; set; }
    }
}