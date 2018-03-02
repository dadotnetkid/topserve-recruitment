using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ExportReportController : Controller
    {
        [HttpPost]
        public ActionResult ExportManpowerStatusReport(ManpowerStatusReportViewModel i)
        {
            ExportReport export = new ExportReport();
            var sr = export.ExportManpowerReport(i);
            return File(sr.ToArray(), "application/vnd.ms-excel", "Manpower Status Report " + Convert.ToDateTime(i.DateFrom).ToString("MM-dd-yy") + "-" + Convert.ToDateTime(i.DateTo).ToString("MM-dd-yy") + ".xls");
        }
    }
}