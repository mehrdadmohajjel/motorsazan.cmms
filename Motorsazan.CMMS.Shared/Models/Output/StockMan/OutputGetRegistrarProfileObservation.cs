using System;

namespace Motorsazan.CMMS.Shared.Models.Output.StockMan
{
    public class OutputGetRegistrarProfileObservation
    {
        public long WorkOrderId { get; set; }

        public string ShamsiCreationDateTime { get; set; }

        public long WorkOrderSerial { get; set; }

        public string MachineName { get; set; }

        public string DepartmentName { get; set; }

        public string RegistrantName { get; set; }

        public string Description { get; set; }

    }
}
