namespace Motorsazan.CMMS.Shared.Models.Output.ItemOperationReport
{
    public class OutputGetItemOperationReportByCondtion
    {
        public long OperationItemId { get; set; }

        public string OperationItemName { get; set; }

        public string OperationItemCode { get; set; }

        public string ParentMachineName { get; set; }

        public string ChildMachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public decimal JobTimeInMinute { get; set; }

        public int PeriodInMinute { get; set; }

        public int SourceWeek { get; set; }

        public int PeriodWeek { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string DepartmentName { get; set; }
    }
}