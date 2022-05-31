using System;

namespace Motorsazan.CMMS.Shared.Models.Output.StockMan
{
    public class OutputGetWorkOrderPerformedOperation
    {
        public string OperationType { get; set; }

        public string Expert { get; set; }

        public long Time { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public long RequestNumber { get; set; }

        public long PreRequestNumber { get; set; }
    }
}
