using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineCheckList
{
    public class InputAddCheckListOperationItem
    {
        [StoredProcedureParameter(Size = 500)]
        public string OperationItemName { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string OperationItemCode { get; set; }

        public long MachineId { get; set; }
        
        public long MaintenanceGroupId { get; set; }
    }
}