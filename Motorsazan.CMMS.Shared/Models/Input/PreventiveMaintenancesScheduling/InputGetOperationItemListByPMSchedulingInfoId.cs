using Motorsazan.CMMS.Shared.Attributes;
using System.Data;

namespace Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesScheduling
{
    public class InputGetOperationItemListByPMSchedulingInfoId
    {
        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public DataTable PMSchedulingInfoIdList { get; set; }
    }
}
