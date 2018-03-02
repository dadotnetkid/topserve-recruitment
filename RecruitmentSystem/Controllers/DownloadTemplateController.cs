using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class DownloadTemplateController : Controller
    {
        // GET: DownloadTemplate
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Download(string title)
        {
            string file = "";
            switch (title)
            {
                case "Import Applicant Template":
                    file = "rs-import-applicant-template";
                    break;
                case "Import MRF":
                    file = "rs-import-mrf";
                    break;
                case "Import Pass-on":
                    file = "rs-import-pass-on";
                    break;
                case "Import Client":
                    file = "rs-import-client";
                    break;
                case "Import Skills":
                    file = "rs-import-skills";
                    break;
                case "Import Certificates":
                    file = "rs-import-certificates";
                    break;
                case "Import Positions":
                    file = "rs-import-positions";
                    break;
                default:
                    break;
            }
            return File(Server.MapPath("~/excel template/" + file + ".xlsx"), "application/vnd.ms-excel", title + ".xlsx");
        }
    }
}