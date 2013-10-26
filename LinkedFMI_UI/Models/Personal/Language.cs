using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkedFMI_UI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkedFMI_UI.Models
{
    public class Language
    {
        [Key]
        public int LangId { get; set; }

        [Required]
        [Display(Name = "Език")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Равнище")]
        public Proficiency Proficiency { get; set; }

        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}