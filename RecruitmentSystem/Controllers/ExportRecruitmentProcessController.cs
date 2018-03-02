using RecruitmentSystem.Recruitment.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ExportRecruitmentProcessController : Controller
    {
       public ActionResult ExportInvitedApplicant(string mrfid)
        {
            ApplicantExport export = new ApplicantExport();
            var ms = export.ExportInvitedApplicant(mrfid);
            return File(ms.ToArray(), "application/vnd.ms-excel", "Applicant-List-" + mrfid+".xls");
        }

       public ActionResult ExportConfirmedApplicant(string mrfid)
       {
           ApplicantExport export = new ApplicantExport();
           var ms = export.ExportConfirmedApplicant(mrfid);
           return File(ms.ToArray(), "application/vnd.ms-excel", "Applicant-Confirmed-List-" + mrfid + ".xls");
       }
       public ActionResult ExportShortlistApplicant(string mrfid)
       {
           ApplicantExport export = new ApplicantExport();
           var ms = export.ExportShortlistApplicant(mrfid);
           return File(ms.ToArray(), "application/vnd.ms-excel", "Applicant-Shortlist-List-" + mrfid + ".xls");
       }
       public ActionResult ExportForRequirementApplicant(string mrfid)
       {
           ApplicantExport export = new ApplicantExport();
           var ms = export.ExportForRequirementApplicant(mrfid);
           return File(ms.ToArray(), "application/vnd.ms-excel", "Applicant-For-Requirement-List-" + mrfid + ".xls");
       }
        public ActionResult ExportAcceptedRequirement(string mrfid)
       {
           ApplicantExport export = new ApplicantExport();
           var ms = export.ExportAcceptedApplicant(mrfid);
           return File(ms.ToArray(), "application/vnd.ms-excel", "Placement Report - " + mrfid + ".xls");
       }
    }
}