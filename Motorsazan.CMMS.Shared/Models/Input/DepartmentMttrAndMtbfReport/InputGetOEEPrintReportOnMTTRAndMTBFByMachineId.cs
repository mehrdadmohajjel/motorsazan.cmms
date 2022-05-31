using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.DepartmentMttrAndMtbfReport
{
    public class InputGetOEEPrintReportOnMTTRAndMTBFByMachineId
    {
        public long MachineId { get; set; }

        public int TimeType { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }

    }
}
