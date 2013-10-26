using LinkedFMI_UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.ViewModels.OfferViewModels
{
    public class OfferAllViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DailyWorkTime { get; set; }

        public string OfferLevel { get; set; }

        public string OfferType { get; set; }

        public string Technologies { get; set; }

        public string EmployerTitle { get; set; }
    }
}