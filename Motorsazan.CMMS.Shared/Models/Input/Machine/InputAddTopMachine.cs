using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.Machine
{
    public class InputAddTopMachine
    {
        [StoredProcedureParameter(Size = 10)]
        public string BuilderMachineCode { get; set; }

        [StoredProcedureParameter(Size = 200)]
        public string MachineName { get; set; }

        public long MachineLevelId { get; set; }

        public long MachineTypeId { get; set; }

        public long ControlSystemTypeId { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string Model { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string ControlSystemName { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string OperationCode { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string SystemModel { get; set; }

        public bool IsTools { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string OldMachineCode { get; set; }

        public int UserId { get; set; }
    }
}
