using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.StockMan
{
    public class InputGetAddingConsumable
    { 
        public long WorkOrderReferralId { get; set; }

        public long UserId { get; set; }

        public long StockId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Decimal)]
        public long Quantity { get; set; }
    }
}
