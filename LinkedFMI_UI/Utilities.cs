using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using LinkedFMI_UI.Enums;

namespace LinkedFMI_UI
{
    public class Utilities
    {
        public static List<SelectListItem> GetHighSchoolYears()
        {
            List<SelectListItem> years = new List<SelectListItem>();
            int startYear = DateTime.Now.Year;
            years.Add(new SelectListItem { Value = "0", Text = "-" });
            years.Add(new SelectListItem { Value = (startYear).ToString(), Text = (startYear).ToString() });

            for (int i = 1; i <= 10; i++)
            {
                years.Add(new SelectListItem { Value = (startYear - i).ToString(), Text = (startYear - i).ToString() });
            }

            return years;
        }

        public static List<SelectListItem> GetWorkTimePreferences()
        {
            List<SelectListItem> preferences = new List<SelectListItem>();
            preferences = Enum.GetValues(typeof(WorkTime))
                                 .Cast<WorkTime>()
                                 .Select(x => new SelectListItem { Value = x.ToString(), Text = EnumTranslator.WorkTimes[x] })
                                 .ToList();
            return preferences;
        }

        public static List<SelectListItem> GetWorkStatuses()
        {
            List<SelectListItem> statuses = new List<SelectListItem>();
            statuses = Enum.GetValues(typeof(WorkStatus))
                                 .Cast<WorkStatus>()
                                 .Select(x => new SelectListItem { Value = x.ToString(), Text = EnumTranslator.WorkStatuses[x] })
                                 .ToList();
            return statuses;
        }

        public static List<SelectListItem> GetProficiencies()
        {
            List<SelectListItem> profs = new List<SelectListItem>();
            profs = Enum.GetValues(typeof(Proficiency))
                                 .Cast<Proficiency>()
                                 .Select(x => new SelectListItem { Value = x.ToString(), Text = EnumTranslator.Proficiencies[x] })
                                 .ToList();
            return profs;
        }

        public static List<SelectListItem> GetMonths()
        {
            List<SelectListItem> months = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            return months;
        }

        public static List<SelectListItem> GetOfferLevels()
        {
            List<SelectListItem> levels = new List<SelectListItem>();
            levels = Enum.GetValues(typeof(Level))
                                 .Cast<Level>()
                                 .Select(x => new SelectListItem { Value = x.ToString(), Text = EnumTranslator.Levels[x] })
                                 .ToList();
            return levels;
        }

        public static List<SelectListItem> GetOfferTypes()
        {
            List<SelectListItem> types = new List<SelectListItem>();
            types = Enum.GetValues(typeof(LinkedFMI_UI.Enums.Type))
                                 .Cast<LinkedFMI_UI.Enums.Type>()
                                 .Select(x => new SelectListItem { Value = x.ToString(), Text = EnumTranslator.Types[x] })
                                 .ToList();
            return types;
        }
    }
}