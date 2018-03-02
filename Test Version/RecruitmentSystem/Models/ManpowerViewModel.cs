using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using RecruitmentSystem.Recruitment.Class;
using System.Threading.Tasks;
namespace RecruitmentSystem.Models
{
    public class ManpowerViewModel : Urls
    {
        public ManpowerViewModel()
        {
            User = HttpContext.Current.User;
            UserId = User.Identity.GetUserId();
        }
        string generateurl(string emailaddress, string mrfid)
        {
            var Request = HttpContext.Current.Request;
            var url = "http://" + Request.Url.Host + ":" + Request.Url.Port + Action("manpowerdetail", "manpoweremailnotification", new { mrfid = mrfid, email = emailaddress });
            return url;
        }
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public string UserId { get; set; }
        public IPrincipal User { get; set; }
        public string mrfid { get; set; }
        public string Remarks { get; set; }
        public sp_manpower_detailResult ManpowerDetail()
        {
            var detail = db.sp_manpower_detail(mrfid).FirstOrDefault();
            return detail;
        }
        public void CompleteManpower()
        {
            db.sp_complete_manpower(mrfid);
        }
        public List<sp_manpower_audit_trail_listResult> AuditTrail()
        {
            var list = db.sp_manpower_audit_trail_list(mrfid).ToList();
            return (list.Skip(0 * 10).Take(10).ToList());
        }
        public async Task<int> ApprovedManpower()
        {
            var detail = db.sp_manpower_detail(mrfid).FirstOrDefault();
            var userid = User.Identity.GetUserId();
            var role = Users.Role(userid);
            var user_involved = db.sp_get_mrf_user_involved(mrfid).FirstOrDefault();
            var adminlist = db.sp_admin_list().ToList();
            string approvedmessage = "";
            string EmailAddress = "";
            string Recipient = "";
            //approved accountmanager
            if (detail.am_approved == false && role == "Account Manager")
            {

                string recruitmentsupervisor = db.fn_get_supervisor_id(UserId);
                approvedmessage = mrfid + " Has Been Approved and Assigned to " + Users.Fullname(recruitmentsupervisor);
                db.sp_approved_manpower_accountmanager(mrfid, recruitmentsupervisor);
                //coor
                db.sp_add_notification(userid, user_involved.coordinator_id, approvedmessage, mrfid);
                //rec sup
                db.sp_add_notification(userid, recruitmentsupervisor, approvedmessage, mrfid);
                //send email to supervisor
                EmailAddress = Users.EmailAddress(recruitmentsupervisor);
                Recipient = recruitmentsupervisor;
                //admin


            }
            //approved supervisor
            else if (detail.am_approved == true && detail.recruitment_supervisor_approved == false && User.IsInRole("Recruitment Supervisor"))
            {
                approvedmessage = mrfid + " Has been assigned to " + Users.Fullname("") + " for OIC allocation";
                db.sp_approved_manpower_recruitment_supervisor(mrfid, "");
                //notify coor
                db.sp_add_notification(userid, user_involved.coordinator_id, approvedmessage, mrfid);
                //notfy am
                db.sp_add_notification(userid, user_involved.am_1_id, approvedmessage, mrfid);
                db.sp_add_notification(userid, user_involved.am_2_id, approvedmessage, mrfid);
                //notify oic
                db.sp_add_notification(userid, "", approvedmessage, mrfid);
                //email send to oic
                await Task.Run(new Action(() =>
                {
                    EmailSender email = new EmailSender();
                    email.EmailNotification(generateurl(Users.EmailAddress(""), mrfid), mrfid, "");

                }));
            }
            //approved oic
            else if ((detail.recruitment_supervisor_approved == true && User.IsInRole("Recruitment Supervisor")) || (detail.oic_recruiter_approved == false && User.IsInRole("OIC Recruiter")))
            {
                approvedmessage = mrfid + " Has been assigned to " + Users.Fullname("") + " for applicant placement";
                db.sp_approved_manpower_oic_recruiter(mrfid, "");
                //notf coor
                db.sp_add_notification(userid, user_involved.coordinator_id, approvedmessage, mrfid);
                //notfy ams
                db.sp_add_notification(userid, user_involved.am_1_id, approvedmessage, mrfid);
                db.sp_add_notification(userid, user_involved.am_2_id, approvedmessage, mrfid);
                //notfy super
                db.sp_add_notification(userid, user_involved.recruitment_supervisor_id, approvedmessage, mrfid);
                //notify recruiter
                db.sp_add_notification(userid, "", approvedmessage, mrfid);
                ////email send to recruiter
                await Task.Run(new Action(() =>
                {
                    EmailSender email = new EmailSender();
                    email.EmailNotification(generateurl(Users.EmailAddress(""), mrfid), mrfid, "");

                }));
                //notify oic
                db.sp_add_notification(userid, user_involved.oic_recruiter_id, approvedmessage, mrfid);
                //notify admin
            }

            //email notification
            await Task.Run(new Action(() =>
            {
                EmailSender email = new EmailSender();
                email.EmailNotification(generateurl(EmailAddress, mrfid), mrfid, Recipient);

            }));
            foreach (var i in adminlist)
            {
                db.sp_add_notification(userid, i.UserId, approvedmessage, mrfid);
            }
            return 0;
        }
        public class ManpowerNotification
        {
            public string from { get; set; }
            public string to { get; set; }
            public string notification_message { get; set; }
            public string mrfid { get; set; }
        }
    }

}