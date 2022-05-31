namespace Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport
{
    public class OutputGetMTTRAndMTBFPerformanceCostReportByMachineId
    {
        public long WorkOrderId { get; set; }

        public string WorkOrderSerial { get; set; }

        public string OldMachineCode { get; set; }

        public string MachineName { get; set; }

        public string WorkOrderTypeTitle { get; set; }

        public string FaultCode { get; set; }

        public string FaultReason { get; set; }

        public string WorkOrderCreationDate { get; set; }

        public decimal Salary { get; set; }
       
        public decimal MaterialCost { get; set; }

        public decimal TotalCost { get; set; }



    }
}
