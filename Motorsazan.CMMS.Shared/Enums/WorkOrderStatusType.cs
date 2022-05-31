using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum WorkOrderStatusType
    {
        [Display(Name = "جدید")]
        New = 1,

        [Display(Name = "ارجاع شده")]
        Referred = 2,

        [Display(Name = "منتظر قطعه")]
        StockWaiting = 3,

        [Display(Name = "اتمام کار نت(اتمام اولیه)")]
        Finished = 4,

        [Display(Name = "اتمام تعمیر(اتمام نهایی)")]
        FinalFinished = 5,

        [Display(Name = "عدم تایید اتمام")]
        FinishRejected = 6,

        [Display(Name = "تایید اتمام")]
        FinishAccepted = 7,

        [Display(Name = "ابطال")]
        Canceled = 8,

        [Display(Name = "تایید نشده")]
        Unconfirmed = 9,

        [Display(Name = "منتظر تایید")]
        ConfirmWaiting = 10,

        [Display(Name = "ارجاع به گروه جدید")]
        ReferredToAnotherGroup = 11,
    }
}