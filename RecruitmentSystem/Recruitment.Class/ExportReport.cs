using ClosedXML.Excel;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using RecruitmentSystem.Recruitment.Data;
using RecruitmentSystem.Recruitment.Data.ExportingReportTableAdapters;
using RecruitmentSystem.Models;
namespace RecruitmentSystem.Recruitment.Class
{
    public class ExportReport
    {
        MemoryStream ms = new MemoryStream();
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        XLWorkbook wb = new XLWorkbook();
        #region Status Report
        public MemoryStream ExportManpowerReport(ManpowerStatusReportViewModel model)
        {
            var dt = ManpowerStatusReportTemplate();
            var list = model.ManpowerStatusReport();
            //if (Searchby == "Yearly")
            //{
            //    list = (from m in list where Convert.ToDateTime(m.DateRequested).Year == year && m.Classification.Contains(classification) && (m.am_1_id.Contains(accountmanger) || m.am_2_id.Contains(accountmanger)) select m).ToList();
            //}
            //else if (Searchby == "Monthly")
            //{
            //    list = (from m in list where Convert.ToDateTime(m.DateRequested).ToString("MM") == Monthly.ToString() && m.Classification.Contains(classification) && (m.am_1_id.Contains(accountmanger) || m.am_2_id.Contains(accountmanger)) select m).ToList();
            //}
            //else if (Searchby == "Daily")
            //{
            //    list = (from m in list where (m.DateRequested >= datefrom && m.DateRequested <= dateto) && m.Classification.Contains(classification) && (m.am_1_id.Contains(accountmanger) || m.am_2_id.Contains(accountmanger)) select m).ToList();
            //}

            foreach (var i in list)
            {
                dt.Rows.Add(i.mrfid, i.DateRequested, i.company_name, i.Establishment, i.industry_name, i.position_name, i.SkillType, i.account_manager_1, i.Coodinator_name,
                            i.Classification, i.RequiredNumber, i.agingdays, i.Recruiter_Name, i.cancelled_requirement, i.closed, i.Pending, i.Passon, i.Invited, i.Shortlist,
                            i.for_requirement, i.variance, i.status, i.Remarks);
            }
            wb.Worksheets.Add(dt);
            wb.SaveAs(ms);
            return ms;
        }
        DataTable ManpowerStatusReportTemplate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Manpower Status Report";
            dt.Columns.Add("MRFID");
            dt.Columns.Add("Date Requested");
            dt.Columns.Add("Company Name");
            dt.Columns.Add("Client Department/Store Location");
            dt.Columns.Add("Industry Name");
            dt.Columns.Add("Position Name");
            dt.Columns.Add("Skill Type");
            dt.Columns.Add("Account Manager");
            dt.Columns.Add("Coordinator");
            dt.Columns.Add("Classification");
            dt.Columns.Add("Required Number");
            dt.Columns.Add("Aging Days");
            dt.Columns.Add("Recruiter Name");
            dt.Columns.Add("Cancelled Requirement");
            dt.Columns.Add("Closed");
            dt.Columns.Add("Pending");
            dt.Columns.Add("Pass On");
            dt.Columns.Add("Invited");
            dt.Columns.Add("Shortlist");
            dt.Columns.Add("For Requirement");
            dt.Columns.Add("Variance");
            dt.Columns.Add("Status");
            dt.Columns.Add("Remarks");
            return dt;
        }
        #endregion
        #region Summary Report
        public MemoryStream ExportManpowerSummaryReport(string am, DateTime? Dtfrom, DateTime? Dtto)
        {
            am = am == null ? "" : am;
            var wb = new XLWorkbook();
            var ms = new MemoryStream();
            DataTable dt1 = this.dt1();
            DataTable dt2 = this.dt2();
            DataTable dt3 = this.dt3();
            DataTable dt4 = this.dt4();
            DataTable dt5 = this.dt5();
            foreach (var i in db.sp_manpower_status_summary(am, "", Dtfrom, Dtto))
            {
                dt1.Rows.Add(i.account_manager, i.New, i.Replacement, i.OnCall, i.TotalRequirement, i.CancelledRequirement, i.Closed, i.Pending, i.OnProcess);
            }

            foreach (var i in db.sp_manpower_status_summary_new_per_total_requirement(am, "", Dtfrom, Dtto))
            {
                dt2.Rows.Add(i.account_manager, i.New, Tools.ToDecimal(i.percent_new_total_requirement, "0") + "%", i.OnCall, Tools.ToDecimal(i.percent_oncall_total_requirement, "0") + "%", i.Replacement, Tools.ToDecimal(i.percent_replacement_total_requirement, "0") + "%", i.TotalRequirement, Tools.ToDecimal(i.percent_total_requirement, "0") + "%");
            }
            var list = db.sp_manpower_status_summary_new_per_total_requirement(am, "", Dtfrom, Dtto).ToList();


            foreach (var i in db.sp_manpower_status_summary_closed_per_total_requirement(am, "", Dtfrom, Dtto))
            {
                dt3.Rows.Add(i.account_manager, i.Closed, Tools.ToDecimal(i.percent_Closed_total_requirement, "0") + "%", i.Pending, Tools.ToDecimal(i.percent_Pending_total_requirement, "0") + "%", i.OnProcess,
                                        Tools.ToDecimal(i.percent_OnProcess_total_requirement, "0") + "%", Tools.ToDecimal(i.PlacementAndOnProcess, "0"), Tools.ToDecimal(i.percent_PlacementAndOnProcess_total_requirement, "0") + "%");
            }
            int? NewReplacement = 0;
            int? allclassificationpending = 0;
            int? allonprocess = 0;
            int? allplacementallprocess = 0;
            foreach (var i in db.sp_manpower_status_summary_newreplacementcancelled_per_total_requirement(am, "", Dtfrom, Dtto))
            {
                dt4.Rows.Add(i.account_manager, i.NewandReplacementCancelled, Tools.ToDecimal(i.percent_NewandReplacementCancelled_total_requirement, "0") + "%",
                    Tools.ToDecimal(i.NewClosed, "0"),
                    Tools.ToDecimal(i.percent_placement_onprocess_replacement, "0") + "%", i.OnProcess,
                    Tools.ToDecimal(i.percent_onprocess_new_replacement, "0") + "%",
                    i.placementandonprocess, Tools.ToDecimal(i.percent_newclosedd_total_requirement, "0") + "%");
                NewReplacement += i.NewandReplacementCancelled;
                allclassificationpending += i.NewClosed;
                allonprocess += i.OnProcess;
                allplacementallprocess += i.placementandonprocess;
            }
            //  dt4.Rows.Add("", NewReplacement, NewReplacement / totalrequirement, allclassificationpending, allclassificationpending / totalrequirement, onprocess, allonprocess / totalrequirement, allplacementallprocess, allplacementallprocess / totalrequirement);
            foreach (var i in db.sp_manpower_status_summary_Oncall_per_total_requirement(am, "", Dtfrom, Dtto))
            {
                dt5.Rows.Add(i.account_manager, i.OncallRequirement, Tools.ToDecimal(i.percent_oncall_total_requirement, "0") + "%",
                    i.OncallClosed, Tools.ToDecimal(i.percent_oncall_closed, "0") + "%", i.Variance, Tools.ToDecimal(i.percent_variance_oncall, "0") + "%");


            }
            var ws = wb.Worksheets.Add(dt1);
            ws.Cell(dt1.Rows.Count + 3, 1).InsertTable(dt2);
            var dt3rows = (dt1.Rows.Count + 3) + dt2.Rows.Count + 2;
            var dt4rows = dt3rows + (dt4.Rows.Count + 2);
            var dt5rows = dt4rows + (dt5.Rows.Count + 2);
            ws.Cell(dt3rows, 1).InsertTable(dt3);
            ws.Cell(dt4rows, 1).InsertTable(dt4);
            ws.Cell(dt5rows, 1).InsertTable(dt5);
            wb.SaveAs(ms);
            return ms;
        }
        DataTable dt1()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Sheet 1";
            dt.Columns.Add("Account Name");
            dt.Columns.Add("New");
            dt.Columns.Add("Replacement");
            dt.Columns.Add("Oncall");
            dt.Columns.Add("Total Requirement");
            dt.Columns.Add("Cancelled");
            dt.Columns.Add("Hired");
            dt.Columns.Add("Pending");
            dt.Columns.Add("OnProcess");
            return dt;
        }
        DataTable dt2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Account Manager");
            dt.Columns.Add("New");
            dt.Columns.Add("% N/TR");
            dt.Columns.Add("On Call");
            dt.Columns.Add("%OC/TR");
            dt.Columns.Add("Replacement");
            dt.Columns.Add("% R/TR");
            dt.Columns.Add("Total Requirement");
            dt.Columns.Add("% TR/TR");
            return dt;
        }
        DataTable dt3()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Account Manager");
            dt.Columns.Add("Hired");
            dt.Columns.Add("% H/TR");
            dt.Columns.Add("Pending");
            dt.Columns.Add("% P/TR");
            dt.Columns.Add("OnProcess");
            dt.Columns.Add("% OP/TR");
            dt.Columns.Add("Hired+OnProcess");
            dt.Columns.Add("% H+OP/TR");
            return dt;
        }
        DataTable dt4()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Account Manager");
            dt.Columns.Add("New + Replacement");
            dt.Columns.Add("% N/TR");
            dt.Columns.Add("Placement/Closed");
            dt.Columns.Add("% P/TR");
            dt.Columns.Add("On Process");
            dt.Columns.Add("% On-P/N");
            dt.Columns.Add("Placement + On-Process");
            dt.Columns.Add("% P+OnO/New");
            return dt;
        }
        DataTable dt5()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Account Manager");
            dt.Columns.Add("OnCall");
            dt.Columns.Add("% OC/TR");
            dt.Columns.Add("H/ired");
            dt.Columns.Add("% H/0C");
            dt.Columns.Add("Variance");
            dt.Columns.Add("% V/OC");
            return dt;
        }
        #endregion
        #region Applicant Survey
        public MemoryStream ExportApplicantSurveyReport(string year, string month)
        {
            wb = new XLWorkbook();
            var ms = new MemoryStream();
            month = Convert.ToInt32(month).ToString("00");
            var dt1 = new ExportingReport.sp_survey_educational_reportDataTable();
            var dt2 = new ExportingReport.sp_survey_job_fair_reportDataTable();
            var dt3 = new ExportingReport.sp_survey_invited_by_reportDataTable();
            var dt4 = new ExportingReport.sp_survey_how_did_you_know_topserve_reportDataTable();


            var da1 = new sp_survey_educational_reportTableAdapter();
            var da2 = new sp_survey_job_fair_reportTableAdapter();
            var da3 = new sp_survey_invited_by_reportTableAdapter();
            var da4 = new sp_survey_how_did_you_know_topserve_reportTableAdapter();
            var connectionstring = db.Connection.ConnectionString;
            da1.Connection.ConnectionString = connectionstring;
            da2.Connection.ConnectionString = connectionstring;
            da3.Connection.ConnectionString = connectionstring;
            da4.Connection.ConnectionString = connectionstring;
            da1.Fill(dt1, month, year);
            da2.Fill(dt2, month, year);
            da3.Fill(dt3, month, year);
            da4.Fill(dt4, month, year);
            var ds = new ExportingReport();

            int dt2rows = dt1.Rows.Count + 3;
            int dt3rows = dt2rows + (dt2.Rows.Count + 2);
            int dt4rows = dt3rows + (dt3.Rows.Count + 2);
            var ws = wb.AddWorksheet(dt1, "Survey Report");
            ws.Cell(dt2rows, 1).InsertTable((DataTable)dt2);
            ws.Cell(dt3rows, 1).InsertTable((DataTable)dt3);
            ws.Cell(dt4rows, 1).InsertTable((DataTable)dt4);
            wb.SaveAs(ms);
            return ms;

        }
        #endregion

    }
}