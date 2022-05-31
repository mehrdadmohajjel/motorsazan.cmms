using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NoneProductiveWorkOrder
{
    public class InputGetNoneProductiveWorkOrderByCondition
    {
        public long WorkOrderStatusTypeId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
    }
}
