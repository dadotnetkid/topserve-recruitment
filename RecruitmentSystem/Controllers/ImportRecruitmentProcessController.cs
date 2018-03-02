using RecruitmentSystem.Recruitment.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace RecruitmentSystem.Controllers
{
    public class ImportRecruitmentProcessController : Controller
    {
        public JsonResult ImportConfirmInvitedApplicant()
        {
            ApplicantImport.ImportConfirmedInvitedApplicant(Path(),User.Identity.GetUserId());

            return Json("Successfully Import Confirmed Applicant", JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ImportConfirmApplicant()
        {
            await Task.Run(new Action(() =>
            {
                ApplicantImport.ImportConfirmedApplicant(Path(), User.Identity.GetUserId());
            }));
            return Json("Successfully Import Shortlist Applicant", JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ImportShortlistApplicant()
        {
            await Task.Run(new Action(() =>
               {
                   ApplicantImport.ImportShortlistApplicant(Path(), User.Identity.GetUserId());
               }));
            return Json("Successfully Import Shortlist Applicant", JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ImportForRequirementApplicant()
        {
            await Task.Run(new Action(() =>
            {
                ApplicantImport.ImportForRequirementApplicant(Path(), User.Identity.GetUserId());
            }));
            return Json("Successfully Import For Requirement Applicant", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImportPassOnApplicant(string Mrfid)
        {
            ApplicantImport.ImportPassOnApplicant(Path(), Mrfid, User.Identity.GetUserId());
            return Json("Successfully Import For Requirement Applicant", JsonRequestBehavior.AllowGet);
        }
        string Path()
        {
            var files = Request.Files[0];
            var filename = Tools.RandomFileName(files);
            string path = Server.MapPath("~/excel/" + filename);
            files.SaveAs(path);
            return path;
        }
    }
}