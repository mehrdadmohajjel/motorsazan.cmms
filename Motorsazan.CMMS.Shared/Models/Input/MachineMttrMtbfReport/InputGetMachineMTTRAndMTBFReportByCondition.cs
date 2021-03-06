using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Enums;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineMttrMtbfReport
{
    public class InputGetMachineMTTRAndMTBFReportByCondition
    {
        public int MachineId { get; set; }

        public int TimeType { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }
    }
}
