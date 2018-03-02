using RecruitmentSystem.Models;
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
    public class ManageCompanyController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ActionResult dashboard(ErrorHandlers ErrorHandlers,string company = "")
        {
            ViewBag.SettingActive = "active";
            ViewBag.company = company;
            CompanyViewModel model = new CompanyViewModel() { ErrorHandler=ErrorHandlers };
            return View(model);
        }
        public ActionResult _dashboard( string search = "", int page = 1)
        {
            CompanyViewModel model = new CompanyViewModel() { Search = search};
            return View(model);
        }
        [HttpGet]
        public ActionResult createcompany()
        {
            ViewBag.SettingActive = "active";
            CompanyViewModel model = new CompanyViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult createcompany(CompanyViewModel model)
        {
            db.sp_add_companies(Tools.ToTitleCase(model.company_name), Tools.ToTitleCase(model.branch), Tools.ToTitleCase(model.brand), Tools.ToTitleCase(model.officeaddress), model.industry);

            return RedirectToAction("dashboard", "managecompany", new { company = model.company_name });
        }
        public ActionResult EditCompany(string CompanyId)
        {
            CompanyViewModel company = new CompanyViewModel() { CompanyId = CompanyId };
            var i = company.CompanyDetail();
            company = new CompanyViewModel() { CompanyId = CompanyId, company_name = i.company_name, branch = i.branch, brand = i.brand, industry = i.industry, officeaddress = i.officeaddress };
            return View(company);
        }
        [HttpPost]
        public ActionResult EditCompany(CompanyViewModel i)
        {
            i.UpdateCompany();
            return RedirectToAction("dashboard");
        }
        [HttpPost]
        public ActionResult importCompany(HttpPostedFileBase file)
        {
            var excel = ExcelConnection.Datasource("select * from sheet", file, Server.MapPath("~/excel/"));
            //foreach(DataRow dr in excel.Rows)
            //{
            //    db.sp_add_companies(Tools.ToTitleCase(dr[0].ToString()), Tools.ToTitleCase(dr[1].ToString()), Tools.ToTitleCase(dr[2].ToString()), Tools.ToTitleCase(dr[3].ToString()), Manpower.GetIndustryID(dr[4].ToString())); 
            //}
            CompanyViewModel model = new CompanyViewModel();
            var errorHandler = model.ImportCompany(excel);
            return RedirectToAction("dashboard", errorHandler);
        }
    }
}