using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentSystem.Recruitment.Class
{
    public class SmsBlaster
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        EmailSender email = new EmailSender();
        public async Task<string> SendSms(string message, string number)
        {
            StreamReader sr = null;
            string retval = "";
            HttpClient client = new HttpClient();
            string result = "";
            HttpResponseMessage msg = null;


            //for (int i = 0; i <= message.Length - 1; i++)
            //{
            //    if (message[i] == 'Y' || message[i] == 'y')
            //    {
            //        message = message.Remove(i, 1);
            //    }
            //}
            message = message.Replace("Y", "%59").Replace("y", "%79").Replace(" ", "+").Replace("&", "%26");
            string url = string.Format("http://www.marketlinkph.com/php/topserve/api/sendsms.php?user=topserve&password=tmsiapi&message={0}&mobile={1}", message, number);

            msg = client.GetAsync(url).Result;
            retval = await msg.Content.ReadAsStringAsync();
            
            return retval;
        }

        public async Task<string> SendBlastInvited(string mrfid, string applicantlist, string userid, bool smssend)
        {
            int sended = 0;
            int applicantcount = 0;
            string res = "";
            foreach (var applicant in applicantlist.Split(','))
            {

                var detail = db.sp_applicant_contact_details(applicant).FirstOrDefault();

                if (smssend == true)
                {
                    var message = invitedmessage(userid);
                    res = await SendSms(message, detail.contactnumber);
                    if (Convert.ToInt64(res) > 0)
                    {
                        db.sp_invite_applicant_applicant_search(mrfid, applicant);
                        sended++;
                    }
                }
                else
                {
                    db.sp_invite_applicant_applicant_search(mrfid, applicant);
                    sended++;
                }
                await Task.Run(new Action(() =>
                {
                    email.InvitedEmailMessage(detail.emailaddress, userid);
                }));

                db.sp_add_applicant_history(applicant, mrfid + " - Invited");
                applicantcount++;
            }

            return "Successfully invited " + sended + " applicant(s) out of " + applicantcount;
        }
        public async Task<string> SendBlastShortlist(string mrfid, string applicantlist, string userid, bool smssend)
        {
            int sended = 0;
            int applicantcount = 0;
            string res = "";
            foreach (var applicant in applicantlist.Split(','))
            {
                try
                {
                    var detail = db.sp_applicant_contact_details(applicant).FirstOrDefault();
                    
                    if (smssend == true)
                    {
                        res = await SendSms(shortlistmessage(userid), detail.contactnumber);
                        if (Convert.ToInt64(res) > 0)
                        {
                            sended++;
                        }
                    }
                    await Task.Run(new Action(() =>
                    {
                        email.ShortlistEmailMessage(detail.emailaddress, userid);
                    }));
                }
                catch (Exception)
                {
                }
                db.sp_add_applicant_history(applicant, mrfid + " - Shortlist");
                applicantcount++;
            }

            return "Successfully Added for Requirement";
        }
        public async Task<string> SendBlastForRequirement(string mrfid, string applicantlist, string userid, bool smssend)
        {
            int sended = 0;
            int applicantcount = 0;
            string res = "";
            foreach (var applicant in applicantlist.Split(','))
            {
                try
                {
                    var detail = db.sp_applicant_contact_details(applicant).FirstOrDefault();
                    if (smssend == true)
                    {
                        res = await SendSms(forrequirementmessage(userid), detail.contactnumber);
                        if (Convert.ToInt64(res) > 0)
                        {

                            sended++;
                        }
                    }
                    await Task.Run(new Action(() =>
                    {
                        email.ForRequirementEmailMessage(detail.emailaddress, userid);
                    }));

                }
                catch (Exception)
                {
                }
                db.sp_add_applicant_history(applicant, mrfid + " - For Requirement");
                applicantcount++;
            }

            return "Successfully Added for Requirement";
        }
        public async Task<string> SendBlastAccepted(string mrfid, string applicantlist, string userid, bool smssend = false)
        {
            int sended = 0;
            int applicantcount = 0;
            string res = "";
            foreach (var applicant in applicantlist.Split(','))
            {
                try
                {
                    var detail = db.sp_applicant_contact_details(applicant).FirstOrDefault();
                    if (smssend == true)
                    {
                        res = await SendSms(Acceptedmessage(userid), detail.contactnumber);
                        if (Convert.ToInt64(res) > 0)
                        {

                            sended++;
                        }
                    }
                    await Task.Run(new Action(() =>
                    {
                        email.AcceptedEmailMessage(detail.emailaddress, userid);
                    }));

                }
                catch (Exception)
                {
                }
                db.sp_add_applicant_history(applicant, mrfid + " - Accepted");
                applicantcount++;
            }

            return "Successfully Added for Requirement";
        }
        public string invitedmessage(string userid)
        {
            var recruiter_detail = db.sp_recruiter_contact_detail(userid).FirstOrDefault();
            var invited = db.sp_sms_detail().FirstOrDefault();

            var sms = invited.invited
                        .Replace("[name]", recruiter_detail.recruiter_name)
                        .Replace("[branch]", recruiter_detail.branch_address)
                        .Replace("[number]", recruiter_detail.contactnumber);
            return sms;
        }
        public string shortlistmessage(string userid)
        {
            var recruiter_detail = db.sp_recruiter_contact_detail(userid).FirstOrDefault();
            var message = db.sp_sms_detail().FirstOrDefault();
            var sms = message.shortlist

                .Replace("[number]", recruiter_detail.contactnumber)
                .Replace("[branch]", recruiter_detail.branch_address)
                .Replace("[name]", recruiter_detail.recruiter_name);

            return sms;
        }
        public string forrequirementmessage(string userid)
        {
            var recruiter_detail = db.sp_recruiter_contact_detail(userid).FirstOrDefault();
            var message = db.sp_sms_detail().FirstOrDefault();
            var sms = message.forrequirement

                .Replace("[number]", recruiter_detail.contactnumber)
                .Replace("[branch]", recruiter_detail.branch_address)
                .Replace("[name]", recruiter_detail.recruiter_name);

            return sms;
        }
        public string Acceptedmessage(string userid)
        {
            var recruiter_detail = db.sp_recruiter_contact_detail(userid).FirstOrDefault();
            var message = db.sp_sms_detail().FirstOrDefault();
            var sms = message.accepted

                .Replace("[number]", recruiter_detail.contactnumber)
                .Replace("[branch]", recruiter_detail.branch_address)
                .Replace("[name]", recruiter_detail.recruiter_name);

            return sms;
        }
        public async Task<string> SendBlastShortlistImport(string userid, string applicant, bool smssend)
        {


            try
            {
                var detail = db.sp_applicant_contact_details(applicant).FirstOrDefault();
                if (smssend == true)
                {
                    await SendSms(shortlistmessage(userid), detail.contactnumber);

                }
                await Task.Run(new Action(() =>
                {
                    email.ShortlistEmailMessage(detail.emailaddress, userid);
                }));
            }
            catch (Exception)
            {
            }
            return "Successfully Added for Requirement";
        }
        public async Task<string> SendBlastForRequirementImport(string userid, string applicant, bool smssend)
        {
            try
            {
                var detail = db.sp_applicant_contact_details(applicant).FirstOrDefault();
                if (smssend == true)
                {
                    SendSms(forrequirementmessage(userid), detail.contactnumber);
                }
                await Task.Run(new Action(() =>
                {
                    email.ForRequirementEmailMessage(detail.emailaddress, userid);
                }));

            }
            catch (Exception)
            {
            }

            return "Successfully Added for Requirement";
        }
        public async Task<string> SendBlastAcceptedImport(string userid, string applicant, bool smssend = false)
        {
            try
            {
                var detail = db.sp_applicant_contact_details(applicant).FirstOrDefault();
                if (smssend == true)
                {
                    SendSms(Acceptedmessage(userid), detail.contactnumber);

                }
                await Task.Run(new Action(() =>
                {
                    email.AcceptedEmailMessage(detail.emailaddress, userid);
                }));

            }
            catch (Exception)
            {
            }
            return "Successfully Added for Requirement";
        }
    }
}