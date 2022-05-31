using Motorsazan.CMMS.Shared.Models.DomainModels;
using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.NetExpert
{
    public class InputAddActionToWorkOrder
    {
        public long WorkOrderId { get; set; }

        public long? DelayTypeId { get; set; }

        public int UserId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public TblEmployeeIdList[] EmployeeIdList { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Time, Size = 7)]
        public TimeSpan StartTime { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Time, Size = 7)]
        public TimeSpan EndTime { get; set; }

        public int DurationInMinute { get; set; }

        public bool IsActivity { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }


        [IgnoreInStoredProcedureParameters]
        public string[] EmployeeNamesList { get; set; }

    }
}