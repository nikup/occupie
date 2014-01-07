using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Occupie.Models
{
    public class Job
    {
        [Key]
		
        public int JobId { get; set; }

		[Display(Name = "Текуща")]
        public bool IsCurrent { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [StringLength(30)]
		[Display(Name = "Позиция")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Това поле е задължително.")]
        [StringLength(20)]
		[Display(Name = "Компания")]
        public string Company { get; set; }

		[Display(Name = "Начален месец")]
        public int StartMonth { get; set; }

		[Display(Name = "Начална година")]
        public int StartYear { get; set; }

		[Display(Name = "Краен Месец")]
        public int? EndMonth { get; set; }

		[Display(Name = "Крайна година")]
        public int? EndYear { get; set; }

        //public string StudentId { get; set; }
        //public virtual Student Student { get; set; }
    }
}