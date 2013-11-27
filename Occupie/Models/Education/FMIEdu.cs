using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Occupie.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occupie.Models
{
    public class FMIEdu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int FMIId { get; set; }

        public virtual Bachelor Bachelor { get; set; }

        public virtual Masters Master { get; set; }

        public virtual Doctorate Doctor { get; set; }
    }
}