using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupie.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Offer> Offers { get; set; }

        public IEnumerable<StudentAllViewModel> Students { get; set; }

        public IEnumerable<EmployerAllViewModel> Employers { get; set; }

        public Student Student { get; set; }

        public bool ShowEmailWarning { get; set; }
    }
}