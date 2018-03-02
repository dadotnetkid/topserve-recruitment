using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Year()
        {
            return View();
        }
        public ActionResult Index()
        {
            CalendarViewModel i = new CalendarViewModel() { Year = DateTime.Now.Year };
            return View(i);
        }
        [HttpPost]
        public ActionResult Index(CalendarViewModel i)
        {
            return View(i);
        }
        public ActionResult CreateHoliday()
        {
            CalendarViewModel i = new CalendarViewModel();
            return View(i);
        }
        public ActionResult EditHoliday(CalendarViewModel i)
        {
            i = i.HolidayDetail();
            return View(i);
        }
        public ActionResult addeditdeleteHoliday(CalendarViewModel i)
        {
            i.AddEditDeleteHoliday();
            return RedirectToAction("index");
        }
    }
}