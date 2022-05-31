using System;

namespace Motorsazan.CMMS.Shared.Models.Output.StockMan
{
    public class OutputGetStockManListByCondition
    { 
        public long WorkOrderId { get; set; }

        public string WorkOrderStatusTypeShowName { get; set; }

        public string StopTypeTitle { get; set; }

        public string ShamsiCreationDate { get; set; }

        public long WorkOrderSerial { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string DepartmentName { get; set; }
    }
}
