using System;

namespace Motorsazan.CMMS.Shared.Models.Output.OilService
{
    public class OutputGetOilServiceListByCondition
    {
        public long OilServiceId { get; set; }

        public long WorkOrderSerial { get; set; }

        public string PreferDate { get; set; }

        public string StockName { get; set; }

        public string WearhouseCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public bool IsUsedOilNormal { get; set; }
    }
}