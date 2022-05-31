using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum ReportFilterType
    {
        [Display(Name = "بازه زمانی")]
        SpecialPeriodDate = 1,

        [Display(Name = "سفارشکارهای بدون شماره حواله")]
        WorkOrderWithoutNewReceipt = 2
    }
}
