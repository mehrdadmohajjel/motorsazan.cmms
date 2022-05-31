using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;
using System.Data;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputSetHavaleNOListByHavalehWorkOrderId
    {
        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public Tbl_SetHavaleNOListByHavalehWorkOrderId[] HavaleWorkOrderReferralIDList { get; set; }
    }
}
