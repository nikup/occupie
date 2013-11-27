using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupie.ViewModels
{
    public class StudentAllViewModel
    {
        public int Id { get; set; }

        public string PictureString { get; set; }

        public string FullName { get; set; }

        public string CurrentJob { get; set; }

		public List<string> Technologies { get; set; }

		public int Year { get; set; }

		public string Programme { get; set; }

		public List<string> Languages { get; set; }

		// Must be a number but is actually a string to print percentage signs
		public string Relevance { get; set; }
	}
}