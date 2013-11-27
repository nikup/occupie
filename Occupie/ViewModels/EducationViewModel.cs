using Occupie.Enums;
using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupie.ViewModels
{
    public class EducationViewModel
    {
        public List<Job> Jobs { get; set; }

        public List<Project> Projects { get; set; }

        public WorkTime WorkTimePreference { get; set; }
    }
}