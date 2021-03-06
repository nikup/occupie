using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Occupie.Models;
using Occupie.Managers;
using WebMatrix.WebData;
using System.IO;
using System.Web.Helpers;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Occupie.ViewModels;
using System.Diagnostics;
using SusiParser;
using System.Web.Security;

namespace Occupie.Controllers
{
    //[Authorize]
    public class StudentController : BaseController
    {
        //
        // GET: /Student/Profile

        public ActionResult Profile(int userId)
        {
            var student = studentManager.GetStudentByUserId(userId);
            return View(student);
        }

        //
        // GET: /Student/All

        public ActionResult All()
        {
            //var students = db.Students.Select(s => new StudentAllViewModel
            //    {
            //        Id = s.UserId,
            //        Email = s.Email,
            //        FullName = s.FirstName + " " + s.LastName,
            //        HasJob = s.Jobs.Any(j => j.IsCurrent) ? "Да" : "Не",
            //        Picture = s.Picture
            //    });

            var students = studentManager.GetStudents();

            ViewBag.RelevanceSorted = false;
            return View(students);
        }

        //
        // GET: /Student/Edit
        [Authorize(Roles = "student")]
        public ActionResult Edit()
        {
            int userId = WebSecurity.CurrentUserId;
            int profileId = studentManager.GetProfileIdByUserId(userId);
            Student student = db.Students.FirstOrDefault(p => p.StudentProfileId == profileId);
            return View(student);
        }

        //
        // POST: /Student/Edit
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                studentManager.SaveStudent(student);

                return RedirectToAction("Profile", new { userId = student.UserId });
            }
            else
            {
                return View(student);
            }
        }

        //[HttpGet]
        //public PartialViewResult RefreshExperience()
        //{
        //    StudentManager manager = new StudentManager();
        //    return PartialView("_EditExperiencePartial", manager.GetStudentByUserId(WebSecurity.CurrentUserId));
        //}

        [HttpGet]
        public ActionResult UploadImage()
        {
            return PartialView("_UploadPartial");
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentType.StartsWith("image/"))
            {
                WebImage img = new WebImage(file.InputStream);
                if (img.Width > 200)
                {
                    img.Resize(200, img.Height);
                }

                if (img.Height > 220)
                {
                    img.Resize(img.Width, 220);
                }
                
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Picture = img.GetBytes();
                db.SaveChanges();
            }
            return RedirectToAction("Profile", new { profileId = WebSecurity.CurrentUserId });
        }

        [HttpGet]
        public PartialViewResult RefreshSusi()
        {
            return PartialView("../Student/_EditEducationPartial", studentManager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }

        [HttpGet]
        public PartialViewResult UpdateSusi()
        {
            return PartialView("_UpdateSusiPartial", new LoginModel() { UserName = WebSecurity.CurrentUserName, Password = String.Empty });
        }

        [HttpPost]
        public void UpdateSusi(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.UserName, model.Password))
                {
                    try
                    {
                        // get SUSI info
                        SusiParser.SusiParser parser = new SusiParser.SusiParser();
                        parser.Login(model.UserName, model.Password);
                        StudentInfo info = parser.GetStudentInfo();
                        IEnumerable<CourseInfo> courses = parser.GetCourses();

                        // Fill info
                        int userId = int.Parse(Membership.GetUser(model.UserName).ProviderUserKey.ToString());
                        studentManager.FillInSusiInfo(studentManager.GetStudentByUserId(userId), info, courses);
                    }
                    catch (MembershipCreateUserException e)
                    {
                        ModelState.AddModelError("", e.Message);
                        Debug.WriteLine(e.Message);
                    }
                    catch (System.Net.WebException e)
                    {
                        ModelState.AddModelError("", e.Message);
                        Debug.WriteLine(e.Message);
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                }
            }
        }

        private const int PointsPerProject = 3;
        private const int GradeMultiplier = 3;
        private const int StudentsLimit = 50;

        [HttpPost]
        public ActionResult FilterStudents(FormCollection form)
        {
            int minYear = 0;
            int.TryParse(form["Year"], out minYear);
            char[] delimiter = new char[] { ',' };
            string[] technologies = form["Technologies"].Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            string[] languages = form["Languages"].Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> courses = form.AllKeys.Where(x => x != "Year" && x != "Technologies" && x != "Languages");

            // Formula is as follows:
            // points = sum(tech * proficiency) + sum(language * proficiency) + GradeMultiplier * average(grades) + PointsPerProject * projectCount
            Func<Student, double> calculateRelevance = (student) =>
                {
                    if (student.FMIInfo.Bachelor.CurrentCourse < minYear)
                        return 0;

                    double points = 0;
                    points += student.Technologies.Sum(x => Convert.ToInt32(technologies.Contains(x.Name)) * (int)x.Proficiency);
                    points += student.Projects.Count * PointsPerProject;
                    points += student.Languages.Sum(x => Convert.ToInt32(languages.Contains(x.Name)) * (int)x.Proficiency);

                    double courseCount = courses.Count();
                    if (courseCount != 0)
                        courseCount = 1;

                    points +=
                        GradeMultiplier *
                        student.FMIInfo.Bachelor.Subjects.Sum(x => Convert.ToInt32(courses.Contains(x.Name)) * (int)(x.Grade)) /
                        courseCount;

                    return points;
                };

            double maxPoints =
                (technologies.Length + languages.Length) * ((int)Occupie.Enums.Proficiency.Excellent - 1) +
                GradeMultiplier * 6 +
                PointsPerProject * 2;


            // TODO: TEST IF IT WORKZ!
            //IQueryable<StudentAllViewModel> query = db.Students.Select(s => new StudentAllViewModel
            //	{
            //		Id = s.StudentProfileId,
            //		Email = s.Email,
            //		FullName = s.FirstName + " " + s.LastName,
            //		HasJob = s.Jobs.Any(j => j.IsCurrent),
            //		Picture = s.Picture,
            //		Relevance = calculateRelevance(s) / maxPoints
            //	})
            //	.OrderByDescending(s => s.Relevance)
            //	.AsQueryable();

            // TODO: With a big table this can hurt a lot
            IEnumerable<Student> query = db.Students.ToList();

            IQueryable<StudentAllViewModel> sortedQuery = query.
                OrderByDescending(student => calculateRelevance(student))
                .Select(s => new StudentAllViewModel
                    {
                        Id = s.UserId,
                        FullName = s.FirstName + " " + s.LastName,
                        PictureString = "data:image;base64," + System.Convert.ToBase64String(s.Picture),
                        Relevance = (calculateRelevance(s) / maxPoints).ToString("P")
                    })
                    .AsQueryable();

            ViewBag.RelevanceSorted = true;
            return View("All", sortedQuery);
        }
    }
}
