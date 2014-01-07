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
    public class JobController : BaseController
    {
        [HttpPost]
        public ActionResult AddJob(Job job)
        {
            if (ModelState.IsValid)
            {
                var student = studentManager.GetStudentByUserId(WebSecurity.CurrentUserId);

                db.Jobs.Add(job);
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Jobs.Add(job);

                db.SaveChanges();
            }

            return PartialView("../Student/_EditExperiencePartial", studentManager.GetStudentByUserId(WebSecurity.CurrentUserId));
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
        public ActionResult DeleteJob(int jobId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Jobs.RemoveAll(x => x.JobId == jobId);
            db.SaveChanges();

            return PartialView("../Student/_EditExperiencePartial", studentManager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }
    }
}
