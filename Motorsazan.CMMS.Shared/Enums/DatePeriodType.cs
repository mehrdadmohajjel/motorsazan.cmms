using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum DatePeriodType
    {
        [Display(Name = "بازه زمانی خاص")]
        CustomPeriod = 0,

        [Display(Name = "از ابتدای سال")]
        ThisYear = 1,

        [Display(Name = "شش ماه اخیر")]
        RecentSixMonths = 2,

        [Display(Name = "سه ماه اخیر")]
        RecentThreeMonths = 3,

        [Display(Name = "یک ماه اخیر")]
        RecentMonth = 4,

        [Display(Name = "یک هفته اخیر")]
        RecentWeek = 5,

        [Display(Name = "روز قبل")]
        Yesterday = 6,

        [Display(Name = "روزجاری")]
        CurrentDay = 7
    }

}