using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupie.Enums
{
    public static class EnumTranslator
    {
        public static Dictionary<OfferType, string> Types { get; private set; }
        public static Dictionary<Level, string> Levels { get; private set; }
        public static Dictionary<Proficiency, string> Proficiencies { get; private set; }
        public static Dictionary<Specialty, string> Specialities { get; private set; }
        public static Dictionary<WorkStatus, string> WorkStatuses { get; private set; }
        public static Dictionary<WorkTime, string> WorkTimes { get; private set; }
        public static Dictionary<int?, string> Months { get; private set; }

        static EnumTranslator()
        {
            Types = new Dictionary<OfferType, string>();
            Types.Add(OfferType.Dev, "Разработчик");
            Types.Add(OfferType.QA, "QA инженер");
            Types.Add(OfferType.Support, "Поддръжка");
            Types.Add(OfferType.SysAdmin, "Системен администратор");

            Levels = new Dictionary<Level, string>();
            Levels.Add(Level.Intern, "Стажант");
            Levels.Add(Level.Junior, "Младши");
            Levels.Add(Level.Senior, "Старши");
            Levels.Add(Level.Middle, "Средно");
            Levels.Add(Level.ProjectManager, "Ръководител на проект");

            Proficiencies = new Dictionary<Proficiency, string>();
            Proficiencies.Add(Proficiency.Excellent, "Отлично");
            Proficiencies.Add(Proficiency.Advanced, "Напреднало");
            Proficiencies.Add(Proficiency.Intermediate, "Междинно");
            Proficiencies.Add(Proficiency.Basic, "Основно");

            Specialities = new Dictionary<Specialty, string>();
            Specialities.Add(Specialty.Applied_Mathematics, "приложна математика");
            Specialities.Add(Specialty.Computer_Science, "компютърни науки");
            Specialities.Add(Specialty.Informatics, "информатика");
            Specialities.Add(Specialty.Information_Systems, "информационни системи");
            Specialities.Add(Specialty.Mathematics, "математика");
            Specialities.Add(Specialty.Mathematics_and_Informatics, "математика и информатика");
            Specialities.Add(Specialty.Software_Engineering, "софтуерно инженерство");
            Specialities.Add(Specialty.Statistics, "статистика");

            WorkStatuses = new Dictionary<WorkStatus, string>();
            WorkStatuses.Add(WorkStatus.looking_for_a_job, "Търси си работа");
            WorkStatuses.Add(WorkStatus.looking_for_an_internship, "Търси си стаж");
            WorkStatuses.Add(WorkStatus.looking_for_a_job_or_internship, "Търси си работа/стаж");
            WorkStatuses.Add(WorkStatus.not_looking, "Не си търси работа/стаж");
            WorkStatuses.Add(WorkStatus.not_specified, "Не е посочено");


            WorkTimes = new Dictionary<WorkTime, string>();
            WorkTimes.Add(WorkTime.eight_hours, "8-часов работен ден");
            WorkTimes.Add(WorkTime.six_hours, "6-часов работен ден");
            WorkTimes.Add(WorkTime.four_hours, "4-часов работен ден");
            WorkTimes.Add(WorkTime.from_home, "От вкъщи");
            WorkTimes.Add(WorkTime.not_specified, "Не е посочено");

            Months = new Dictionary<int?, string>();
            Months.Add(1, "Януари");
            Months.Add(2, "Февруари");
            Months.Add(3, "Март");
            Months.Add(4, "Април");
            Months.Add(5, "Май");
            Months.Add(6, "Юни");
            Months.Add(7, "Юли");
            Months.Add(8, "Август");
            Months.Add(9, "Септември");
            Months.Add(10, "Октомври");
            Months.Add(11, "Ноември");
            Months.Add(12, "Декември");
        }
    }
}