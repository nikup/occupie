using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkedFMI_UI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkedFMI_UI.Models
{
    public class Technology
    {
        [Key]
        public int TechId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Proficiency Proficiency { get; set; }

        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}