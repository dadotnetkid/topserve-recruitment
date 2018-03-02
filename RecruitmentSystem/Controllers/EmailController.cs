using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        [HttpGet]
        public ActionResult EmailSetting(string updated)
        {
            ViewBag.SettingActive = "active";
            var db = new DatabaseModelDataContext();
            var list = db.sp_email_setting_detail().FirstOrDefault();
            ViewBag.updated = updated;
            return View(list);
        }
        [HttpPost]
        public ActionResult EmailSetting(sp_email_setting_detailResult model)
        {
            var db = new DatabaseModelDataContext();
            db.sp_email_setting(model.email, model.password, model.host, model.port, model.ssl);
            return RedirectToAction("emailsetting", "email", new { updated = "Successfully" });
        }
        [HttpGet]
        public ActionResult EmailMessage(string updated)
        {
            ViewBag.SettingActive = "active";
            var db = new DatabaseModelDataContext();
            var list = db.sp_email_message_detail().FirstOrDefault();
            ViewBag.updated = updated;
            return View(list);
        }
        [HttpPost]
        public ActionResult EmailMessage(sp_email_message_detailResult i)
        {
            var db = new DatabaseModelDataContext();
            db.sp_email_message(i.invited, i.shortlist, i.forrequirement, i.accepted);
            return RedirectToAction("emailmessage", "email", new { @updated = "successfull" });

            
        }
    }
}