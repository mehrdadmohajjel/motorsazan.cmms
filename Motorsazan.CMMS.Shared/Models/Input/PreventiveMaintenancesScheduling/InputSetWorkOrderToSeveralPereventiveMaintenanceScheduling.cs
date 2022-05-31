using Motorsazan.CMMS.Shared.Attributes;
using System.Data;

namespace Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesScheduling
{
    public class InputSetWorkOrderToSeveralPereventiveMaintenanceScheduling
    {

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public DataTable PMSchedulingInfoDetailList { get; set; }

        public int UserId { get; set; }
    }
}
