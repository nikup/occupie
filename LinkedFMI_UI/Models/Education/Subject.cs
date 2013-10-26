using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        public string Name { get; set; }

        public double? Grade { get; set; }

        public double? Credits { get; set; }

        public bool IsVisible { get; set; }

        public int? BachelorId { get; set; }
        public virtual Bachelor Bachelor { get; set; }

        public int? MastersId { get; set; }
        public virtual Masters Masters { get; set; }

        public int? DoctorateId { get; set; }
        public virtual Doctorate Doctorate { get; set; }
    }
}