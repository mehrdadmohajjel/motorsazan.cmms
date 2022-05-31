namespace Motorsazan.CMMS.Shared.Models.Output.WorkOrderCostReport
{
    public class OutputGetWorkOrderCostReportByCondition
    {
        public long ActionOrDelayListId { get; set; }

        public string WorkOrderTypeName { get; set; }

        public string StopTypeName { get; set; }

        public string WorkOrderSerial { get; set; }

        public string MainDepartmentName { get; set; }

        public string SubDepartmentName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string PersianWorkOrderCreationDate { get; set; }

        public string PersianWorkOrderEndDate { get; set; }

        public decimal Price { get; set; }

        public decimal Salary { get; set; }


    }
}
