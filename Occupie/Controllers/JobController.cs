using Occupie.Managers;
using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Occupie.Controllers
{
    public class JobController : Controller
    {
        private StudentManager manager = new StudentManager();
        private OccupieDb db = new OccupieDb();

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

        [HttpGet]
        public PartialViewResult DeleteJob(int jobId, int listId)
        {
            return PartialView("_DelJobPartial", new Int32[] { jobId, listId });
        }

        [HttpPost]
        public void DeleteJob(int jobId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Jobs.RemoveAll(x => x.JobId == jobId);
            db.SaveChanges();
        }

        [HttpGet]
        public PartialViewResult RefreshJobs()
        {
            return PartialView("../Job/_JobPartial", manager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }
    }
}
