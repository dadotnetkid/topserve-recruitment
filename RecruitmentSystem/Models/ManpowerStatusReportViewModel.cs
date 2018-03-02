using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class ManpowerStatusReportViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public string SearchBy { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? YearlyDate { get; set; }
        public int? Yearly { get; set; }
        public int? Monthly { get; set; }
        public string Classification { get; set; }
        private string _accountmanager = "";
        public string AccountManager { get { return _accountmanager; } set { _accountmanager = value; } }
        public string BusinessUnit { get; set; }
        public string Industry { get; set; }
        public string Client { get; set; }
        public string Branch { get; set; }
        public string OrderBy { get; set; }
        public List<sp_manpower_status_reportResult> ManpowerStatusReport()
        {
            try
            {
                if (Yearly != null && Monthly == null)
                {
                    DateFrom = new DateTime((int)Yearly, 1, 1);
                    DateTo = Convert.ToDateTime(DateFrom).AddMonths(12).AddDays(-1);
                }
                else if (YearlyDate != null && Monthly != null)
                {
                    DateFrom = new DateTime((int)YearlyDate, (int)Monthly, 1);
                    DateTo = Convert.ToDateTime(DateFrom).AddMonths(1).AddDays(-1);
                }
            }
            catch (Exception)
            {
            }
            var list = db.sp_manpower_status_report(DateFrom, DateTo, BusinessUnit, Industry, Client, Branch, Classification, this.AccountManager).ToList();
            if (OrderBy == "desc")
            {
                list = list.OrderByDescending(m => m.DateRequested).ToList();
            }
            return list;


        }
    }
}