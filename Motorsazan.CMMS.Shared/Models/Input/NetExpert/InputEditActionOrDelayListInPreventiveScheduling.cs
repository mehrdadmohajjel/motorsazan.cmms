using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputEditActionOrDelayListInPreventiveScheduling
    {
        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public Tbl_ActionOrDelayListDetail[] ActionOrDelayListDetailList { get; set; }
    }
}