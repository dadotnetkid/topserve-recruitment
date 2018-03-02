using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RecruitmentSystem.Recruitment.Data;
using System.Net;
using System.IO;
using RecruitmentSystem.Recruitment.Class;
using System.Threading.Tasks;

namespace RecruitmentSystem.Controllers
{
    [AllowAnonymous]
    public class ManpowerEmailNotificationController : AccountManager
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ActionResult ApprovedManpower(string mrfid, string emailaddress)
        {
            var userid = UserManager.FindByEmail(emailaddress);
            string recruitmentsupervisor = db.fn_get_supervisor_id(userid.Id);
            db.sp_approved_manpower_accountmanager(mrfid, recruitmentsupervisor);
            var adminlist = db.sp_admin_list().ToList();
            var user_involved = db.sp_get_mrf_user_involved(mrfid).FirstOrDefault();
            //coor
            db.sp_add_notification(userid.Id, user_involved.coordinator_id, mrfid + " Has Been Approved and Assigned to " + Users.Fullname(recruitmentsupervisor), mrfid);
            //rec sup
            db.sp_add_notification(userid.Id, recruitmentsupervisor, mrfid + " Has Been Approved and Assigned to " + Users.Fullname(recruitmentsupervisor), mrfid);
            //admin
            foreach (var i in adminlist)
            {
                db.sp_add_notification(userid.Id, i.UserId, mrfid + " Has Been Approved and Assigned to " + Users.Fullname(recruitmentsupervisor), mrfid);
            }
            return RedirectToAction("dashboard", "RecruitmentProcess", new { mrfid = mrfid });
        }

        public ActionResult RejectManpower(string mrfid, string emailaddress)
        {
            var userid = UserManager.FindByEmail(emailaddress);
            string recruitmentsupervisor = db.fn_get_supervisor_id(userid.Id);
            db.sp_approved_manpower_accountmanager(mrfid, recruitmentsupervisor);
            var adminlist = db.sp_admin_list().ToList();
            var user_involved = db.sp_get_mrf_user_involved(mrfid).FirstOrDefault();
            //coor
            db.sp_add_notification(userid.Id, user_involved.coordinator_id, mrfid + " Has Been Approved and Assigned to " + Users.Fullname(recruitmentsupervisor), mrfid);
            //rec sup
            db.sp_add_notification(userid.Id, recruitmentsupervisor, mrfid + " Has Been Approved and Assigned to " + Users.Fullname(recruitmentsupervisor), mrfid);
            //admin
            foreach (var i in adminlist)
            {
                db.sp_add_notification(userid.Id, i.UserId, mrfid + " Has Been Approved and Assigned to " + Users.Fullname(recruitmentsupervisor), mrfid);
            }
            return RedirectToAction("dashboard", "RecruitmentProcess", new { mrfid = mrfid });
        }



        public ActionResult ManpowerDetail(string mrfid, string email)
        {
            var detail = db.sp_manpower_detail(mrfid).FirstOrDefault();
            ViewBag.email = email;
            return View(detail);
        }




        public async Task<ActionResult> sendemail(string mrfid)
        {
            var url = "http://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("manpowerdetail", "manpoweremailnotification", new { mrfid = mrfid, email = "test.smtp.hsgsoftware@gmail.com" });
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream sr = response.GetResponseStream();
            string data = new StreamReader(sr).ReadToEnd();
            var email = new EmailSender();
            await Task.Run(new Action(() =>
            {
                //email.EmailNotification("test.smtp.hsgsoftware@gmail.com", "Topserver Manpower Request - " + mrfid, data);
            }));
            return View();
        }
    }
}