using Motorsazan.CMMS.Shared.Attributes;
using System.Data;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputAddStockWaitingStatusToWorkOrder
    {
        public int WorkOrderId { get; set; }

        public int RequestNumber { get; set; }

        public int RequestYear { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public PurchaseDetails[] PurchaseDetails { get; set; }

        public int UserId { get; set; }

    }
}
