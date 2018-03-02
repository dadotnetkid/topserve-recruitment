using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{

    public class TurnAroundTimeReportController : Controller
    {
        public ActionResult Index()
        {
            TurnAroundTimeViewModel i = new TurnAroundTimeViewModel();
            return View(i);
        }
        [HttpPost]
        public ActionResult Index(TurnAroundTimeViewModel i)
        {
            return View(i);
        }
        [HttpPost]
        public ActionResult Download(TurnAroundTimeViewModel i)
        {
            return File(i.DownloadTurnAroundTime().ToArray(), "application/pdf", "Turn Around Time.xlsx");
        }
    }
}