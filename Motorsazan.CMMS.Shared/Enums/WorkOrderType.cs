using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Enums
{
    public enum WorkOrderType
    {
        [Display(Name = "اتفاقی(تولیدی)")]
        AccidentalProductive = 3,

        [Display(Name = "پیشگیرانه(EM)")]
        PredictiveNoneProductive = 4,

        [Display(Name = "اتفاقی(غیر تولیدی)")]
        AccidentalNoneProductive = 12,

        [Display(Name = "بازرسی")]
        InspectionNoneProductive = 14,

        [Display(Name = "آزمایشگاه شیمی")]
        ChemistryLabNoneProductive = 20,

        [Display(Name = "بهبود و ارتقاع سیستم")]
        ImprovementSystemNoneProductive = 21,

        [Display(Name = "تعمیرات برد")]
        RepairingBoardNoneProductive = 22,
        
        [Display(Name = "تعمیرات تکنولوژی")]
        RepairingTechnologyNoneProductive = 23,

        [Display(Name = "روغنکاری")]
        Lubrication = 24
    }
}