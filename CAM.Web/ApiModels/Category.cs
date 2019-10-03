using System;
using System.ComponentModel.DataAnnotations;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Contains data relating to categories used for parts.
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}