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

        public string PictureString { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string HasJob { get; set; }

		public string Technologies { get; set; }

		public int Year { get; set; }

		public string Programme { get; set; }

		public string Languages { get; set; }

		// Must be a number but is actually a string to print percentage signs
		public string Relevance { get; set; }
	}
}