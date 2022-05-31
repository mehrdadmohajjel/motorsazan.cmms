namespace Motorsazan.CMMS.Shared.Models.Output.SparePartReport
{
    public class OutputGetMachineSparePartReportByMachineId
    {
        public long MachineSparePartId { get; set; }

        public string StockName { get; set; }

        public string StockCode { get; set; }

        public string RackCode { get; set; }

        public string TechNO { get; set; }

        public string InternalSupply { get; set; }

        public string TechnicalCharacteristicsOfCatalog { get; set; }

        public decimal StockCount { get; set; }

    }
}
