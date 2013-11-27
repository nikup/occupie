using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using Occupie.Enums;
using Occupie.Models;

namespace Occupie
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

        public static List<SelectListItem> GetJobYears()
        {
            List<SelectListItem> years = new List<SelectListItem>();
            int startYear = DateTime.Now.Year;
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
            types = Enum.GetValues(typeof(Occupie.Enums.Type))
                                 .Cast<Occupie.Enums.Type>()
                                 .Select(x => new SelectListItem { Value = x.ToString(), Text = EnumTranslator.Types[x] })
                                 .ToList();
            return types;
        }

        public static List<Job> OrderJobs(List<Job> jobs)
        {
            List<Job> orderedJobs = jobs.OrderByDescending(x => x.IsCurrent).ThenByDescending(x => x.EndYear).ThenByDescending(x => x.EndMonth).ToList();

            return orderedJobs;
        }

        public static string GetCorrectPrepositionV(string nextWord)
        {
            if (nextWord != null)
            {
                char[] letters = {'v', 'f', 'в', 'ф'};

                if (letters.Any(x => x == nextWord.ToLower()[0]))
                {
                    return " във ";
                }
            }            

            return " в ";
        }
    }
}