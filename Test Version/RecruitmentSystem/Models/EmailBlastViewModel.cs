using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class EmailBlastViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        [Required(ErrorMessage = "This field is required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Message { get; set; }
        public async Task<int> SendEmailBlast()
        {
            EmailSender e = new EmailSender();
            foreach (var i in db.sp_send_email_blast().ToList())
            {
                await e.sendemail(i.emailaddress, Subject, Message);
            }
            return 0;
            //get all list using storedproc
        }
    }
}