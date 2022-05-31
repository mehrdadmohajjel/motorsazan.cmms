using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputAddSubMachine
    {
        public long MachineParentId { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string SubMachineName { get; set; }

        public int UserId { get; set; }
    }
}