﻿using LinkedFMI_UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.ViewModels
{
    public class StudentProfileViewModel
    {
        public int StudentProfileId { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Picture { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string Blog { get; set; }

        [DataType(DataType.Url)]
        public string Facebook { get; set; }

        [DataType(DataType.Url)]
        public string LinkedIn { get; set; }

        [DataType(DataType.Url)]
        public string Twitter { get; set; }

        [DataType(DataType.Upload)]
        public byte[] CV { get; set; }

        public EducationViewModel Education { get; set; }

        public ExperienceViewModel Experience { get; set; }

        public PersonalViewModel Personal { get; set; }
    }
}