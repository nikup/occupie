using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Occupie.Enums;
using System.ComponentModel.DataAnnotations;

namespace Occupie.Models
{
    public class Technology
    {
        [Key]
        public int TechId { get; set; }

        [Required(ErrorMessage = "���� ���� � ������������.")]
        [StringLength(20)]
        [Display(Name="����������")]
        public string Name { get; set; }

        [Required(ErrorMessage = "���� ���� � ������������.")]
        [Display(Name = "�������")]
        public Proficiency Proficiency { get; set; }

        //public string StudentId { get; set; }
        //public virtual Student Student { get; set; }
    }
}