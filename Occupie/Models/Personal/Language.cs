using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Occupie.Enums;
using System.ComponentModel.DataAnnotations;

namespace Occupie.Models
{
    public class Language
    {
        [Key]
        public int LangId { get; set; }

        [Required]
        [Display(Name = "Език")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Равнище")]
        public Proficiency Proficiency { get; set; }

        //public string StudentId { get; set; }
        //public virtual Student Student { get; set; }
    }
}