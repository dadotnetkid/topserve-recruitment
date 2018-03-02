using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ReserveRecruitmentProcessController : Controller
    {
        
        public ActionResult Dashboard(string mrfid)
        {
            ViewBag.mrfid = mrfid;
            return View();
        }
     
    }
}