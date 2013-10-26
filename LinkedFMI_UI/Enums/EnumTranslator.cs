using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedFMI_UI.Enums
{
    public static class EnumTranslator
    {
        public static Dictionary<Type, string> Types { get; private set; }
        public static Dictionary<Level, string> Levels { get; private set; }
        public static Dictionary<Proficiency, string> Proficiencies { get; private set; }
        public static Dictionary<Specialty, string> Specialities { get; private set; }
        public static Dictionary<WorkStatus, string> WorkStatuses { get; private set; }
        public static Dictionary<WorkTime, string> WorkTimes { get; private set; }
        static EnumTranslator()
        {
            Types = new Dictionary<Type, string>();
            Types.Add(Type.Dev, "Разработчик");
            Types.Add(Type.QA, "QA инженер");
            Types.Add(Type.Support, "Поддръжка");
            Types.Add(Type.SysAdmin, "Системен администратор");

            Levels = new Dictionary<Level, string>();
            Levels.Add(Level.Intern, "Стажант");
            Levels.Add(Level.Junior, "Младши");
            Levels.Add(Level.Senior, "Старши");
            Levels.Add(Level.Middle, "Средно");
            Levels.Add(Level.ProjectManager, "Ръководител на проект");

            Proficiencies = new Dictionary<Proficiency, string>();
            Proficiencies.Add(Proficiency.basic, "Основно");
            Proficiencies.Add(Proficiency.advanced, "Напреднал");
            Proficiencies.Add(Proficiency.excellent, "Отлично");
            Proficiencies.Add(Proficiency.intermediate, "Междинно");

            Specialities = new Dictionary<Specialty, string>();
            Specialities.Add(Specialty.Applied_Mathematics, "Приложна математика");
            Specialities.Add(Specialty.Computer_Science, "Компютърни науки");
            Specialities.Add(Specialty.Informatics, "Информатика");
            Specialities.Add(Specialty.Information_Systems, "Информационни системи");
            Specialities.Add(Specialty.Mathematics, "Математика");
            Specialities.Add(Specialty.Mathematics_and_Informatics, "Математика и информатика");
            Specialities.Add(Specialty.Software_Engineering, "Софтуерно инженерство");
            Specialities.Add(Specialty.Statistics, "Статистика");

            WorkStatuses = new Dictionary<WorkStatus, string>();
            WorkStatuses.Add(WorkStatus.looking_for_a_job, "Търси си работа");
            WorkStatuses.Add(WorkStatus.looking_for_an_internship, "Търси си стаж");
            WorkStatuses.Add(WorkStatus.not_looking, "Не си търси работа/стаж");
            WorkStatuses.Add(WorkStatus.not_specified, "Не е посочено");


            WorkTimes = new Dictionary<WorkTime, string>();
            WorkTimes.Add(WorkTime.eight_hours, "8 часов работен ден");
            WorkTimes.Add(WorkTime.six_hours, "6 часов работен ден");
            WorkTimes.Add(WorkTime.four_hours, "4 часов работен ден");
            WorkTimes.Add(WorkTime.from_home, "От дома");
            WorkTimes.Add(WorkTime.not_specified, "Не е посочено");
        }
    }
}