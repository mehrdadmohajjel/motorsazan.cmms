using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputRemoveUploadedAttachmentsOfMachine
    {
        [StoredProcedureParameter(Size = 10)]
        public string MachineId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured, ParameterName = "@FileNameList")]
        public Tbl_FileName[] FileNames { get; set; }
    }
}