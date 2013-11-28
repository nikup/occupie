using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Occupie.Models
{
    public class HighSchoolEdu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HighSchoolId { get; set; }

        [Display(Name = "Име на училището")]
        [StringLength(20)]
        public string SchoolName { get; set; }

        [Display(Name = "Година на започване")]
        public int? StartYear { get; set; }

        [Display(Name = "Година на завършване")]
        public int? EndYear { get; set; }

        [Range(2.00, 6.00)]
        [Display(Name = "Оценка")]
        public double? Grade { get; set; }
    }
}