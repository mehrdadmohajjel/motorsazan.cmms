using System;

namespace Motorsazan.CMMS.Shared.Models.Output.StockMan
{
    public class OutputEditConsumablesManagementByWorkOrder
    {
        public long WorkOrderId { get; set; }

        public long RecipeNumber { get; set; }

        public string StockName { get; set; }

        public long StockCount { get; set; }

        public string AddressInWarehouse { get; set; }

        public string RecipeNumberStatus { get; set; }

    }
}