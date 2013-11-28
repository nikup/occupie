using Occupie.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupie.ViewModels.OfferViewModels
{
	public class OfferFilterViewModel
	{
		public int Id { get; set; }

		public string Technologies { get; set; }

		public string EmployerTitles { get; set; }

		public List<bool> WorkTimes { get; set; }
		public List<bool> Levels { get; set; }
		public List<bool> Types { get; set; }

		public OfferFilterViewModel()
		{
			this.WorkTimes = new List<bool>(new bool[Enum.GetValues(typeof(WorkTime)).Length]);
			this.Levels = new List<bool>(new bool[Enum.GetValues(typeof(Level)).Length]);
			this.Types = new List<bool>(new bool[Enum.GetValues(typeof(Occupie.Enums.OfferType)).Length]);
		}
	}
}