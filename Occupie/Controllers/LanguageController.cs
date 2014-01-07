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
    public class LanguageController : BaseController
    {
        [HttpPost]
        public ActionResult AddLanguage(Language lang)
        {
            if (ModelState.IsValid)
            {
                var student = studentManager.GetStudentByUserId(WebSecurity.CurrentUserId);

                db.Languages.Add(lang);
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Languages.Add(lang);

                db.SaveChanges();
            }

            return PartialView("../Student/_EditPersonalPartial", studentManager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }

        [HttpGet]
        public PartialViewResult AddLanguage()
        {
            return PartialView("_AddLangPartial", new Language());
        }

        [HttpGet]
        public PartialViewResult RefreshLanguages()
        {            
            return PartialView("../Student/_EditPersonalPartial", studentManager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }

        [HttpGet]
        public PartialViewResult DeleteLanguage(int langId, int listId)
        {
            return PartialView("_DelLangPartial", new Int32[] { langId, listId });
        }

        [HttpPost]
        public ActionResult DeleteLanguage(int langId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Languages.RemoveAll(x => x.LangId == langId);
            db.SaveChanges();

            return PartialView("../Student/_EditPersonalPartial", studentManager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }
    }
}
