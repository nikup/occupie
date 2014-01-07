using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Occupie.Enums;
using System.ComponentModel.DataAnnotations;

namespace Occupie.Models
{
    public class Bachelor
    {
        [Key]
        public int BachelorId { get; set; }

		[Display(Name = "Курс")]
        public int CurrentCourse { get; set; }

		[Display(Name = "Специалност")]
        public Specialty Specialty { get; set; }

		[Display(Name = "Година на започване")]
        public int StartYear { get; set; }

		[Display(Name = "Година на завършване")]
        public int EndYear { get; set; }

        [Display(Name = "Бал досега")]
        public double CurrentAverageGrade { get; set; }

        public bool IsAverageGradeVisible { get; set; }

        public virtual List<Subject> Subjects { get; set; }
    }
}