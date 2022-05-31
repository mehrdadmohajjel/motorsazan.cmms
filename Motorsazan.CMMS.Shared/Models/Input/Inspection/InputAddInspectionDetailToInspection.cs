using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.Inspection
{
   public class InputAddInspectionDetailToInspection
    {
        public long InspectionId { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string ActionName { get; set; }
        
        public int PersonHour { get; set; }
    }
}
