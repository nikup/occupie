using LinkedFMI_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Offer> Offers { get; set; }

        public IEnumerable<StudentAllViewModel> Students { get; set; }

        public IEnumerable<EmployerAllViewModel> Employers { get; set; }

    }
}