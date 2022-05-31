namespace Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport
{
    public class OutputGetDepartmentMttrAndMtbfReportByCondition
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public int AccessTimeInHour { get; set; }

        public int PureRepairingTimeInMinute { get; set; }

        public int ImPureRepairingTimeInMinute { get; set; }

        public decimal PureMTTR { get; set; }

        public decimal ImPureMTTR { get; set; }

        public decimal PureMTBF { get; set; }

        public decimal ImPureMTBF { get; set; }

        public decimal? PureReliability { get; set; }

        public decimal? ImPureReliability { get; set; }

        public decimal Availability { get; set; }

        public decimal Performance { get; set; }

        public decimal Quality { get; set; }

        public decimal OEE { get; set; }

        public decimal FinishedWorkOrderCount { get; set; }

        public int NotFinishedWorkOrderCount { get; set; }

        public string DepartmentName { get; set; }

        public decimal PureEA { get; set; }

        public decimal ImPureEA { get; set; }

    }
}
