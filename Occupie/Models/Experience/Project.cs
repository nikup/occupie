using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Occupie.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
		[Display(Name = "Име")]
        [StringLength(20, ErrorMessage = "Името трябва да бъде под 20 символа.")]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string Link { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
		[Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [StringLength(300, ErrorMessage="Описанието трябва да бъде под 300 символа.")]
        public string Description { get; set; }

        //public string StudentId { get; set; }
        //public virtual Student Student { get; set; }
    }
}