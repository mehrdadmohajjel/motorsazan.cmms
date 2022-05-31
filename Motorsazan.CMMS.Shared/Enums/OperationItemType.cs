using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum OperationItemType
    {
        [Display(Name = "پیشگیرانه")]
        Predictive = 1,

        [Display(Name = "اتفاقی")]
        Accidental = 2 ,

        [Display(Name = "چک لیست")]
        Checklist = 3
    }
}