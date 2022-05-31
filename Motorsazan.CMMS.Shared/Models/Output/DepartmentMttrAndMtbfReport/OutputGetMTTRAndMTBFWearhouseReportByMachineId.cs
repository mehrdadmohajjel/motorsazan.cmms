namespace Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport
{
    public class OutputGetMTTRAndMTBFWearhouseReportByMachineId
    {
        public long ActionOrDelayListID { get; set; }

        public long WorkOrderSerial { get; set; }

        public string DepartmentName { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string WorkOrderTypeTitle { get; set; }

        public string ExpertName { get; set; }

        public string StockName { get; set; }

        public string RackCode { get; set; }

        public decimal Quantity { get; set; }

        public string MaintenanceGroupName { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalPrice { get; set; }


    }
}
