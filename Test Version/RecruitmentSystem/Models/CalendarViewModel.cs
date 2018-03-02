using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class CalendarViewModel
    {
        public CalendarViewModel()
        {
            Year = DateTime.Now.Year;
        }
        public int Year { get; set; }
        public string HolidayId { get; set; }
        public DateTime? Holiday { get; set; }
        public string HolidayName { get; set; }
        public string method { get; set; }

        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public List<sp_holiday_listResult> HolidayList()
        {
            return db.sp_holiday_list(Year).ToList();
        }
        public CalendarViewModel HolidayDetail()
        {
            CalendarViewModel model = null;
            foreach (var i in db.sp_holiday_detail(HolidayId).ToList())
            {
                model = new CalendarViewModel() { HolidayId = i.HolidayId, HolidayName = i.HolidayName, Holiday = i.Holiday };
            }
            return model;
        }
        public void AddEditDeleteHoliday()
        {
            db.sp_add_edit_delete_holiday(method, HolidayId, HolidayName, Holiday, Users.GetUserid());
        }
    }
}