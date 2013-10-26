using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkedFMI_UI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkedFMI_UI.Models
{
    public class Doctorate
    {
        [Key]
        public int DoctorateId { get; set; }

        public string Specialty { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        public virtual List<Subject> Subjects { get; set; }
    }
}