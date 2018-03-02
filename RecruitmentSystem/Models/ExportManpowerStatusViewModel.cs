using ClosedXML.Excel;
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class ExportManpowerStatusViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        class Summary : sp_manpower_status_summaryResult { }
        class TotalRequirement : sp_manpower_status_summary_new_per_total_requirementResult { }
        class ClosedPerTotalRequirement : sp_manpower_status_summary_closed_per_total_requirementResult { }
        class PlacementCancelledTotalRequirement : sp_manpower_status_summary_newreplacementcancelled_per_total_requirementResult { }
        class OncallPerTotalRequirement : sp_manpower_status_summary_Oncall_per_total_requirementResult { }
        public MemoryStream ExportManpowerSummaryReport(string am, string BranchId, DateTime? Dtfrom, DateTime? Dtto)
        {
            am = am == null ? "" : am;
            var wb = new XLWorkbook();
            var ms = new MemoryStream();
            DataTable dt1 = this.dt1();
            DataTable dt2 = this.dt2();
            DataTable dt3 = this.dt3();
            DataTable dt4 = this.dt4();
            DataTable dt5 = this.dt5();
            var summary = db.sp_manpower_status_summary(am, BranchId, Dtfrom, Dtto).ToList();
            var totalRequirement = db.sp_manpower_status_summary_new_per_total_requirement(am, BranchId, Dtfrom, Dtto).ToList();
            var closedPerTotalRequirement = db.sp_manpower_status_summary_closed_per_total_requirement(am, BranchId, Dtfrom, Dtto).ToList();
            var placementCancelledTotalRequirement = db.sp_manpower_status_summary_newreplacementcancelled_per_total_requirement(am, BranchId, Dtfrom, Dtto).ToList();
            var oncallPerTotalRequirement = db.sp_manpower_status_summary_Oncall_per_total_requirement(am, BranchId, Dtfrom, Dtto).ToList();
            foreach (var i in summary)
            {
                dt1.Rows.Add(i.account_manager, i.New, i.Replacement, i.OnCall, Tools.ToInteger(i.TotalRequirement), i.CancelledRequirement, i.Closed, i.Pending, Tools.ToInteger(i.OnProcess), Tools.ToInteger(i.Variance));
            }
            var s = new Summary()
            {
                CancelledRequirement = summary.Sum(m => m.CancelledRequirement).Value,
                Closed = summary.Sum(m => m.Closed).Value,
                New = summary.Sum(m => m.New).Value,
                OnCall = summary.Sum(m => m.OnCall).Value,
                OnProcess = summary.Sum(m => m.OnProcess).Value,
                Pending = summary.Sum(m => m.Pending).Value,
                Reliever = summary.Sum(m => m.Reliever).Value,
                Replacement = summary.Sum(m => m.Replacement).Value,
                TotalRequirement = summary.Sum(m => m.TotalRequirement).Value,
                Variance = Tools.ToInteger(summary.Sum(m => m.Variance).Value)
            };
            dt1.Rows.Add(s.account_manager, s.New, s.Replacement, s.OnCall, s.TotalRequirement, s.CancelledRequirement, s.Closed, s.Pending, s.OnProcess, s.Variance);
            foreach (var i in totalRequirement)
            {
                dt2.Rows.Add(i.account_manager, i.New, Tools.ToDecimal(i.percent_new_total_requirement, "0") + "%", i.OnCall, Tools.ToDecimal(i.percent_oncall_total_requirement, "0") + "%", i.Replacement, Tools.ToDecimal(i.percent_replacement_total_requirement, "0") + "%", i.TotalRequirement, Tools.ToDecimal(i.percent_total_requirement, "0") + "%");
            }
            var t = new TotalRequirement()
            {
                New = totalRequirement.Sum(m => m.New).Value,
                OnCall = totalRequirement.Sum(m => m.OnCall).Value,
                percent_new_total_requirement = Tools.ParseDivideToZero(totalRequirement.Sum(m => m.New).Value, s.TotalRequirement),
                percent_oncall_total_requirement = Tools.ParseDivideToZero(totalRequirement.Sum(m => m.OnCall).Value, s.TotalRequirement),
                percent_reliever_total_requirement = totalRequirement.Sum(m => m.percent_reliever_total_requirement).Value,
                percent_replacement_total_requirement = Tools.ParseDivideToZero(totalRequirement.Sum(m => m.Replacement).Value, s.TotalRequirement),
                percent_total_requirement = Tools.ParseDivideToZero(totalRequirement.Sum(m => m.TotalRequirement).Value, s.TotalRequirement),
                Reliever = totalRequirement.Sum(m => m.Reliever).Value,
                Replacement = totalRequirement.Sum(m => m.Replacement).Value,
                TotalRequirement = totalRequirement.Sum(m => m.TotalRequirement).Value,
            };
            dt2.Rows.Add(t.account_manager, t.New, Tools.ToDecimal(t.percent_new_total_requirement, "0") + "%", t.OnCall, Tools.ToDecimal(t.percent_oncall_total_requirement, "0") + "%", t.Replacement, Tools.ToDecimal(t.percent_replacement_total_requirement, "0") + "%", t.TotalRequirement, Tools.ToDecimal(t.percent_total_requirement, "0") + "%");

            foreach (var i in closedPerTotalRequirement)
            {
                dt3.Rows.Add(i.account_manager, i.Closed, Tools.ToDecimal(i.percent_Closed_total_requirement, "0") + "%", i.Pending, Tools.ToDecimal(i.percent_Pending_total_requirement, "0") + "%", i.OnProcess,
                                        Tools.ToDecimal(i.percent_OnProcess_total_requirement, "0") + "%", Tools.ToDecimal(i.PlacementAndOnProcess, "0"), Tools.ToDecimal(i.percent_PlacementAndOnProcess_total_requirement, "0") + "%");
            }
            var c = new ClosedPerTotalRequirement()
            {
                Closed = closedPerTotalRequirement.Sum(m => m.Closed).Value,
                OnProcess = closedPerTotalRequirement.Sum(m => m.OnProcess).Value,
                Pending = closedPerTotalRequirement.Sum(m => m.Pending).Value,

                PlacementAndOnProcess = closedPerTotalRequirement.Sum(m => m.PlacementAndOnProcess).Value,
            };
            c.percent_Closed_total_requirement = Tools.ParseDivideToZero(c.Closed, s.TotalRequirement);
            c.percent_Pending_total_requirement = Tools.ParseDivideToZero(c.Pending, s.TotalRequirement);
            c.percent_OnProcess_total_requirement = Tools.ParseDivideToZero(c.OnProcess, s.TotalRequirement);
            c.percent_PlacementAndOnProcess_total_requirement = Tools.ParseDivideToZero(c.PlacementAndOnProcess, s.TotalRequirement);

            dt3.Rows.Add(c.account_manager,
                c.Closed,
                Tools.ToDecimal(c.percent_Closed_total_requirement, "0") + "%",
                c.Pending,
                Tools.ToDecimal(c.percent_Pending_total_requirement, "0") + "%",
                c.OnProcess,
                Tools.ToDecimal(c.percent_OnProcess_total_requirement, "0") + "%",
                Tools.ToDecimal(c.PlacementAndOnProcess, "0"),
                Tools.ToDecimal(c.percent_PlacementAndOnProcess_total_requirement, "0") + "%");
            foreach (var i in placementCancelledTotalRequirement)
            {
                dt4.Rows.Add(i.account_manager, i.NewandReplacementCancelled, Tools.ToDecimal(i.percent_NewandReplacementCancelled_total_requirement, "0") + "%",
                    Tools.ToDecimal(i.NewClosed, "0"),
                    Tools.ToDecimal(i.percent_placement_onprocess_replacement, "0") + "%", i.OnProcess,
                    Tools.ToDecimal(i.percent_onprocess_new_replacement, "0") + "%",
                    i.placementandonprocess, Tools.ToDecimal(i.percent_newclosedd_total_requirement, "0") + "%");
            }
            //row 4
            var p = new PlacementCancelledTotalRequirement()
            {
                NewandReplacementCancelled = placementCancelledTotalRequirement.Sum(m => m.NewandReplacementCancelled).Value,
                NewClosed = placementCancelledTotalRequirement.Sum(m => m.NewClosed).Value,
                OnProcess = placementCancelledTotalRequirement.Sum(m => m.OnProcess).Value,
                placementandonprocess = placementCancelledTotalRequirement.Sum(m => m.placementandonprocess).Value,
            };
            p.percent_NewandReplacementCancelled_total_requirement = Tools.ParseDivideToZero(p.NewandReplacementCancelled, s.TotalRequirement);
            p.percent_placement_onprocess_replacement = Tools.ParseDivideToZero(p.NewClosed, p.NewandReplacementCancelled);
            p.percent_onprocess_new_replacement = Tools.ParseDivideToZero(p.OnProcess, p.NewandReplacementCancelled);
            p.percent_newclosedd_total_requirement = Tools.ParseDivideToZero(p.placementandonprocess, p.NewandReplacementCancelled);

            dt4.Rows.Add(p.account_manager,
                   p.NewandReplacementCancelled,
                   Tools.ToDecimal(p.percent_NewandReplacementCancelled_total_requirement, "0") + "%",
                   Tools.ToDecimal(p.NewClosed, "0"),
                   Tools.ToDecimal(p.percent_placement_onprocess_replacement, "0") + "%",
                   p.OnProcess,
                   Tools.ToDecimal(p.percent_onprocess_new_replacement, "0") + "%",
                   p.placementandonprocess,
                   Tools.ToDecimal(p.percent_newclosedd_total_requirement, "0") + "%");

            var o = new OncallPerTotalRequirement()
            {
                OncallClosed = oncallPerTotalRequirement.Sum(m => m.OncallClosed).Value,
                OncallRequirement = oncallPerTotalRequirement.Sum(m => m.OncallRequirement).Value,
                percent_oncall_closed = Tools.ParseDivideToZero(oncallPerTotalRequirement.Sum(m => m.OncallClosed).Value, oncallPerTotalRequirement.Sum(m => m.OncallRequirement).Value),
                percent_oncall_total_requirement = Tools.ParseDivideToZero(oncallPerTotalRequirement.Sum(m => m.OncallRequirement).Value, s.TotalRequirement),

                Variance = oncallPerTotalRequirement.Sum(m => m.Variance).Value,
                percent_variance_oncall = Tools.ParseDivideToZero(oncallPerTotalRequirement.Sum(m => m.Variance).Value, oncallPerTotalRequirement.Sum(m => m.OncallRequirement).Value),

            };
            foreach (var i in oncallPerTotalRequirement)
            {
                dt5.Rows.Add(i.account_manager, i.OncallRequirement, Tools.ToDecimal(i.percent_oncall_total_requirement, "0") + "%",
                    i.OncallClosed, Tools.ToDecimal(i.percent_oncall_closed, "0") + "%", i.Variance, Tools.ToDecimal(i.percent_variance_oncall, "0") + "%");
            }
            dt5.Rows.Add(o.account_manager, o.OncallRequirement, Tools.ToDecimal(o.percent_oncall_total_requirement, "0") + "%",
                   o.OncallClosed, Tools.ToDecimal(o.percent_oncall_closed, "0") + "%", o.Variance, Tools.ToDecimal(o.percent_variance_oncall, "0") + "%");
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
            dt.Columns.Add("Variance");
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
    }
}
