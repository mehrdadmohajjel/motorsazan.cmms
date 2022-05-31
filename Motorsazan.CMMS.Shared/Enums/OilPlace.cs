using System.ComponentModel;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum OilPlace
    {
        [Description("انتخاب با دستگاه")]
        انتخاب_با_دستگاه = 0,

        [Description("مخزن مرکزی")]
        مخزن_مرکزی = 1,
        
        [Description("روغن کاری")]
        روغن = 2,
        
        [Description("هیدورلیک")]
        هیدرولیک = 3,
        
        [Description("شستشو")]
        شستشو = 4,
        
        [Description("سیستم خنک کاری")]
        سیستم_خنک_کاری = 5,
        
        [Description("مومیایی")]
        مومیایی = 6,
        
        [Description("گریس کاری")]
        گریس_کاری = 7
    }
}
