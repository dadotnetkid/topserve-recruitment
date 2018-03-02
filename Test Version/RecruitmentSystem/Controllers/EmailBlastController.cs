using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class EmailBlastController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.EmailBlast = "active";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(EmailBlastViewModel i)
        {
            await i.SendEmailBlast();
            return View(i);
        }
    }
}