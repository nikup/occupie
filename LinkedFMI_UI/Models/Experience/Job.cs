using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        public bool IsCurrent { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Company { get; set; }

        public int StartMonth { get; set; }

        public int StartYear { get; set; }

        public int? EndMonth { get; set; }

        public int? EndYear { get; set; }

        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}