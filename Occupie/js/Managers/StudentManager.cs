using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Occupie.Enums;
using System.IO;
using SusiParser;

namespace Occupie.Managers
{
    public class StudentManager
    {
        private LinkedFMIDb db = new LinkedFMIDb();  
        private UsersContext context = new UsersContext();

		private static Dictionary<string, Specialty> programmeMapping = new Dictionary<string,Specialty>()
		{
			{ "СИ(рб)", Specialty.Software_Engineering },
			{ "КН(рб)", Specialty.Computer_Science },
			// TODO: Add other programme's

		};

        public void AddStudent(int userId, StudentInfo studentInfo, IEnumerable<CourseInfo> courseInfo)
        {
            Student student = new Student();
            student.UserId = userId;

            string imageFile = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/default-avatar.jpg"));
            byte[] buffer = File.ReadAllBytes(imageFile);

            student.Picture = buffer;

            this.FillInSusiInfo(student, studentInfo, courseInfo);

            db.Students.Add(student);

            db.SaveChanges();
        }

        public void SaveStudent(Student student)
        {
            Student dbEntry = db.Students.Find(student.StudentProfileId);

            if (dbEntry != null)
            {
                // Main info
                if (student.Picture != null)
                {
                    dbEntry.Picture = student.Picture;
                }
                dbEntry.FirstName = student.FirstName;
                dbEntry.LastName = student.LastName;

                dbEntry.Email = student.Email;
                dbEntry.Blog = student.Blog;
                dbEntry.Phone = student.Phone;


                dbEntry.Facebook = student.Facebook;
                dbEntry.LinkedIn = student.LinkedIn;
                dbEntry.Twitter = student.Twitter;
                dbEntry.GooglePlus = student.GooglePlus;
                dbEntry.Github = student.Github;

                dbEntry.CV = student.CV;

                // Education
                if (student.HighSchoolInfo != null)
                {
                    dbEntry.HighSchoolInfo = student.HighSchoolInfo;
                }

                if (student.FMIInfo != null)
                {
                    for (int i = 0; i < student.FMIInfo.Bachelor.Subjects.Count; i++)
                    {
                        dbEntry.FMIInfo.Bachelor.Subjects[i].IsVisible = student.FMIInfo.Bachelor.Subjects[i].IsVisible;
                    }
                }

                // Experience
                if (student.Jobs != null && dbEntry.Jobs != null)
                {
                    for (int i = 0; i < Math.Min(dbEntry.Jobs.Count, student.Jobs.Count); i++)
                    {
                        dbEntry.Jobs[i] = student.Jobs[i];
                    }
                }

                if (student.Projects != null && dbEntry.Projects != null)
                {
                    for (int i = 0; i < Math.Min(dbEntry.Projects.Count, student.Projects.Count); i++)
                    {
                        dbEntry.Projects[i] = student.Projects[i];
                    }
                }

                if (student.Technologies != null && dbEntry.Technologies != null)
                {
                    for (int i = 0; i < Math.Min(dbEntry.Technologies.Count, student.Technologies.Count); i++)
                    {
                        dbEntry.Technologies[i] = student.Technologies[i];
                    }
                }
                

                dbEntry.WorkTimePreference = student.WorkTimePreference;
                dbEntry.WorkStatus = student.WorkStatus;

                // Personal
                if (student.Languages != null && dbEntry.Languages != null)
                {
                    for (int i = 0; i < Math.Min(dbEntry.Languages.Count, student.Languages.Count); i++)
                    {
                        dbEntry.Languages[i] = student.Languages[i];
                    }
                }

                dbEntry.InterestsString = student.InterestsString;
                dbEntry.DateOfBirth = student.DateOfBirth;
                dbEntry.Town = student.Town;

            }

            db.SaveChanges();
        }

        public Student GetStudentByUserId(int id)
        {
            Student student = db.Students.FirstOrDefault(x => x.UserId == id);

            return student;
        }

        public int GetProfileIdByUserId(int id)
        {
            Student student = db.Students.FirstOrDefault(x => x.UserId == id);

            return student.StudentProfileId;
        }



        public void FillInSusiInfo(Student student, StudentInfo studentInfo, IEnumerable<CourseInfo> courseInfo)
        {
            // TODO: edit when SUSI API is available

            student.FirstName = studentInfo.FirstName;
            student.LastName = studentInfo.LastName;
            student.Email = "didi.zlatkova93@gmail.com";

			Tuple<int, int> startEndYears = studentInfo.GetStartEndYear();
            Bachelor bachelor = new Bachelor
            {
                StartYear = startEndYears.Item1,
                EndYear = startEndYears.Item2,
                Specialty = StudentManager.programmeMapping[studentInfo.Programme],
                Subjects = courseInfo.Select(x => x.ToSubject()).ToList(),
            };

            FMIEdu fmiEdu = new FMIEdu
            {
                Bachelor = bachelor,
            };

            student.FMIInfo = fmiEdu;

            db.SaveChanges();
        }
    }
}