using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum OperationType
    {
        [Display(Name = "فعالیت")]
        Activity = 0,

        [Display(Name = "تاخیر")]
        Delay = 1
    }
}