using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace RecruitmentSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ActionResult Dashboard()
        {
            if(User.IsInRole("Recruiter"))
            {
                ViewBag.Ongoing = db.fn_count_ongoing_manpower_recruiter(User.Identity.GetUserId());
                ViewBag.Completed = db.fn_count_completed_manpower_recruiter(User.Identity.GetUserId());
            }
            else
            {
                ViewBag.New = db.fn_count_new_manpower();
                ViewBag.Ongoing = db.fn_count_ongoing_manpower();
                ViewBag.Completed = db.fn_count_completed_manpower();
            }
           
            ViewBag.HomeActive = "active";
            return View();
        }
    }
}