using System.ComponentModel.DataAnnotations;

namespace Build_IT_SnowLoads.Enums
{
    public enum Zones
    {
        None = 0,
        [Display(Name = "I")]
        FirstZone = 1,
        [Display(Name = "I-II")]
        BetweenFirst_Second = 3,
        [Display(Name = "II")]
        SecondZone = 2,
        [Display(Name = "II-III")]
        BetweenSecond_Third = 6,
        [Display(Name = "III")]
        ThirdZone = 4,
        [Display(Name = "III-IV")]
        BetweenThird_Fourth = 12,
        [Display(Name = "IV")]
        FourthZone = 8,
        [Display(Name = "III-V")]
        BetweenThird_Fifth = 24,
        [Display(Name = "V")]
        FifthZone = 16
    }
}
