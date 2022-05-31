using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.Inspection
{
    public class InputEditTheInspection
    {
        public long InspectionId { get; set; }

        public long InspectionTypeId { get; set; }

        public long MaintenanceGroupId { get; set; }

        public int DepartmentId { get; set; }

        public long? MachineId { get; set; }

        public string Description { get; set; }

        public decimal PersonHour { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime PreferDate { get; set; }
        
        public int UserId { get; set; }
        
        public bool IsProductive { get; set; }

    }
}
