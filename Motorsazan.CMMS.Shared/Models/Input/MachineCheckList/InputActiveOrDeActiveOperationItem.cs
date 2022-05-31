
using System.Data;
using DevExpress.Xpo;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineCheckList
{
    public class InputActiveOrDeActiveOperationItem
    {
        public long OperationItemId { get; set; }

        [StoredProcedureParameter(Size = 2000)]
        public string DeActiveReason { get; set; }
    }
}