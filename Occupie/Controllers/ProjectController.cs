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
    public class ProjectController : BaseController
    {
        [HttpPost]
        public ActionResult AddProject(Project project)
        {
            if (ModelState.IsValid)
            {
                var student = studentManager.GetStudentByUserId(WebSecurity.CurrentUserId);

                db.Projects.Add(project);
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Projects.Add(project);

                db.SaveChanges();
            }

            return PartialView("../Student/_EditExperiencePartial", studentManager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }

        [HttpGet]
        public PartialViewResult AddProject()
        {
            return PartialView("_AddProjectPartial", new Project());
        }

        [HttpGet]
        public PartialViewResult DeleteProject(int projId, int listId)
        {
            return PartialView("_DelProjectPartial", new Int32[] { projId, listId });
        }

        [HttpPost]
        public ActionResult DeleteProject(int projId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Projects.RemoveAll(x => x.ProjectId == projId);
            db.SaveChanges();

            return PartialView("../Student/_EditExperiencePartial", studentManager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }
    }
}
