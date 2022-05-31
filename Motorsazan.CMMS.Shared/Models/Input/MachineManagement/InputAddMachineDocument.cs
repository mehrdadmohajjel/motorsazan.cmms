using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputAddMachineDocument
    {
        public long MachineId { get; set; }

        public long FileTypeId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public Tbl_MachineFileDetailList[] MachineFileDetailList { get; set; }

        public long UserId { get; set; }
    }
}