using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Occupie.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Оценка")]
        public double? Grade { get; set; }

        [Display(Name = "Кредити")]
        public double? Credits { get; set; }

        public bool IsVisible { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        //public int? BachelorId { get; set; }
        //public virtual Bachelor Bachelor { get; set; }

        //public int? MastersId { get; set; }
        //public virtual Masters Masters { get; set; }

        //public int? DoctorateId { get; set; }
        //public virtual Doctorate Doctorate { get; set; }
    }
}