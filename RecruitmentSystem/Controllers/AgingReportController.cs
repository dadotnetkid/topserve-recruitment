using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class AgingReportController : Controller
    {
        // GET: AgingReport
        public ActionResult Index()
        {
            AgingReportViewModel i = new AgingReportViewModel();
            return View(i);
        }
        [HttpPost]
        public ActionResult Index(AgingReportViewModel i )
        {
            return View(i);
        }
        [HttpPost]
        public ActionResult Download(AgingReportViewModel i)
        {
            return File(i.DownloadAgingReport().ToArray(),"application/pdf","Aging Report.xlsx");
        }
    }
}