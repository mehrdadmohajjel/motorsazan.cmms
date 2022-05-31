namespace Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport
{
    public class OutputGetOEEPrintReportOnMTTRAndMTBFByMachineId
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }
       
        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string DepartmentName { get; set; }

        public string EmployeeName { get; set; }

        public string EId { get; set; }

        public string AccessTimeInHour { get; set; }

        public string PureMTTR { get; set; }

        public string PureMTBF { get; set; }

        public string PureEA { get; set; }

        public string Performance { get; set; }

        public string Quality { get; set; }

        public string OEE { get; set; }

        public string AllWorkOrderCount { get; set; }

    }
}
