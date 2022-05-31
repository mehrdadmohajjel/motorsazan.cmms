using Motorsazan.CMMS.Shared.Attributes;
using System.Data;

namespace Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesScheduling
{
    public class InputRemoveSeveralOperationItemByPMSchedulingInfoId
    {
        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public DataTable PMSchedulingInfoIDList { get; set; }

        [StoredProcedureParameter(Size = 2000)]
        public string DeactiveReason { get; set; }
    }
}
