using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace RecruitmentSystem.Controllers
{
    [Authorize]
    public class RecruitmentProcessController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Dashboard(RecruitmentManpowerProcessViewModel i, int tab = 1)
        {
            ViewBag.mrfid = i.mrfid;
            i.tab = tab;
            return View(i);
        }
        public ActionResult ApplicantList(string mrfid)
        {
            ViewBag.mrfid = mrfid;
            return View();
        }
        public ActionResult _applicantlist(ApplicantListSearchViewModel model, string mrfid = "")
        {
            var list = new List<sp_applicant_list_searchResult>();
            if (model.Position != "" && model.EducationalAttainment != "")
            {
                ViewBag.mrfid = mrfid;
                var gender = (Tools.ReturnEmptyString(model.Gender)) == "M/F" ? "" : Tools.ReturnEmptyString(model.Gender);
                list = db.sp_applicant_list_search(Tools.ReturnEmptyString(model.Position), Tools.ReturnEmptyString(model.schoolname), Tools.ReturnEmptyString(model.course), Tools.ReturnEmptyString(model.Certification), Tools.ReturnEmptyString(model.skills), gender, Tools.ReturnEmptyString(model.Location), Tools.ReturnEmptyString(model.religion), Convert.ToInt32(model.agerequirementfrom), Convert.ToInt32(model.agerequirementto)).ToList();
                if (model.EducationalAttainment.ToLower() == "Post Graduate".ToLower())
                {
                    list = (from m in list where m.educationattainment.ToLower() == "Post Graduate".ToLower() select m).ToList();
                }
                else if (model.EducationalAttainment.ToLower() == "College Graduate".ToLower())
                {
                    list = (from m in list where m.educationattainment.ToLower() == "College Graduate".ToLower() || m.educationattainment.ToLower() == "Post Graduate".ToLower() select m).ToList();
                }
                else if (model.EducationalAttainment.ToLower() == "Graduate of Certificate Course".ToLower())
                {
                    list = (from m in list where m.educationattainment.ToLower() == "Post Graduate".ToLower() || m.educationattainment.ToLower() == "College Graduate".ToLower() || m.educationattainment.ToLower() == "Graduate of Certificate Course".ToLower() select m).ToList();
                }

                else if (model.EducationalAttainment.ToLower() == "Graduate of Vocational Course".ToLower())
                {
                    list = (from m in list where m.educationattainment.ToLower() == "Post Graduate".ToLower() || m.educationattainment.ToLower() == "College Graduate".ToLower() || m.educationattainment.ToLower() == "Graduate of Vocational Course".ToLower() || m.educationattainment.ToLower() == "Graduate of Certificate Course".ToLower() select m).ToList();
                }
                else if (model.EducationalAttainment.ToLower() == ("College Level").ToLower())
                {
                    list = (from m in list where m.educationattainment.ToLower() == "Post Graduate".ToLower() || m.educationattainment.ToLower() == "College Graduate".ToLower() || m.educationattainment.ToLower() == "Graduate of Certificate Course".ToLower() || m.educationattainment.ToLower() == "College Level".ToLower() select m).ToList();
                }
                else if (model.EducationalAttainment.ToLower() == ("High School Graduate").ToLower())
                {
                    list = (from m in list where m.educationattainment.ToLower() == "Post Graduate".ToLower() || m.educationattainment.ToLower() == "College Graduate".ToLower() || m.educationattainment.ToLower() == "College Level".ToLower() || m.educationattainment.ToLower() == "Graduate of Certificate Course".ToLower() || m.educationattainment.ToLower() == "Graduate of Vocational Course".ToLower() || m.educationattainment.ToLower() == "Post Graduate".ToLower() || m.educationattainment.ToLower() == "High School Graduate".ToLower() select m).ToList();
                }
                else if (model.EducationalAttainment.ToLower() == "Elementary Graduate")
                {
                    list = (from m in list where m.educationattainment.ToLower() == "Post Graduate".ToLower() || m.educationattainment.ToLower() == "College Graduate".ToLower() || m.educationattainment.ToLower() == "Graduate of Certificate Course".ToLower() || m.educationattainment.ToLower() == "College Level".ToLower() || m.educationattainment.ToLower() == "High School Graduate".ToLower() || m.educationattainment.ToLower() == "Elementary Graduate".ToLower() select m).ToList();
                }

                return View(list);
            }
            return View(list);
        }
        //invited tab
        public ActionResult InvitedApplicantList(string mrfid)
        {
            ViewBag.mrfid = mrfid;
            if (unitOfWork.ManpowerRepo.Find(filter: m => m.mrfid == mrfid).date_completed != null)
                return View(new List<sp_invited_applicant_listResult>());
            var list = db.sp_invited_applicant_list(mrfid).ToList();
            return View(list);
        }
        //confirm tab
        public ActionResult ConfirmedApplicantList(string mrfid)
        {
            ViewBag.mrfid = mrfid;
            if (unitOfWork.ManpowerRepo.Find(filter: m => m.mrfid == mrfid).date_completed != null)
                return View(new List<sp_confirmed_applicant_listResult>());
            var list = db.sp_confirmed_applicant_list(mrfid).ToList();
            return View(list);
        }
        public ActionResult ShorlistApplicantList(string mrfid)
        {
            ViewBag.mrfid = mrfid;
            if (unitOfWork.ManpowerRepo.Find(filter: m => m.mrfid == mrfid).date_completed != null)
                return View(new List<sp_shorlist_applicant_listResult>());
            var list = db.sp_shorlist_applicant_list(mrfid).ToList();
            return View(list);
        }
        public ActionResult ForRequirementApplicantList(string mrfid)
        {
            ViewBag.mrfid = mrfid;
            if (unitOfWork.ManpowerRepo.Find(filter: m => m.mrfid == mrfid).date_completed != null)
                return View(new List<sp_for_requirement_applicant_listResult>());
            var list = db.sp_for_requirement_applicant_list(mrfid).ToList();
            return View(list);
        }
        public ActionResult AcceptedApplicantList(string mrfid)
        {
            ViewBag.mrfid = mrfid;
            var list = db.sp_accepted_applicant_list(mrfid).ToList();

            return View(list);
        }
        public ActionResult VarianceApplicantList(string mrfid)
        {
            ViewBag.mrfid = mrfid;
            var list = db.sp_variance_applicant_list(mrfid).ToList();

            return View(list);
        }

        #region Inviting
        //applicant-list-tab

        public async Task<JsonResult> InviteApplicantListSearch(string mrfid, string applicant, bool sendsms = false)
        {
            SmsBlaster sms = new SmsBlaster();
            var retval = await sms.SendBlastInvited(mrfid, applicant, User.Identity.GetUserId(), sendsms);
            Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Send Invitation", mrfid);
            return Json(retval, JsonRequestBehavior.AllowGet);
        }
        //invited-tab confirm invited
        public JsonResult ConfirmedInvitedApplicant(string mrfid, string applicant, bool sendsms = false)
        {
            foreach (var i in applicant.Split(','))
            {
                db.sp_confirm_applicant_invited_applicant(mrfid, i);
            }
            Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Confirmed Invited Applicant", mrfid);
            return Json("Successfully added in Confirm Tab", JsonRequestBehavior.AllowGet);
        }
        //confirm-tab confirm confirmed applicant
        public async Task<JsonResult> ConfirmedApplicant(string mrfid, string applicant, bool sendsms = false)
        {
            SmsBlaster sms = new SmsBlaster();
            var retval = await sms.SendBlastShortlist(mrfid, applicant, User.Identity.GetUserId(), sendsms);

            foreach (var i in applicant.Split(','))
            {
                db.sp_confirm_applicant(mrfid, i);
                db.sp_add_applicant_history(i, mrfid + " - Confirm");
            }
            Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Confirm Applicant to Shortlist", mrfid);
            return Json("Successfully added in Shortlist Tab", JsonRequestBehavior.AllowGet);
        }
        //shortlist-tab confirm shortlist applicant
        public async Task<JsonResult> ConfirmShortlistApplicant(string mrfid, string applicant, bool sendsms = false)
        {
            SmsBlaster sms = new SmsBlaster();
            var classification = db.fn_get_manpower_classification(mrfid);
            if (classification == "One Time Placement")
            {
                await sms.SendBlastAccepted(mrfid, applicant, User.Identity.GetUserId(), sendsms);
                foreach (var i in applicant.Split(','))
                {
                    db.sp_confirm_shorlist_applicant(mrfid, i);
                    db.sp_confirm_accepted_applicant(mrfid, i);
                }
                return Json("Successfully added applicant(s) in Hired Tab", JsonRequestBehavior.AllowGet);
            }
            else
            {
                await sms.SendBlastForRequirement(mrfid, applicant, User.Identity.GetUserId(), sendsms);
                foreach (var i in applicant.Split(','))
                {
                    db.sp_confirm_shorlist_applicant(mrfid, i);
                }
                Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Confirm Applicant to For Requirement", mrfid);

                return Json("Successfully added in For Requirement Tab", JsonRequestBehavior.AllowGet);
            }

        }
        //for-requirement-tab confirm requirement applicant
        public async Task<JsonResult> ConfirmForRequirementApplicant(string mrfid, string applicant, bool sendsms = false)
        {
            SmsBlaster sms = new SmsBlaster();
            await sms.SendBlastAccepted(mrfid, applicant, User.Identity.GetUserId(), sendsms);
            foreach (var i in applicant.Split(','))
            {
                db.sp_confirm_accepted_applicant(mrfid, i);
            }
            Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Successfully added in Hired Tab", mrfid);

            return Json("Successfully added in For Accepted Tab", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ConfirmVarianceApplicant(string mrfid, string applicant, int tab)
        {
            foreach (var i in applicant.Split(','))
            {
                db.sp_confirm_variance_applicant(mrfid, i);
            }
            Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Confirm Applicant to Variance", mrfid);

            return RedirectToAction("dashboard", new RecruitmentManpowerProcessViewModel() { tab = tab, mrfid = mrfid, SuccessMessage = "Successfully added in For Variance Tab" });
        }
        #endregion
    }

}