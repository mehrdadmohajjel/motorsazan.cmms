using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.PreventiveItemOperation
{
    public class InputAddPreventiveOperationItem
    {
        [StoredProcedureParameter(Size = 500)]
        public string OperationItemName { get; set; }

        [StoredProcedureParameter(Size = 10)]
        public string OperationItemCode { get; set; }

        public long MachineId { get; set; }

        public long MiterMeasuringTypeId { get; set; }

        public long MaintenanceGroupId { get; set; }

        public int? PeriodInHour { get; set; }

        public int? JobTimeInMinute { get; set; }

        public int? PeriodInWeek { get; set; }

        public int? SourceWeek { get; set; }
    }
}