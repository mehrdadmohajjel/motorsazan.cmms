namespace Motorsazan.CMMS.Shared.Models.Output.OEEReport
{
    public class OutputGetOEEReportByCondition
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string DepartmentName { get; set; }

        public decimal Availability { get; set; }

        public decimal Performance { get; set; }

        public decimal Quality { get; set; }

        public decimal OEE { get; set; }
    }
}