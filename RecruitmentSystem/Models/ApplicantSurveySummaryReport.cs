using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecruitmentSystem.Recruitment.Data;
namespace RecruitmentSystem.Models
{
    public class ApplicantSurveySummaryReport
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public string SurveyType { get; set; }
        public int? @_01 { get; set; }
        public int? @_02 { get; set; }
        public int? @_03 { get; set; }
        public int? @_04 { get; set; }
        public int? @_05 { get; set; }
        public int? @_06 { get; set; }
        public int? @_07 { get; set; }
        public int? @_08 { get; set; }
        public int? @_09 { get; set; }
        public int? @_10 { get; set; }
        public int? @_11 { get; set; }
        public int? @_12 { get; set; }
        public int? @_13 { get; set; }
        public int? @_14 { get; set; }
        public int? @_15 { get; set; }
        public int? @_16 { get; set; }
        public int? @_17 { get; set; }
        public int? @_18 { get; set; }
        public int? @_19 { get; set; }
        public int? @_20 { get; set; }
        public int? @_21 { get; set; }
        public int? @_22 { get; set; }
        public int? @_23 { get; set; }
        public int? @_24 { get; set; }
        public int? @_25 { get; set; }
        public int? @_26 { get; set; }
        public int? @_27 { get; set; }
        public int? @_28 { get; set; }
        public int? @_29 { get; set; }
        public int? @_30 { get; set; }
        public int? @_31 { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public List<sp_survey_educational_reportResult> sp_survey_educational_report()
        {
            return db.sp_survey_educational_report(Month, Year).ToList();
        }
    }
}