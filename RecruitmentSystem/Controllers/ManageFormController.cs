using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ManageFormController : Controller
    {
        // GET: ManageForm
        public ActionResult manageform(string status="")
        {
            ViewBag.SettingActive = "active";
            ViewBag.status = status;
            return View();
        }
        [HttpGet]
        public ActionResult skills()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = db.sp_skills_list("", true).ToList();
            return View(list);
        }
        [HttpPost]
        public JsonResult skills(string skill)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            db.sp_add_skill(Tools.ToTitleCase( skill));
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult certificates()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = db.sp_certificates_list("", true).ToList();
            return View(list);
        }
        [HttpPost]
        public JsonResult certificates(string certificate)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            db.sp_add_certificates(Tools.ToTitleCase(certificate));
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult positions()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = db.sp_positions_list("", true).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult positions(string position)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            db.sp_add_position(Tools.ToTitleCase(position));
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ImportSkills(HttpPostedFileBase file)
        {
            var excel = ExcelConnection.Datasource("select * from sheet  ", file, Server.MapPath("~/excel/"));
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            foreach (DataRow dr in excel.Rows)
            {
                if (dr[0].ToString() == "" || dr[0].ToString() == null)
                {
                    break;
                }
                db.sp_add_skill(dr[0].ToString());
            }
            return RedirectToAction("manageform", new { status = "Successfully Import Skills" });
        }
        [HttpPost]
        public ActionResult ImportPositions(HttpPostedFileBase file)
        {
            var excel = ExcelConnection.Datasource("select * from sheet", file, Server.MapPath("~/excel/"));
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            foreach (DataRow dr in excel.Rows)
            {
                if (dr[0].ToString() == "" || dr[0].ToString() == null)
                {
                    break;
                }
                db.sp_add_position(Tools.ToTitleCase(dr[0].ToString()));
            }
            return RedirectToAction("manageform", new { status = "Successfully Import Position" });
        }
        [HttpPost]
        public ActionResult ImportCertificates(HttpPostedFileBase file)
        {
            var excel = ExcelConnection.Datasource("select * from sheet", file, Server.MapPath("~/excel/"));
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            foreach (DataRow dr in excel.Rows)
            {
                if(dr[0].ToString()=="" || dr[0].ToString()==null)
                {
                    break;
                }
                db.sp_add_certificates(Tools.ToTitleCase(dr[0].ToString()));
            }
            return RedirectToAction("manageform", new { status="Successfully Import Certificate"});
        }
    }
}