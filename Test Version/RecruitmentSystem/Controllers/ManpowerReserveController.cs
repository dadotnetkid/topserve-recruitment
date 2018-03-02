using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
namespace RecruitmentSystem.Controllers
{
    public class ManpowerReserveController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        [HttpGet]
        public ActionResult ReserveDashboard(string mrfid = "")
        {
            ViewBag.ManpowerReserveActive = "active";
            ViewBag.mrfid = mrfid;
            return View();
        }
        [HttpGet]
        public ActionResult _ReserveDashboard(string search = "", int page = 1)
        {
            var user = (Users.Role(User.Identity.GetUserId()) == "Recruiter") ? User.Identity.GetUserId() : "";
            var list = db.sp_manpower_reserve_list(user).ToList();
            list = (from m in list where m.position_name.ToLower().Contains(search.ToLower().ToString()) || m.educationalattainment.ToLower().Contains(search.ToLower().ToString()) || m.rafid.ToLower().Contains(search.ToLower().ToString()) select m).ToList();
            //ViewBag.pagesize = list.Count() / 10;
            //page = (page <= 0) ? 1 : page;
            //ViewBag.page = page;  
            //return View(list.Skip(10*(page-1)).Take(10));
            return View(list);
        }
        [HttpGet]
        public ActionResult CreateManpowerReserve()
        {
            ViewBag.ManpowerReserveActive = "active";
            return View();
        }
        [HttpPost]
        public ActionResult CreateManpowerReserve(ManpowerReserveViewModel i)
        {
            var user = User.Identity.GetUserId();
            var rafid = db.fn_generate_raf_id(user);
            db.sp_create_manpower_reserve(user, rafid, i.RequiredPosition, i.EducationalAttainment, i.course, i.gender, i.agerequirement, i.skilltype, i.specificskill, i.certification);
            return RedirectToAction("ReserveDashboard", "manpowerreserve", new { mrfid = rafid });
        }
        public ActionResult ManpowerReserveDetail(string mrfid)
        {
            ViewBag.ManpowerReserveActive = "active";
            var list = db.sp_manpower_reserve_detail(mrfid).FirstOrDefault();
            return View(list);
        }

    }
}