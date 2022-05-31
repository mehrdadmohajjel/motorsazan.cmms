using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
   public class InputAddInspectionActionToWorkOrder
    {
        public long WorkOrderId { get; set; }

        public long InspectionDetailId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public EmployeeIdList[] ResposibleEmployeeIDList { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Time)]
        public string StartTime { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Time)]
        public string EndTime { get; set; }

        public int DurationInMinute { get; set; }

        public int UserId { get; set; }
    }
}
