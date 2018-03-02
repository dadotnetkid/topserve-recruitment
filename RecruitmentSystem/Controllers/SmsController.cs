using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class SmsController : Controller
    {
        // GET: Sms
        [HttpGet]
        public ActionResult ManageSms()
        {
            ViewBag.SettingActive = "active";
            var db = new DatabaseModelDataContext();
            var list = db.sp_sms_detail().FirstOrDefault();
            return View(list);
        }
        [HttpPost ]
        public ActionResult ManageSms(sp_sms_detailResult model)
        {
            var db = new DatabaseModelDataContext();
            db.sp_update_sms_setting(model.invited, model.shortlist, model.accepted, model.forrequirement);
            return View(model);
        }
    }
}