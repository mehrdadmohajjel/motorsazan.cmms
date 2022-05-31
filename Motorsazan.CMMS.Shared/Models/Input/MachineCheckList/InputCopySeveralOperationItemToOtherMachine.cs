using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineCheckList
{
    public class InputCopySeveralOperationItemToOtherMachine
    {
        public long MachineId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public Tbl_OperationItemIdList[] OperationItemIdList { get; set; }
    }
}
