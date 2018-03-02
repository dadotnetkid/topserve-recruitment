using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ManpowerSummaryReportController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        [HttpGet]
        public ActionResult ManpowerSummary()
        {
            ManpowerStatusSummaryViewModel i = new ManpowerStatusSummaryViewModel();
            return View(i);
        }
        [HttpPost]
        public ActionResult ManpowerSummary(ManpowerStatusSummaryViewModel i)
        {
            return View(i);
        }
      /*  public ActionResult ManpowerStatusSummary(string accountmanager, DateTime? datefrom, DateTime? dateto)
        {
            var list = db.sp_manpower_status_summary(accountmanager, datefrom, dateto);
            return View(list);
        }

        public ActionResult ManpowerStatusSummaryNewPerTotalRequirement(string accountmanager, DateTime? datefrom, DateTime? dateto)
        {
            var list = db.sp_manpower_status_summary_new_per_total_requirement(accountmanager, datefrom, dateto);
            return View(list);
        }
        public ActionResult ManpowerStatusSummaryClosedPerTotalRequirement(string accountmanager, DateTime? datefrom, DateTime? dateto)
        {
            var list = db.sp_manpower_status_summary_closed_per_total_requirement(accountmanager, datefrom, dateto);
            return View(list);
        }
        public ActionResult ManpowerStatusSummaryNewReplacementCancelledPerTotalRequirement(string accountmanager, DateTime? datefrom, DateTime? dateto)
        {
            var list = db.sp_manpower_status_summary_newreplacementcancelled_per_total_requirement(accountmanager, datefrom, dateto);
            return View(list);
        }
        public ActionResult ManpowerStatusSummaryOncallPerTotalRequirement(string accountmanager, DateTime? datefrom, DateTime? dateto)
        {
            var list = db.sp_manpower_status_summary_Oncall_per_total_requirement(accountmanager, datefrom, dateto);
            return View(list);
        }*/
        public ActionResult ManpowerStatusSummaryExport(string accountmanager, DateTime? datefrom, DateTime? dateto)
        {
            ExportReport export = new ExportReport();
            var res = export.ExportManpowerSummaryReport(accountmanager, datefrom, dateto);
            return File(res.ToArray(), "application/vnd.ms-excel", "Manpower Status Summary Report.xlsx");
        }
    }
}