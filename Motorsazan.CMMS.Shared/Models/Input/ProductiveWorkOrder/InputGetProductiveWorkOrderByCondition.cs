using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder
{
    public class InputGetProductiveWorkOrderByCondition
    {
        public long WorkOrderStatusTypeID { get; set; } = 0;

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }

        public int UserID { get; set; }
    }
}
