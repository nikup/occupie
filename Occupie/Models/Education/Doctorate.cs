using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Occupie.Enums;
using System.ComponentModel.DataAnnotations;

namespace Occupie.Models
{
    public class Doctorate
    {
        [Key]
        public int DoctorateId { get; set; }

		[Display(Name = "Специалност")]
        public string Specialty { get; set; }

		[Display(Name = "Година на започване")]
        public int? StartYear { get; set; }

		[Display(Name = "Година на завършване")]
        public int? EndYear { get; set; }

        public virtual List<Subject> Subjects { get; set; }
    }
}