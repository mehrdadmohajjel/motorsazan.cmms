using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Output.SchedulerWorkOrderPrintReport
{
    public class OutputGetSchedulerWorkOrderReportByCondition
    {
        public long WorkOrderId { get; set; }

        public bool IsPrinted { get; set; }

        public string WorkOrderSerial { get; set; }

        public string WorkOrderStatusTypeShowName { get; set; }

        public string WorkOrderCreationDate { get; set; }

        public string RequesterUser { get; set; }

        public string WorkOrderType { get; set; }

        public string OldMachineCode { get; set; }

        public string DepartmentName { get; set; }

        public string MachineName { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string StopTypeTitle { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Bit)]
        public bool IsPreventive { get; set; }

    }
}
