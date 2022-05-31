using System;

namespace Motorsazan.CMMS.Shared.Models.Output.Inspection
{
    public class OutputGetInspectionByInspectionId
    {
        public int ParentDepartmentId { get; set; }

        public int ChildDepartmentId { get; set; }

        public string Description { get; set; }

        public long InspectionId { get; set; }

        public bool IsProductive { get; set; }

        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public int PersonHour { get; set; }

        public string PreferDate { get; set; }

        public long InspectionTypeId { get; set; }

        public long MaintenanceGroupId { get; set; }

        public long WorkOrderId { get; set; }

        public int InspectionDetailCount { get; set; }
    }
}
