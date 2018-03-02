using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ApplicantSurveySummaryReportController : Controller
    {

        DatabaseModelDataContext db = new DatabaseModelDataContext();
        List<ApplicantSurveySummaryReport> surverylist = new List<ApplicantSurveySummaryReport>();
        [HttpGet]
        public ActionResult ApplicantSurveySummary()
        {
            ViewBag.ReportsActive = "active";
            return View(surverylist);
        }
        public ActionResult EducationalAttainmentReport(int month, int year)
        {
            var is_leap_year = DateTime.IsLeapYear(year);
            foreach (var i in db.sp_survey_educational_report(month.ToString("00"), year.ToString()))
            {
                surverylist.Add(new ApplicantSurveySummaryReport()
                {
                    SurveyType=i.educationattainment,
                    _01 = i._1,
                    _02 = i._2,
                    _03 = i._3,
                    _04 = i._4,
                    _05 = i._5,
                    _06 = i._6,
                    _07 = i._7,
                    _08 = i._8,
                    _09 = i._9,
                    _10 = i._10,
                    _11 = i._11,
                    _12 = i._12,
                    _13 = i._13,
                    _14 = i._14,
                    _15 = i._15,
                    _16 = i._16,
                    _17 = i._17,
                    _18 = i._18,
                    _19 = i._19,
                    _20 = i._20,
                    _21 = i._21,
                    _22 = i._22,
                    _23 = i._23,
                    _24 = i._24,
                    _25 = i._25,
                    _26 = i._26,
                    _27 = i._27,
                    _28 = i._28,
                    _29 = i._29,
                    _30 = i._30,
                    _31 = i._31
                });
            }
            ViewBag.month = month;
            ViewBag.year = year;
            return View(surverylist);
        }
        public ActionResult HowDidYouKnowTopserveReport(int month, int year)
        {
            ViewBag.month = month;
            ViewBag.year = year;
            var is_leap_year = DateTime.IsLeapYear(year);
            foreach (var i in db.sp_survey_how_did_you_know_topserve_report(month.ToString("00"), year.ToString()))
            {
                surverylist.Add(new ApplicantSurveySummaryReport()
                {
                    SurveyType=i.howdidyouknowtopserve,
                    _01 = i._1,
                    _02 = i._2,
                    _03 = i._3,
                    _04 = i._4,
                    _05 = i._5,
                    _06 = i._6,
                    _07 = i._7,
                    _08 = i._8,
                    _09 = i._9,
                    _10 = i._10,
                    _11 = i._11,
                    _12 = i._12,
                    _13 = i._13,
                    _14 = i._14,
                    _15 = i._15,
                    _16 = i._16,
                    _17 = i._17,
                    _18 = i._18,
                    _19 = i._19,
                    _20 = i._20,
                    _21 = i._21,
                    _22 = i._22,
                    _23 = i._23,
                    _24 = i._24,
                    _25 = i._25,
                    _26 = i._26,
                    _27 = i._27,
                    _28 = i._28,
                    _29 = i._29,
                    _30 = i._30,
                    _31 = i._31
                });
            }
            return View(surverylist);
        }
        public ActionResult InvitedByReport(int month, int year)
        {
            ViewBag.month = month;
            ViewBag.year = year;
            var is_leap_year = DateTime.IsLeapYear(year);
            foreach (var i in db.sp_survey_invited_by_report(month.ToString("00"), year.ToString()))
            {
                surverylist.Add(new ApplicantSurveySummaryReport()
                {
                    SurveyType=i.invited,
                    _01 = i._1,
                    _02 = i._2,
                    _03 = i._3,
                    _04 = i._4,
                    _05 = i._5,
                    _06 = i._6,
                    _07 = i._7,
                    _08 = i._8,
                    _09 = i._9,
                    _10 = i._10,
                    _11 = i._11,
                    _12 = i._12,
                    _13 = i._13,
                    _14 = i._14,
                    _15 = i._15,
                    _16 = i._16,
                    _17 = i._17,
                    _18 = i._18,
                    _19 = i._19,
                    _20 = i._20,
                    _21 = i._21,
                    _22 = i._22,
                    _23 = i._23,
                    _24 = i._24,
                    _25 = i._25,
                    _26 = i._26,
                    _27 = i._27,
                    _28 = i._28,
                    _29 = i._29,
                    _30 = i._30,
                    _31 = i._31
                });
            }
            return View(surverylist);
        }
        public ActionResult JobFairReport(int month, int year)
        {
            ViewBag.month = month;
            ViewBag.year = year;
            var is_leap_year = DateTime.IsLeapYear(year);
            foreach (var i in db.sp_survey_job_fair_report(month.ToString("00"), year.ToString()))
            {
                surverylist.Add(new ApplicantSurveySummaryReport()
                {
                    SurveyType=i.jobfair,
                    _01 = i._1,
                    _02 = i._2,
                    _03 = i._3,
                    _04 = i._4,
                    _05 = i._5,
                    _06 = i._6,
                    _07 = i._7,
                    _08 = i._8,
                    _09 = i._9,
                    _10 = i._10,
                    _11 = i._11,
                    _12 = i._12,
                    _13 = i._13,
                    _14 = i._14,
                    _15 = i._15,
                    _16 = i._16,
                    _17 = i._17,
                    _18 = i._18,
                    _19 = i._19,
                    _20 = i._20,
                    _21 = i._21,
                    _22 = i._22,
                    _23 = i._23,
                    _24 = i._24,
                    _25 = i._25,
                    _26 = i._26,
                    _27 = i._27,
                    _28 = i._28,
                    _29 = i._29,
                    _30 = i._30,
                    _31 = i._31
                });
            }
            return View(surverylist);
        }
        public ActionResult ExportApplicantSurveySummary(string month,string year)
        {
            ExportReport export=new ExportReport();
            return File(export.ExportApplicantSurveyReport(year, month).ToArray(), "application/vnd.ms-excel", "Applicant Survey Summary Report.xlsx");
        }
    }
}