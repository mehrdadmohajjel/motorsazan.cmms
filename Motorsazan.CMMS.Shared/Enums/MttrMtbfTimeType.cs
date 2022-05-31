using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum MttrMtbfTimeType
    {
        [Display(Name = "بر مبنای زمان استاندارد")]
        BasedOnStandardTime = 0,

        [Display(Name = "بر مبنای زمان برنامه ریزی تولید")]
        BasedOnProductionPlanning = 1
    }

}