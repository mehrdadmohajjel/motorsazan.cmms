using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.DomainModels
{
    public class Tbl_MachineFileDetailList
    {
        [StoredProcedureParameter(Size = 200)]
        public string FileName { get; set; }

        [StoredProcedureParameter(Size = 100)]
        public string FileTitle { get; set; }
    }
}
