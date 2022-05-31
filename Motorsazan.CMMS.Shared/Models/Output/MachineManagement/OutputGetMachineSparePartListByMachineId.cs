namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineSparePartListByMachineId
    {
        public long MachineSparePartId { get; set; }

        public string StockName { get; set; }

        public string StockCode { get; set; }

        public string RackCode { get; set; }

        public string InternalSupply { get; set; }

        public string TechnicalCharacteristicsOfCatalog { get; set; }

        public decimal CurrentCount { get; set; }

        public long CodeId { get; set; }
    }
}