using System;

namespace Motorsazan.CMMS.Shared.Models.Output.StockMan
{
    public class OutputGetRegisterConsumablesByWorkOrder
    {
        public long WorkOrderId { get; set; }

        public long ReferralNumber { get; set; }

        public string Officer { get; set; }

        public long EmployeeNumber { get; set; }

    }
}
