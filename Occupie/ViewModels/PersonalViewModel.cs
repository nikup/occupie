using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupie.ViewModels
{
    public class PersonalViewModel
    {
        public DateTime? DateOfBirth { get; set; }

        public string Town { get; set; }

        public List<Language> Languages { get; set; }

        public List<Technology> Technologies { get; set; }

        public List<string> Interests { get; set; }
    }
}