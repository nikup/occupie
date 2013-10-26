using LinkedFMI_UI.Enums;
using LinkedFMI_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.ViewModels
{
    public class EducationViewModel
    {
        public List<Job> Jobs { get; set; }

        public List<Project> Projects { get; set; }

        public WorkTime WorkTimePreference { get; set; }
    }
}