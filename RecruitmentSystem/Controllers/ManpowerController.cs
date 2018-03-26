using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RecruitmentSystem.Recruitment.Class;
using System.Threading.Tasks;

namespace RecruitmentSystem.Controllers
{
    [Authorize]
    public class ManpowerController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        public ActionResult CreateManpowerRequest()
        {
            var model = new CreateManpowerViewModel();
            ViewBag.ManpowerRequestActive = "active";
            return View(model);
        }
        string generateurl(string emailaddress, string mrfid)
        {
            var url = "http://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("manpowerdetail", "manpoweremailnotification", new { mrfid = mrfid, email = emailaddress });
            return url;
        }
        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        public async Task<ActionResult> CreateManpowerRequest(CreateManpowerViewModel i)
        {
            string userid = User.Identity.GetUserId();
            var mrfid = db.fn_generate_mrf_id(userid, i.BusinessUnit);
            var am = db.sp_get_account_manager_id(userid).FirstOrDefault();
            var User_fullname = Users.Fullname(userid);
            //create mrf
            db.sp_create_mrf_request(userid, mrfid, i.DateRequested, i.DateofDeployment, i.RequiredPosition, i.RequiredNumber, i.EducationalAttainment, i.Course, i.Gender, i.AgeRequirement, i.SkillType, i.SpecificSkill, i.Certification, i.CostCenter, i.Department, Tools.ToTitleCase(i.JobDescription), i.SalaryDetails, i.BasicPay, i.Cola, i.Skilled, i.Meal, i.Transportation, i.Gas, i.Communication, i.Motorcycle, i.Clothing, (decimal)i.Medical, i.PayoutDate, Tools.ToTitleCase(i.WhotoLookfor), Tools.ToTitleCase(i.Establishment), Tools.ToTitleCase(i.OfficeAddresstoReport), i.LocationofDeployment, i.BusinessUnit, i.Classification, i.EmploymentStatus, i.ProjectName, i.CompanyRequested, Tools.ToTitleCase(i.Requestor), i.RequestorContactNumber, i.RequestorEmailAddress);
            //notify 2 am
            db.sp_add_notification(userid, am.account_manager_1, string.Format("{0} sent {1} to {2} for approval", User_fullname, mrfid, Users.Fullname(am.account_manager_1)), mrfid);
            db.sp_add_notification(userid, am.account_manager_2, string.Format("{0} sent {1} to {2} for approval", User_fullname, mrfid, Users.Fullname(am.account_manager_2)), mrfid);
            //notify admins
            foreach (var admin in db.sp_admin_list().ToList())
            {
                db.sp_add_notification(userid, admin.UserId, string.Format("{0} sent {1} to {2} for approval", User_fullname, mrfid, Users.Fullname(am.account_manager_1)), mrfid);
                db.sp_add_notification(userid, admin.UserId, string.Format("{0} sent {1} to {2} for approval", User_fullname, mrfid, Users.Fullname(am.account_manager_2)), mrfid);
            }
            //Send approval via email
            await Task.Run(new Action(() =>
            {
                EmailSender email = new EmailSender();
                email.EmailNotification(generateurl(Users.EmailAddress(am.account_manager_1), mrfid), mrfid, am.account_manager_1);
                email.EmailNotification(generateurl(Users.EmailAddress(am.account_manager_1), mrfid), mrfid, am.account_manager_2);
            }));
            i = new CreateManpowerViewModel();
            return RedirectToAction("dashboard", "manpower", new { createmanpower = "Successfully created Manpower Request " + mrfid });
            //return View(i);
        }
        [AllowAnonymous]
        public ActionResult CourseList(string search = "")
        {
            var list = db.sp_course_list(search, true).ToList();
            ViewBag.count = list.Count();
            return View(list.Skip(5 * 0).Take(5));
        }
        public ActionResult UpdateManpowerRequest()
        {

            return View();
        }
        public ActionResult Dashboard(ErrorHandlers errorHandler, string createmanpower = "", string Filter = "")
        {
            ViewBag.createmanpower = createmanpower;
            ViewBag.ManpowerRequestActive = "active";
            ManpowerDashboardViewModel i = new ManpowerDashboardViewModel() { Filter = Filter, ErrorHandlers = errorHandler };
            return View(i);
        }

