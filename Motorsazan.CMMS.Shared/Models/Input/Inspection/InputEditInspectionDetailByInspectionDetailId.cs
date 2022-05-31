using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.Inspection
{
    public class InputEditInspectionDetailByInspectionDetailId
    {
        public long InspectionDetailId { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string ActionName { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Decimal)]
        public decimal PersonHour { get; set; }
    }
}
