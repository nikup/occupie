using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
		[Display(Name = "Име")]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string Link { get; set; }

        [Required]
		[Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}