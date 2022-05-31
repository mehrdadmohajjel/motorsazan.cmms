namespace Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport
{
    public class OutputGetMTTRAndMTBFFaultCodeCountReportByMachineId
    {
        public long OperationItemId { get; set; }

        public string OperationItemCode { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationItemName { get; set; }

        public int OperationItemCodeCount { get; set; }
    }
}
