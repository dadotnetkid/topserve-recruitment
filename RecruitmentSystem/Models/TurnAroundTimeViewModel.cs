using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecruitmentSystem.Recruitment.Data;
using System.Data;
using System.IO;
using ClosedXML.Excel;
namespace RecruitmentSystem.Models
{
    public class TurnAroundTimeViewModel
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

        public List<sp_turn_around_time_reportResult> TurnAroundTime()
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
                    DateFrom = new DateTime(Convert.ToInt32(YearDate), Convert.ToInt32(MonthDate), 1);
                    DateTo = Convert.ToDateTime(DateFrom).AddMonths(1).AddSeconds(-1);
                }
            }
            catch (Exception)
            {
            }

            return db.sp_turn_around_time_report(DateFrom, DateTo, YearDate, MonthDate, Recruiter, Branch, SkillType, Status).ToList();
        }
        DataTable TurnAroundTimeDataTable()
        {
            DataTable dt = new DataTable("Aging Report");
            var columns = new string[] { "MRFID", "REQUESTED", "APPROVED","CLIENT NAME", "POSITION", "REQUIRED APPLICANT", "CLASSIFICATION", "TAT", "STATUS" };
            foreach (var i in columns)
            {
                dt.Columns.Add(i);

            }
            return dt;
        }
        public MemoryStream DownloadTurnAroundTime()
        {
            XLWorkbook wb = new XLWorkbook();
            var dt = TurnAroundTimeDataTable();
            foreach (var i in TurnAroundTime())
            {
                dt.Rows.Add(i.mrfid, i.DateRequested, i.am_date_approved,i.company_name, i.position_name, i.RequiredNumber - i.cancel_number_requirement, i.Classification, i.TAT, i.status);
            }
            var ws = wb.Worksheets.Add(dt);
            MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms;
        }
    }
}