using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.PreventiveItemOperation
{
    public class InputEditPreventiveOperationByOperationItemId
    {
        public long OperationItemId { get; set; }

        public long MaintenanceGroupId { get; set; }

        public int? SourceWeek { get; set; }

        public int? PeriodInWeek { get; set; }

        public int? PeriodInMinute { get; set; }

        public int? JobTimeInMinute { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string OperationItemCode { get; set; }

        [StoredProcedureParameter(Size = 500)]
        public string OperationItemName { get; set; }
    }
}