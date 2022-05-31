using System;

namespace Motorsazan.CMMS.Shared.Models.Output.StockMan
{
    public class OutputGetAddingConsumable
    {
        public long WorkOrderId { get; set; }

        public long RecipeNumber { get; set; }

        public string StockName { get; set; }

        public int Count { get; set; }

        public string AddressInStore { get; set; }

        public string Price { get; set; }
    }
}
