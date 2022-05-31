using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;

namespace Motorsazan.CMMS.Shared.Models.Input.OperationItem
{
    public class InputAddOperationItemMappingToPMItem
    {
        public long OperationItemId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public TblPMOperationItemIdList[] PMOperationItemIdList { get; set; }

        public int UserId { get; set; }
    }
}