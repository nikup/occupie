using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkedFMI_UI.Models;
using LinkedFMI_UI.Managers;
using WebMatrix.WebData;
using System.IO;
using System.Web.Helpers;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using LinkedFMI_UI.ViewModels;

namespace LinkedFMI_UI.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private StudentManager manager = new StudentManager();
        private LinkedFMIDb db = new LinkedFMIDb();

        //
        // GET: /Student/Profile

        public ActionResult Profile()
        {
            var student = manager.GetStudentByUserId(WebSecurity.CurrentUserId);
            return View(student);
        }

        //
        // GET: /Student/All

        public ActionResult All()
        {
            var students = db.Students.Select(s => new StudentAllViewModel
                {
                    Id = s.StudentProfileId,
                    Email = s.Email,
                    FullName = s.FirstName + " " + s.LastName,
                    HasJob = s.Jobs.Any(j => j.IsCurrent),
                    Picture = s.Picture
                });

            return View(students);
        }

        //
        // GET: /Student/Edit?profileId=2

        public ActionResult Edit(int profileId)
        {
            // TO FIX: link contains id if student, find a better way of handling it
            int userId = WebSecurity.CurrentUserId;
            if (manager.GetProfileIdByUserId(userId) == profileId)
            {
                Student student = db.Students.FirstOrDefault(p => p.StudentProfileId == profileId);
                return View(student);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Student/Edit
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                manager.SaveStudent(student);

                return RedirectToAction("Profile");
            }
            else
            {
                return View(student);
            }
        }

        [HttpPost]
        public void AddJob(Job job)
        {
            if (ModelState.IsValid)
            {
                var student = manager.GetStudentByUserId(WebSecurity.CurrentUserId);

                db.Jobs.Add(job);
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Jobs.Add(job);

                db.SaveChanges();
            }
        }

        [HttpGet]
        public PartialViewResult AddJob()
        {
            return PartialView("_AddJobPartial", new Job());
        }

        [HttpPost]
        public void DeleteJob(int jobId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Jobs.RemoveAll(x => x.JobId == jobId);
            db.SaveChanges();
        }

        [HttpPost]
        public void AddProject(Project project)
        {
            if (ModelState.IsValid)
            {
                var student = manager.GetStudentByUserId(WebSecurity.CurrentUserId);

                db.Projects.Add(project);
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Projects.Add(project);

                db.SaveChanges();
            }
        }

        [HttpGet]
        public PartialViewResult AddProject()
        {
            return PartialView("_AddProjectPartial", new Project());
        }

        [HttpPost]
        public void DeleteProject(int projectId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Projects.RemoveAt(projectId);
            db.SaveChanges();
        }

        [HttpPost]
        public int AddLanguage(Language lang)
        {
            if (ModelState.IsValid)
            {
                var student = manager.GetStudentByUserId(WebSecurity.CurrentUserId);

                db.Languages.Add(lang);
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Languages.Add(lang);

                db.SaveChanges();
                return lang.LangId;
            }
            return -1;
        }

        [HttpGet]
        public PartialViewResult AddLanguage()
        {
            return PartialView("_AddLangPartial", new Language());
        }

        [HttpGet]
        public PartialViewResult LanguagesEdit()
        {
            StudentManager manager = new StudentManager();
            return PartialView("_LanguagePartial", manager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }

        [HttpPost]
        public void DeleteLanguage(int langId, int listId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Languages.RemoveAll(x => x.LangId == langId);
            db.SaveChanges();
        }

        [HttpPost]
        public void AddTechnology(Technology tech)
        {
            if (ModelState.IsValid)
            {
                var student = manager.GetStudentByUserId(WebSecurity.CurrentUserId);

                db.Technologies.Add(tech);
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Technologies.Add(tech);

                db.SaveChanges();
            }
        }

        [HttpGet]
        public PartialViewResult AddTechnology()
        {
            return PartialView("_AddTechPartial", new Technology());
        }

        [HttpPost]
        public void DeleteTechnology(int techId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Technologies.RemoveAt(techId);
            db.SaveChanges();
        }

        [HttpGet]
        public PartialViewResult JobsEdit()
        {
            StudentManager manager = new StudentManager();
            return PartialView("_JobPartial", manager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }

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

                    db.Students.FirstOrDefault(x=>x.UserId == WebSecurity.CurrentUserId).Picture = img.GetBytes();
                    db.SaveChanges();
            }
            return RedirectToAction("Profile");
        }
    }
}
