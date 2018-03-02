using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ImportUpdateManpowerController : Controller
    {
       
        public ActionResult Index()
        {
            ImportUpdateManpowerViewModel i = new ImportUpdateManpowerViewModel() ;
            return View(i);
        }
        [HttpPost]
        public ActionResult Index(ImportUpdateManpowerViewModel i)
        {
            i.importUpdateManpowerViewModel = i.importUpdateManpower();
            i.errorHandlers = new Recruitment.Class.ErrorHandlers() { _Class="alert alert-success",Message="Successfully updated"};
            return View(i);
        }
    }
}