namespace Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesScheduling
{
    public class OutputGetPreventiveMaintenanceSchedulingListByCondition
    {
        public long PMSchedulingInfoId { get; set; }

        public string MiterMeasuringTypeShowName { get; set; }

        public int DestinationTimeOrWeek { get; set; }

        public int SourceTimeOrWeek { get; set; }

        public string MachineName { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string OperationItemName { get; set; }

        public string OperationItemCode { get; set; }

        public int DoneCount { get; set; }

        public int DurationFromCreationDate { get; set; }

        public string PersianWorkOrderPreferDate { get; set; }

        public string DepartmentName { get; set; }

        public string ParentMachineName { get; set; }

        public string ParentOldMachineCode { get; set; }

        public long OperationItemId { get; set; }

        public string OperationCode { get; set; }
    }
}