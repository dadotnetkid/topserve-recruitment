using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ManpowerStatusReportController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ActionResult ManpowerStatus()
        {
            ViewBag.ReportsActive = "active";
            return View();
        }
        public ActionResult _ManpowerStatus(ManpowerStatusReportViewModel i,string orderby="asc")
        {
            i.OrderBy = orderby;
            return View(i);
        }
    }
}