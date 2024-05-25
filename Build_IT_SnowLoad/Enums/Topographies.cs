using System.ComponentModel.DataAnnotations;

namespace Build_IT_SnowLoads.Enums
{
    /// <summary>
    /// Topography enumerator
    /// </summary>
    public enum Topographies
    {
        None,
        /// <summary>
        /// Flat unobstructed areas exposed on all sides
        /// without, or little shelter afforded by terrain, higher construction works or
        /// trees.
        /// </summary>
        [Display(Name = "Windswept")]
        Windswept,
        /// <summary>
        /// Areas where there is no significant removal of snow
        /// by wind on construction work, because of terrain, other construction works
        /// or trees.
        /// </summary>
        [Display(Name = "Normal")]
        Normal,
        /// <summary>
        /// Areas in which the construction work being
        /// considered is considerably lower than the surrounding terrain or
        /// surrounded by high trees and/or surrounded by higher construction works.
        /// </summary>
        [Display(Name = "Sheltered")]
        Sheltered
    }
}
