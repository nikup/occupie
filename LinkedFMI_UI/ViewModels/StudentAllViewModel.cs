using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.ViewModels
{
    public class StudentAllViewModel
    {
        public int Id { get; set; }

        public byte[] Picture { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool HasJob { get; set; }

		public string Technologies { get; set; }

		public int Year { get; set; }

		public string Programme { get; set; }

		public string Languages { get; set; }

		public double Relevance { get; set; }
	}
}