using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputAddMachineSparePartDocument
    {
        public long MachineSparePartId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public Tbl_MachineFileDetailList[] MachineFileDetailList { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string MechanicalSpecification { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string ElectricalSpecification { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string MadeInCompany { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }

        public long UserId { get; set; }
    }
}