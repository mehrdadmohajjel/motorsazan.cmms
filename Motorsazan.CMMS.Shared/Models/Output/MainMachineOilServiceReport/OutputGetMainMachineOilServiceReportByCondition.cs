namespace Motorsazan.CMMS.Shared.Models.Output.MainMachineOilServiceReport
{
    public class OutputGetMainMachineOilServiceReportByCondition
    {
        public long OilServiceId { get; set; }

        public long WorkOrderSerial { get; set; }

        public string WorkOrderCreationDate { get; set; }

        public string StockName { get; set; }

        public string RackCode { get; set; }

        public decimal Quantity { get; set; }

        public string MeasurementUnitShowName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalCost { get; set; }

        public string Description { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string IsUsedOilNormal { get; set; }
    }
}