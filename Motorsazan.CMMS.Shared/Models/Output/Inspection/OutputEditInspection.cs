namespace Motorsazan.CMMS.Shared.Models.Output.Inspection
{
    public class OutputEditInspection
    {
        public long InspectionType { get; set; }

        public long WorkOrderType { get; set; }

        public int DepartmentId { get; set; }

        public int SubDepartmentId { get; set; }

        public long? MachineId { get; set; }

        public long MaintenanceGroupId { get; set; }

        public string PreferDate { get; set; }

        public decimal NeedTime { get; set; }

        public string FaultDescription { get; set; }

        public int UserId { get; set; } 

    }
}
