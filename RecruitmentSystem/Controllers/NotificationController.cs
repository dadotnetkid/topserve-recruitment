using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace RecruitmentSystem.Controllers
{
    public class NotificationController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ActionResult NotificationDashboard()
        {
            var list = db.sp_notification_list(User.Identity.GetUserId()).ToList();
            return View(list.OrderByDescending(m => m.id).Skip(0 * 10).Take(10));
        }
        public ActionResult NoficationNavBar()
        {
            var list = db.sp_notification_list(User.Identity.GetUserId()).ToList();
            return View(list.OrderByDescending(m => m.id).Skip(0 * 5).Take(5));
        }
        public ActionResult NotificationSummary()
        {
            var list = db.sp_notification_list(User.Identity.GetUserId()).ToList();
            return View(list.OrderByDescending(m=>m.id));
        }
    }
}