        public ActionResult _Dashboard(string Search = "", string Classification = "", string Status = "", string BusinessUnit = "", string Filter = "")
        {
            ManpowerDashboardViewModel i = new ManpowerDashboardViewModel() { Classification = Classification, Search = Search, Status = Status, BusinessUnit = BusinessUnit, Filter = Filter };
            return View(i);
        }
        public ActionResult minidashboard()
        {
            var list = db.sp_manpower_list("", "", "", "", "", User.IsInRole("Admin") ? "" : User.Identity.GetUserId(), db.fn_get_user_role(User.Identity.GetUserId())).ToList();

            return View(list.Skip(10 * 0).Take(10));
        }
        [HttpGet]
        public ActionResult ManpowerRequestDetail(ManpowerViewModel i)
        {
            ViewBag.ManpowerRequestActive = "Active";
            if (i.mrfid == null)
            {
                return RedirectToAction("dashboard");
            }

            return View(i);
        }
        [HttpGet]
        public ActionResult UpdateManpowerRequestDetail(string mrfid)
        {
            var i = db.sp_manpower_detail(mrfid).FirstOrDefault();
            var detail = new UpdateManpowerViewModel()
            {
                mrfid = mrfid,
                DateDeployment = (DateTime)i.DateofDeployment,
                position = i.RequiredPosition,
                agerequirement = i.AgeRequirement,
                numberofrequired = i.RequiredNumber.ParseToInt(),
                educationattainment = i.EducationalAttainment,
                course = i.Course,
                gender = i.Gender,
                skilltype = i.SkillType,
                skills = i.SpecificSkill,
                certification = i.certificate_id,
                costcenter = i.CostCenter,
                department = i.Department,
                jobdescription = Tools.ToTitleCase(i.JobDescription),
                salarydetails = i.SalaryDetails,
                basicpay = i.BasicPay.ParseToDecimal(),
                cola = i.Cola.ParseToDecimal(),
                skilled = i.Skilled.ParseToDecimal(),
                meal = i.Meal.ParseToDecimal(),
                transportation = i.Transportation.ParseToDecimal(),
                gas = i.Gas.ParseToDecimal(),
                communication = i.Communication.ParseToDecimal(),
                clothing = i.Clothing.ParseToDecimal(),
                medical = i.Medical.ParseToDecimal(),
                payoutdate = i.PayoutDate,
                whotolookfor = Tools.ToTitleCase(i.WhotoLookfor),
                establishment = Tools.ToTitleCase(i.Establishment),
                officeaddresstoreport = Tools.ToTitleCase(i.OfficeAddresstoReport),
                location = i.location_id,
                classification = i.Classification,
                projectname = Tools.ToTitleCase(i.ProjectName),
                companyrequested = i.company_id,
                requestor = Tools.ToTitleCase(i.Requestor),
                requestorcontactnumber = i.RequestorContactNumber,
                requestoremailaddress = i.RequestorEmailAddress,
                businessunit = i.BusinessUnit,
                employmentstatus = i.EmploymentStatus,
                Course = i.Course

            };
            return View(detail);
        }
        [HttpPost]
        public ActionResult UpdateManpowerRequestDetail_(CreateManpowerViewModel i, string mrfid)
        {
            db.sp_update_manpower_request(mrfid, i.DateofDeployment, i.RequiredPosition, i.RequiredNumber, i.EducationalAttainment, i.Course, i.Gender, i.AgeRequirement, i.SkillType,
                i.SpecificSkill, i.Certification, i.CostCenter, i.Department, i.JobDescription, i.SalaryDetails, i.BasicPay, i.Cola, i.Skilled, i.Meal, i.Transportation, i.Gas, i.Communication, i.Motorcycle, i.Clothing, i.Medical, i.PayoutDate, i.WhotoLookfor, i.Establishment, i.OfficeAddresstoReport, i.LocationofDeployment, i.Classification, i.ProjectName, i.CompanyRequested, i.Requestor, i.RequestorContactNumber, i.RequestorEmailAddress, "For AM Approval");
            var user = db.sp_get_mrf_user_involved(mrfid).FirstOrDefault();
            var userid = User.Identity.GetUserId();
            db.sp_add_notification(userid, user.am_1_id, Users.Fullname(userid) + " sent " + mrfid + " to " + Users.Fullname(user.am_1_id) + " for approval", mrfid);
            db.sp_add_notification(userid, user.am_2_id, Users.Fullname(userid) + " sent " + mrfid + " to " + Users.Fullname(user.am_2_id) + " for approval", mrfid);
            return RedirectToAction("dashboard", "recruitmentprocess", new { mrfid = mrfid });
        }
        [HttpPost]
        public async Task<ActionResult> ApproveManpower(string mrfid, string Remarks, string oic_recruiter_id = "", string recruiter_id = "", string recruitment_supervisor_id = "")
        {
            var detail = db.sp_manpower_detail(mrfid).FirstOrDefault();
            var userid = User.Identity.GetUserId();
            var role = Users.Role(userid);
            var user_involved = db.sp_get_mrf_user_involved(mrfid).FirstOrDefault();
            var adminlist = db.sp_admin_list().ToList();
            string approvedmessage = "";
            List<ManpowerViewModel.ManpowerNotification> Notification = new List<ManpowerViewModel.ManpowerNotification>();

            //approved accountmanager
            if (detail.am_approved == false && role == "Account Manager")
            {

                approvedmessage = mrfid + " Has been approved and assigned to " + Users.Fullname(recruitment_supervisor_id);
                db.sp_approved_manpower_accountmanager(mrfid, recruitment_supervisor_id);
                db.ExecuteCommand("update tbl_manpower_request set remarks={0} where mrfid={1}", Remarks, mrfid);
                foreach (var m in new string[] { user_involved.coordinator_id, recruitment_supervisor_id })
                {
                    Notification.Add(new ManpowerViewModel.ManpowerNotification() { from = userid, to = m, notification_message = approvedmessage, mrfid = mrfid });
                }

                //send email to supervisor
                await Task.Run(new Action(() =>
                {
                    EmailSender email = new EmailSender();
                    email.EmailNotification(generateurl(Users.EmailAddress(recruitment_supervisor_id), mrfid), mrfid, recruitment_supervisor_id);

                }));
            }
            //approved supervisor
            else if (detail.am_approved == true && detail.recruitment_supervisor_approved == false && User.IsInRole("Recruitment Supervisor"))
            {
                approvedmessage = mrfid + " Has been assigned to " + Users.Fullname(recruiter_id) + " for Recruiter allocation";
                db.sp_approved_manpower_recruitment_supervisor(mrfid, recruiter_id);
                foreach (var m in new string[] { user_involved.coordinator_id, user_involved.am_1_id, user_involved.am_2_id, recruiter_id })
                {
                    Notification.Add(new ManpowerViewModel.ManpowerNotification() { from = userid, to = m, notification_message = approvedmessage, mrfid = mrfid });
                }
                //email send to oic
                await Task.Run(new Action(() =>
                {
                    EmailSender email = new EmailSender();
                    email.EmailNotification(generateurl(Users.EmailAddress(recruiter_id), mrfid), mrfid, recruiter_id);

                }));
            }

            //notification loop all added in list of notification
            foreach (var m in Notification)
            {
                db.sp_add_notification(m.from, m.to, m.notification_message, m.mrfid);
            }
            //notify admin
            foreach (var i in adminlist)
            {
                db.sp_add_notification(userid, i.UserId, approvedmessage, mrfid);
            }

            Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Approved the Manpower Request", mrfid);

            await Task.Run(new Action(() =>
            {
                new EmailSender().EmailNotificationRejectCancelled(mrfid, string.Format("{0} Has been approved.", mrfid), string.Format("{0} Has been approved.", mrfid), "", Recruitment.Class.Users.EmailAddress(user_involved.coordinator_id), Url.Action("ManpowerRequestDetail", "manpower", new { mrfid = mrfid }));
            }));

            return RedirectToAction("dashboard", "RecruitmentProcess", new { mrfid = mrfid, approved = approvedmessage });
        }
        [HttpPost]
        public async Task<ActionResult> RejectManpower(string remarks, string mrfid)
        {
            var user_involved = db.sp_get_mrf_user_involved(mrfid).FirstOrDefault();
            var user = User.Identity.GetUserId();
            db.sp_add_notification(user, user_involved.coordinator_id, Users.Fullname(User.Identity.GetUserId()) + " updated, to Rejected", mrfid);
            db.sp_reject_manpower_request(remarks, mrfid);
            Manpower.AddAuditTrail(user, "Manpower is rejected", mrfid);
            await Task.Run(new Action(() =>
            {
                new EmailSender().EmailNotificationRejectCancelled(mrfid, string.Format("{0} Has been rejected", mrfid), string.Format("{0} Has been rejected", mrfid), remarks, Recruitment.Class.Users.EmailAddress(user_involved.coordinator_id), Url.Action("ManpowerRequestDetail", "manpower", new { mrfid = mrfid }));
            }));
            foreach (var i in db.sp_admin_list())
            {
                db.sp_add_notification(user, i.UserId, mrfid + " Has been rejected by " + Users.Fullname(User.Identity.GetUserId()), mrfid);
            }
            return RedirectToAction("dashboard", "recruitmentprocess", new { mrfid = mrfid, reject = "rejected" });
        }
        [HttpPost]
        public ActionResult CancelRequirement(int numbercancelrequirement, string mrfid)
        {

            db.sp_cancelled_requirement(mrfid, (int)numbercancelrequirement);
            Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Update Number of Cancelled", mrfid);
            return RedirectToAction("dashboard", "recruitmentprocess", new { mrfid = mrfid });
        }
        public async Task<ActionResult> CancelManpower(string mrfid, string reason)
        {
            db.sp_cancel_manpower(mrfid);
            var user_involved = db.sp_get_mrf_user_involved(mrfid).FirstOrDefault();
            var user = User.Identity.GetUserId();
            db.sp_add_notification(user, user_involved.coordinator_id, $"Manpower is Cancelled {Environment.NewLine + reason}", mrfid);
            var manpower = await unitOfWork.ManpowerRepo.FindAsync(m => m.mrfid == mrfid);
            if (manpower.Classification == ManpowerClassification.New ||
                manpower.Classification == ManpowerClassification.Replacement)
            {
                manpower.RequiredNumber = 0;
                await unitOfWork.SaveAsync();
            }
            Manpower.AddAuditTrail(User.Identity.GetUserId(), Users.Fullname(User.Identity.GetUserId()) + " Cancelled Manpower", mrfid);


            await Task.Run(new Action(() =>
            {
                new EmailSender().EmailNotificationRejectCancelled(mrfid, string.Format("{0} Has been cancelled", mrfid), string.Format("{0} Has been cancelled", mrfid), "", Recruitment.Class.Users.EmailAddress(user_involved.coordinator_id), Url.Action("ManpowerRequestDetail", "manpower", new { mrfid = mrfid }));
            }));
            return RedirectToAction("dashboard", "recruitmentprocess", new { mrfid = mrfid });
        }
        public ActionResult ReAssignRecruiterList(String MRFID)
        {
            ManpowerViewModel i = new ManpowerViewModel() { mrfid = MRFID };
            return View(i);
        }

        public ActionResult _ReAssignRecruiterList(ManpowerViewModel i)
        {
            return View(i);
        }
        [HttpPost]
        public ActionResult ReAssignRecruiter(ManpowerViewModel i)
        {
            i.ReAssignedManpower();
            return RedirectToAction("dashboard", "recruitmentprocess", new { mrfid = i.mrfid });
        }
        public ActionResult AuditTrail(ManpowerViewModel i)
        {
            return View(i.AuditTrail());
        }
        public ActionResult CompleteManpower(ManpowerViewModel i)
        {
            i.CompleteManpower();
            return RedirectToAction("dashboard", "recruitmentprocess", new { mrfid = i.mrfid });
        }
        public JsonResult RecruiterListSearch(string Recruiter)
        {
            ManpowerViewModel model = new ManpowerViewModel();
            return Json(model.RecruiterListSearch(), JsonRequestBehavior.AllowGet);
        }
    }
}