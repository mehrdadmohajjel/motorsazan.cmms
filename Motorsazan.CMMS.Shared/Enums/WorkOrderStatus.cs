using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum WorkOrderStatus
    {
        [Display(Name = "همه سفارشکارها")]
        AllWorkOrders = 0,

        [Display(Name = "سفارشکارهای جدید")]
        NewWorkOrders = 1,

        [Display(Name =" سفارشکارهای دارای حواله جدید")]
        WorkOrderWithNewReceipt = 2,

        [Display(Name="سفارشکار های دارای کالای بدون حواله")]
        WithoutReceiptWorkOrders = 3,

        [Display(Name="سفارشکارهای تکمیل شده")]
        CompletedWorkOrders = 4
    }
}
