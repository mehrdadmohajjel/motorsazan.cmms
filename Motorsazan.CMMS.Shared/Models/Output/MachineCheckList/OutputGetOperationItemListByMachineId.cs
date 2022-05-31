namespace Motorsazan.CMMS.Shared.Models.Output.MachineCheckList
{
    public class OutputGetOperationItemListByMachineId
    {
        public long OperationItemId { get; set; }

        public string OperationItemCode { get; set; }

        public string OperationItemName { get; set; }

        public int JobTimeInMinute { get; set; }

        public int HourDuration { get; set; }

        public int SourceWeek { get; set; }

        public int PeriodInWeek { get; set; }

        public string MaintenanceGroupName { get; set; }

        public long MaintenanceGroupId { get; set; }

        public bool IsActive { get; set; }
    }
}