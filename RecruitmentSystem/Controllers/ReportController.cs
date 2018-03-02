using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.Recruitment.Report.Endorsement;
using System.Data.SqlClient;
using RecruitmentSystem.Recruitment.Report.Endorsement.EndorsementTableAdapters;
using CrystalDecisions.Shared;
using System.Data;
using System.IO;
using RecruitmentSystem.Recruitment.Data;
using RecruitmentSystem.Recruitment.Report.Contracts;
namespace RecruitmentSystem.Controllers
{
    public class ReportController : Controller
    {
        #region Endorsement
        public ActionResult EndorsementLetter(string mrfid, string exporttype = "PDF",string applicantlist="")
        {
            Endorsement endorsement = new Endorsement();
            Endorsement.sp_print_endorsement_letterDataTable dt = new Endorsement.sp_print_endorsement_letterDataTable();
            sp_print_endorsement_letterTableAdapter da = new sp_print_endorsement_letterTableAdapter();
        
            da.Fill(dt, mrfid,applicantlist);
            rpt_endorsement_letter rpt = new rpt_endorsement_letter();
            rpt.SetDataSource((DataTable)dt);
            Stream sr;
            if (exporttype == "PDF")
            {
                sr = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
                return File(sr, "application/pdf", string.Format("Endorsement Letter - {0}.pdf", mrfid));
            }
            else
            {
                sr = rpt.ExportToStream(ExportFormatType.WordForWindows);
                return File(sr, "application/msword", string.Format("Endorsement Letter - {0}.doc", mrfid));
            }
        }
        #endregion

        public ActionResult Contract(string mrfid, string applicants, string classification)
        {
            ReportContract report = new ReportContract();

            return File(report.Contract(mrfid, applicants, classification), "application/msword", "Contract - " + mrfid + ".doc");
        }
    }
}