using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentSystem.Recruitment.Class
{
    public class EmailSender
    {
        public async Task<int> sendemail(string emailto, string subject, string body)
        {
            await Task.Run(new Action(() =>
            {
                try
                {
                    sp_email_setting_detailResult list = new sp_email_setting_detailResult();
                    using (DatabaseModelDataContext db = new DatabaseModelDataContext())
                    {
                        list = db.sp_email_setting_detail().FirstOrDefault();
                    }


                    SmtpClient client = new SmtpClient(list.host, (int)list.port)
                    {
                        EnableSsl = (bool)list.ssl,
                        Credentials = new System.Net.NetworkCredential(list.email, list.password),

                    };
                    MailMessage message = new MailMessage(list.email, emailto, subject, body)
                    {
                        IsBodyHtml = true
                    };


                    client.SendAsync(message, null);


                }
                catch (Exception)
                {


                }

            }));
            return 0;

        }
        public async void sendemail(string emailto, string subject, string body, bool ishtml = true)
        {
            await Task.Run(new Action(() =>
            {
                try
                {
                    sp_email_setting_detailResult list = new sp_email_setting_detailResult();
                    using (DatabaseModelDataContext db = new DatabaseModelDataContext())
                    {
                        list = db.sp_email_setting_detail().FirstOrDefault();
                    }


                    SmtpClient client = new SmtpClient(list.host, (int)list.port)
                    {
                        EnableSsl = (bool)list.ssl,
                        Credentials = new System.Net.NetworkCredential(list.email, list.password),

                    };
                    MailMessage message = new MailMessage(list.email, emailto, subject, body)
                    {
                        IsBodyHtml = ishtml
                    };


                    client.SendAsync(message, null);


                }
                catch (Exception)
                {


                }

            }));


        }
        public async void InvitedEmailMessage(string email, string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var recruiter_detail = db.sp_recruiter_contact_detail(userid).FirstOrDefault();
            try
            {
                var detail = db.sp_email_message_detail().FirstOrDefault();
                await Task.Run(new Action(() =>
                {
                    sendemail(email, "You are invited to interview",
                        detail.invited
                        .Replace("[number]", recruiter_detail.contactnumber)
                        .Replace("[branch]", recruiter_detail.branch_address)
                        .Replace("[name]", recruiter_detail.recruiter_name)
                        );
                }));
            }
            catch (Exception)
            {


            }

        }
        public async void ShortlistEmailMessage(string email, string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var recruiter_detail = db.sp_recruiter_contact_detail(userid).FirstOrDefault();
            try
            {
                var detail = db.sp_email_message_detail().FirstOrDefault();
                await Task.Run(new Action(() =>
                {
                    sendemail(email, "Congatulation you passed the interview",
                        detail.shortlist
                        .Replace("[number]", recruiter_detail.contactnumber)
                        .Replace("[branch]", recruiter_detail.branch_address)
                        .Replace("[name]", recruiter_detail.recruiter_name)
                        );
                }));
            }
            catch (Exception)
            {


            }

        }
        public async void ForRequirementEmailMessage(string email, string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var recruiter_detail = db.sp_recruiter_contact_detail(userid).FirstOrDefault();
            try
            {
                var detail = db.sp_email_message_detail().FirstOrDefault();
                await Task.Run(new Action(() =>
                {
                    sendemail(email, "Congatulation You Passed the Interview",
                        detail.forrequirement
                        .Replace("[number]", recruiter_detail.contactnumber)
                        .Replace("[branch]", recruiter_detail.branch_address)
                        .Replace("[name]", recruiter_detail.recruiter_name)
                        );
                }));
            }
            catch (Exception)
            {


            }

        }
        public async void AcceptedEmailMessage(string email, string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var recruiter_detail = db.sp_recruiter_contact_detail(userid).FirstOrDefault();
            try
            {
                var detail = db.sp_email_message_detail().FirstOrDefault();
                await Task.Run(new Action(() =>
                {
                    sendemail(email, "Congratulation you are now hired",
                        detail.accepted
                        .Replace("[number]", recruiter_detail.contactnumber)
                        .Replace("[branch]", recruiter_detail.branch_address)
                        .Replace("[name]", recruiter_detail.recruiter_name)
                        );
                }));
            }
            catch (Exception)
            {
            }

        }
        //public async void EmailNotification(string email, string subject, string body)
        //{

        //    try
        //    {
        //        await Task.Run(new Action(() =>
        //        {
        //            sendemail(email, subject, body);
        //        }));
        //    }
        //    catch (Exception)
        //    {
        //    }

        //}
        public async void EmailNotification(string url, string mrfid, string am1, string am2)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream sr = response.GetResponseStream();
                string data = new StreamReader(sr).ReadToEnd();
                await Task.Run(new Action(() =>
                {
                    sendemail(Users.EmailAddress(am1), "Manpower Request - " + mrfid, data);
                    sendemail(Users.EmailAddress(am2), "Manpower Request - " + mrfid, data);
                    sendemail("test.smtp.hsgsoftware@gmail.com", "Manpower Request - " + mrfid, data);
                }));
            }
            catch (Exception)
            {
            }

        }
        public async void EmailNotification(string url, string mrfid, string am)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream sr = response.GetResponseStream();
                string data = new StreamReader(sr).ReadToEnd();
                await Task.Run(new Action(() =>
                {
                    sendemail(Users.EmailAddress(am), "Manpower Request - " + mrfid, data);
                    sendemail("test.smtp.hsgsoftware@gmail.com", "Manpower Request - " + mrfid, data);
                }));
            }
            catch (Exception)
            {
            }

        }
        public async Task<int> EmailNotificationImport(string url, string mrfid, string am)
        {

            try
            {
                url = url + "&mrfid=" + mrfid;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream sr = response.GetResponseStream();
                string data = new StreamReader(sr).ReadToEnd();

                await sendemail(Users.EmailAddress(am), "Manpower Request - " + mrfid, data);
                await sendemail("test.smtp.hsgsoftware@gmail.com", "Manpower Request - " + mrfid, data);

            }
            catch (Exception)
            {
            }
            return 0;
        }
        public async void EmailNotificationRejectCancelled(string mrfid, string subject, string message, string remarks, string email = "", string url = "")
        {
            var msg = string.Format("<strong>{0}</strong>", message);
            if (remarks != "")
            {
                msg += string.Format("<br/><br/> Remarks:{0}", remarks);
            }

            msg += string.Format("<br/><br/> <a href='{1}'>{0}</a>", mrfid, url);
            await Task.Run(new Action(() =>
            {
                sendemail(email, subject, msg, true);
            }));
        }
    }
}