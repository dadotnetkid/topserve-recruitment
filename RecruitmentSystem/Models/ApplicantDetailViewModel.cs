using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Models
{
    public class ApplicantDetailViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public string mrfid { get; set; }
        public string ApplicantID { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string ApplicantFullname { get; set; }
        public string address { get; set; }
        public string position { get; set; }
        public string location { get; set; }
        public string religion { get; set; }
        public string gender { get; set; }
        [EmailAddress]
        public string emailaddress { get; set; }
        public DateTime birthday { get; set; }
        public int age { get; set; }
        public string course { get; set; }
        public string school { get; set; }
        public string JobFair { get; set; }

        public string Certification { get; set; }
        public string HowDidYouKnowTopserve { get; set; }
        [MinLength(12)]
        [MaxLength(12)]
        public string contactnumber { get; set; }

        public bool resume { get; set; }
        public bool nbi { get; set; }
        [MaxLength(10, ErrorMessage = "10 Digit")]
        [MinLength(10, ErrorMessage = "10 Digit")]
        [Required]
        public string sss { get; set; }
        [MinLength(12, ErrorMessage = "12 Digit")]
        [MaxLength(12, ErrorMessage = "12 Digit")]
        [Required]
        public string philhealth { get; set; }
        public bool birthcertificate { get; set; }
        [MinLength(10, ErrorMessage = "10 Digit")]
        [MaxLength(12, ErrorMessage = "12 Digit")]
        [Required]
        public string pagibig { get; set; }
        [MinLength(10, ErrorMessage = "10 Digit")]
        [MaxLength(12, ErrorMessage = "10 Digit")]
        [Required]
        public string tin { get; set; }
        public bool diploma { get; set; }
        public bool marriage { get; set; }
        public bool dependantsbirthcertificate { get; set; }
        public bool certificateofemployment { get; set; }
        public bool drugtest { get; set; }
        public bool basic3 { get; set; }
        public bool basic5 { get; set; }
        public bool basic7 { get; set; }
        public string referredby { get; set; }
        public string EducationAttainment { get; set; }
        public string Skill { get; set; }
        public ApplicantDetailViewModel ApplicantDetail()
        {
        
            var i = db.sp_applicant_detail(ApplicantID).FirstOrDefault();
            var detail = new ApplicantDetailViewModel()
            {
                ApplicantID = i.applicant_id,
                ApplicantFullname = i.applicant_fullname,
                address = i.address,
                position = i.requiredposition,
                location = i.location,
                birthday = (DateTime)i.birthday,
                religion = i.religion,
                age = (int)i.age,
                gender = i.gender,
                contactnumber = i.contactnumber,
                emailaddress = i.emailaddress,
                EducationAttainment = i.educationattainment,
                resume = Tools.ToBoolean(i.resume),
                sss = (i.sss == "" || i.sss == null) ? "N/A" : i.sss,
                nbi = Tools.ToBoolean(i.nbi),
                philhealth = (i.philhealth == "" || i.philhealth == null) ? "N/A" : i.philhealth,
                birthcertificate = Tools.ToBoolean(i.birthcertificate),
                pagibig = (i.pagibig == "" || i.pagibig == null) ? "N/A" : i.pagibig,
                tin = (i.tin == "" || i.tin == null) ? "N/A" : i.tin,
                diploma = Tools.ToBoolean(i.diploma),
                marriage = Tools.ToBoolean(i.marriage),
                dependantsbirthcertificate = Tools.ToBoolean(i.dependents_birth_certificate),
                certificateofemployment = Tools.ToBoolean(i.certifcateofemployment),
                drugtest = Tools.ToBoolean(i.drugtest),
                basic3 = Tools.ToBoolean(i.basic3),
                basic5 = Tools.ToBoolean(i.basic5),
                basic7 = Tools.ToBoolean(i.basic7),
                course = i.course,
                school = i.schoolname,
                HowDidYouKnowTopserve=i.howdidyouknowtopserve,
                JobFair=i.jobfair


            };
            return detail;
        }
    }
    public class ApplicantRegistrationViewModel
    {
        public List<SelectListItem> Days()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (var i = 1; i <= 31; i++)
            {
                list.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            return list;
        }
        public List<SelectListItem> Month()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (var i = 1; i <= 12; i++)
            {
                list.Add(new SelectListItem() { Value = i.ToString(), Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i) });
            }
            return list;
        }
        public List<SelectListItem> Year()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (var i = DateTime.Now.Year - 50; i <= DateTime.Now.Year; i++)
            {
                list.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            return list;
        }
        public string applicant_id { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string surname { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string middleinitial { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string address { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string requiredposition { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string location { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public int day { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public int month { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public int year { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public System.Nullable<System.DateTime> birthday { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public System.Nullable<int> age { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string gender { get; set; }
        public string religion { get; set; }

        [MaxLength(10, ErrorMessage = "10 Digit")]
        [MinLength(10, ErrorMessage = "10 Digit")]
        public string sssnumber { get; set; }

        [MinLength(12, ErrorMessage = "12 Digit")]
        [MaxLength(12, ErrorMessage = "12 Digit")]
        public string philhealth { get; set; }
        [MinLength(10, ErrorMessage = "10 Digit")]
        [MaxLength(12, ErrorMessage = "12 Digit")]
        public string pagibignumber { get; set; }
        [MinLength(10, ErrorMessage = "10 Digit")]
        [MaxLength(12, ErrorMessage = "10 Digit")]
        public string tinnumber { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [MinLength(11)]
        [MaxLength(11)]
        public string contactnumber { get; set; }
        [EmailAddress]
        public string emailaddress { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string educationattainment { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string schoolname { get; set; }

        public string course { get; set; }

        public string certification { get; set; }

        public string skills { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string jobfair { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string howdidyouknowtopserve { get; set; }

        public string referrals { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string relativesworkingintopserve { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string relativeworkingindirectcompetitionoftopserve { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string invited { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string dateinvited { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public System.Nullable<bool> oncall { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public System.Nullable<bool> reliever { get; set; }

    }
}