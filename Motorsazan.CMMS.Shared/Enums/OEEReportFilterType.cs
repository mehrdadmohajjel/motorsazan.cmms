using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum OEEReportFilterType
    {
        [Display(Name = "انتخاب با دستگاه")]
        SelectWithMachine = 0,

        [Display(Name = "انتخاب با دپارتمان")]
        SelectWithDepartment = 1,

        [Display(Name = "انتخاب کلی")]
        SpecialPeriodDate = 2
    }
}
