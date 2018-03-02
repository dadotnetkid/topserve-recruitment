using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using ClosedXML.Excel;

namespace RecruitmentSystem.Models
{
    public class AgingReportViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? YearDate { get; set; }
        public int? YearlyDate { get; set; }
        public string MonthDate { get; set; }
        public string Recruiter { get; set; }
        public string Branch { get; set; }
        public string SkillType { get; set; }
        public string Status { get; set; }
        public string SearchBy { get; set; }
        public string AccountManager { get; set; }
        public string Coordinator { get; set; }
        public string Industry { get; set; }
        public string Client { get; set; }
        public List<sp_aging_reportResult> AgingReport()
        {
            try
            {
                if (SearchBy == "Yearly")
                {
                    DateFrom = new DateTime((int)YearlyDate, 1, 1);
                    DateTo = Convert.ToDateTime(DateFrom).AddMonths(12).AddSeconds(-1);
                }
                else if (SearchBy == "Monthly")
                {
                    DateFrom = new DateTime((int)YearDate, Convert.ToInt32(MonthDate), 1);
                    DateTo = Convert.ToDateTime(DateFrom).AddMonths(1).AddSeconds(-1);
                }
            }
            catch (Exception)
            {
            }
            return db.sp_aging_report(DateFrom, DateTo, SkillType, Branch, AccountManager, Coordinator, Recruiter, Industry, Client).ToList();
        }
        DataTable AgingReportDataTable()
        {
            DataTable dt = new DataTable("Aging Report");
            var columns = new string[] { "MRFID", "REQUESTED", "APPROVED", "POSITION", "REQUIRED APPLICANT", "CLASSIFICATION", "BATTING RATIO", "TAT", "DATE HIRED", "AGING", "STATUS" };
            foreach (var i in columns)
            {
                dt.Columns.Add(i);
            }
            return dt;
        }
        public MemoryStream DownloadAgingReport()
        {
            XLWorkbook wb = new XLWorkbook();
            var dt = AgingReportDataTable();
            foreach (var i in AgingReport())
            {
                dt.Rows.Add(i.mrfid, i.DateRequested, i.am_date_approved, i.position_name, i.RequiredNumber - i.cancel_number_requirement, i.Classification, Convert.ToDecimal(i.batting_ratio).ToString("N2"), i.tat, i.date_hired, i.aging_days, i.status);
            }
            var ws = wb.Worksheets.Add(dt);
            MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms;
        }
    }
}