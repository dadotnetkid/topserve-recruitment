using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using RecruitmentSystem.Models;
using Microsoft.AspNet.Identity;
using PagedList;
namespace RecruitmentSystem.Controllers
{
    public class ApplicantController : Controller
    {
        // GET: Applicant
        DatabaseModelDataContext db = new DatabaseModelDataContext();

        [HttpPost]
        public ActionResult ImportApplicant(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/excel/" + file.FileName));
            var list = ApplicantImport.ImportApplicantList(Server.MapPath("~/excel/" + file.FileName));
            if ((from m in list
                 where m.educationattainment.ToUpper() != ("COLLEGE GRADUATE") &&
                     m.educationattainment.ToUpper() != ("ELEMENTARY GRADUATE") &&
                     m.educationattainment.ToUpper() != ("GRADUATE OF CERTIFICATE COURSE") &&
                     m.educationattainment.ToUpper() != ("GRADUATE OF VOCATIONAL COURSE") &&
                     m.educationattainment.ToUpper() != ("HIGH SCHOOL GRADUATE") &&

                     m.educationattainment.ToUpper() != ("POST GRADUATE") && m.educationattainment.ToUpper() != ("COLLEGE LEVEL")
                 select m).Count() > 0)
            {
                ViewBag.error = ("Invalid Education Attainment found in the file!");
                return View();
            }
            else if ((from m in list
                      where m.age == 0 &&
                            m.age < 18
                      select m).Count() > 0)
            {
                ViewBag.error = ("Invalid Date of Birth found in the file!");
                return View();
            }
            else if (
                (from m in list
                 where m.howdidyouknowtopserve.ToUpper() != ("BROCHURES") &&
                     m.howdidyouknowtopserve.ToUpper() != ("NEWS PAPER") &&
                     m.howdidyouknowtopserve.ToUpper() != ("ONLINE ADVERTISEMENT") &&
                     m.howdidyouknowtopserve.ToUpper() != ("SOCIAL MEDIA") &&
                     m.howdidyouknowtopserve.ToUpper() != ("WALK-IN")
                 select m).Count() > 0
                )
            {
                ViewBag.error = ("Invalid How did you know Topserve in the file!");
                return View();
            }
            else if (
                (from m in list
                 where m.gender.ToUpper() != ("M") &&
                     m.gender.ToUpper() != ("F")
                 select m).Count() > 0
                )
            {
                ViewBag.error = ("Invalid entry on Gender, It should be either M or F");
                return View();
            }
            else if (
               (from m in list
                where m.contactnumber.Length != 10
                select m).Count() > 0
               )
            {
                ViewBag.error = ("Invalid entry on Contact Number, It should be in this format 9123456789");
                return View();
            }
            foreach (var i in list)
            {
                db.sp_import_applicant_list(i.surname, i.firstname, i.middleinitial, i.address, i.requiredposition, i.location, i.birthday, i.age, i.gender, i.religion, i.sssnumber, i.philhealth, i.pagibignumber, i.tinnumber, i.contactnumber, i.emailaddress, i.educationattainment, i.schoolname, i.course, i.certification, i.skills, i.jobfair, i.howdidyouknowtopserve, i.referrals, i.relativesworkingintopserve, i.relativeworkingindirectcompetitionoftopserve, i.invited, Tools.ToDateTime(i.dateinvited), i.oncall, i.reliever);
            }
            foreach (var i in db.sp_admin_list().ToList())
            {
                db.sp_add_notification(User.Identity.GetUserId(), i.UserId, "Successfully uploaded applicant list", "");

            }
            System.IO.File.Delete(Server.MapPath("~/excel/" + file.FileName));
            return View(list);
        }
        [HttpGet]
        public ActionResult ImportApplicant()
        {
            ViewBag.ApplicantsActive = "active";
            List<sp_applicant_listResult> list = new List<sp_applicant_listResult>();
            return View(list);
        }
        public ActionResult ApplicantList(string search = "", int page = 1)
        {
            ViewBag.ApplicantsActive = "active";
            ViewBag.search = search;
            ViewBag.page = page;
            return View();
        }
        public ActionResult _applicantlist(string search = "", int page = 1)
        {
            var list = db.sp_applicant_list(search).ToList();
            //list = (from m in list where (m.firstname + " " + m.middleinitial + "" + m.surname).ToLower().Contains(search.ToLower()) select m).ToList();
            page = (page <= 0) ? 1 : page;
            //ViewBag.page = page;
            //ViewBag.pagesize = list.Count() / 50;
            //ViewBag.search = search;
            return View(list);
        }
        public ActionResult ApplicantDetail(string applicantid, string mrfid)
        {
            ViewBag.ApplicantsActive = "active";
            ApplicantDetailViewModel i = new ApplicantDetailViewModel();
            i.ApplicantID = applicantid;
            return View(i);
        }
        [HttpGet]
        public ActionResult UpdateDetail(string applicantid, string mrfid)
        {
            ViewBag.applicantid = applicantid;
            ViewBag.mrfid = mrfid;
            var i = db.sp_applicant_detail(applicantid).FirstOrDefault();
            var detail = new ApplicantDetailViewModel()
            {
                ApplicantID = i.applicant_id,
                Fname = i.firstname,
                Mname = i.middleinitial,
                Lname = i.surname,
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
                sss = i.sss,
                nbi = Tools.ToBoolean(i.nbi),
                philhealth = i.philhealth,
                birthcertificate = Tools.ToBoolean(i.birthcertificate),
                pagibig = i.pagibig,
                tin = i.tin,
                diploma = Tools.ToBoolean(i.diploma),
                marriage = Tools.ToBoolean(i.marriage),
                dependantsbirthcertificate = Tools.ToBoolean(i.dependents_birth_certificate),
                certificateofemployment = Tools.ToBoolean(i.certifcateofemployment),
                drugtest = Tools.ToBoolean(i.drugtest),
                basic3 = Tools.ToBoolean(i.basic3),
                basic5 = Tools.ToBoolean(i.basic5),
                basic7 = Tools.ToBoolean(i.basic7),
                referredby = i.referrals,
                HowDidYouKnowTopserve = i.howdidyouknowtopserve,
                JobFair = i.jobfair
            };
            return View(detail);
        }
        [HttpPost]
        public ActionResult UpdateDetail(ApplicantDetailViewModel i, string applicantid, string mrfid)
        {
            db.sp_applicant_requirement(applicantid, i.nbi, i.birthcertificate, i.diploma, i.marriage, i.certificateofemployment, false, i.basic5, i.basic7, i.basic3, i.drugtest, i.dependantsbirthcertificate, i.resume, i.sss, i.philhealth, i.pagibig, i.tin);

            return RedirectToAction("ApplicantDetail", "Applicant", new { mrfid = mrfid, applicantid = applicantid });
        }
        [HttpPost]
        public ActionResult UpdateApplicantInformation(ApplicantDetailViewModel i, string applicantid, string mrfid)
        {
            db.sp_update_applicant_detail(applicantid,i.Fname,i.Mname,i.Lname, i.address, i.position, i.birthday, i.religion, i.gender, i.contactnumber, i.emailaddress, i.EducationAttainment, i.Certification,i.Skill,i.HowDidYouKnowTopserve,i.referredby);
            db.sp_applicant_requirement(applicantid, i.nbi, i.birthcertificate, i.diploma, i.marriage, i.certificateofemployment, false, i.basic5, i.basic7, i.basic3, i.drugtest, i.dependantsbirthcertificate, i.resume, i.sss, i.philhealth, i.pagibig, i.tin);
            return RedirectToAction("ApplicantDetail", "Applicant", new { mrfid = mrfid, applicantid = applicantid });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteApplicant(string applicant_id = "")
        {
            db.ExecuteCommand("delete from tbl_applicant where applicant_id={0}", applicant_id);
            return RedirectToAction("ApplicantList");
        }
        [HttpGet]
        public ActionResult ApplicantHistory(string applicant = "")
        {
            var list = db.sp_applicant_history(applicant).ToList();
            return View(list);
        }
        public ActionResult ApplicantRegistration()
        {

            return View(new ApplicantRegistrationViewModel());

        }
        [HttpPost]
        public ActionResult ApplicantRegistration(ApplicantRegistrationViewModel i)
        {
            var position = i.requiredposition.Replace('/', ',');
            var number = "63" + i.contactnumber.Substring(i.contactnumber.Length - 10);
            i.birthday = Convert.ToDateTime(i.month + "/" + i.day + "/" + i.year);
            if (db.fn_check_applicant_duplicate(i.sssnumber, i.tinnumber, i.pagibignumber, i.philhealth, i.contactnumber, i.firstname, i.middleinitial, i.surname) == 0)
            {
                db.sp_import_applicant_list(i.surname, i.firstname, i.middleinitial, i.address, position, i.location, i.birthday, DateTime.Now.Year - Tools.ToDateTime(i.birthday).Year, i.gender, i.religion, i.sssnumber, i.philhealth, i.pagibignumber, i.tinnumber, i.contactnumber, i.emailaddress, i.educationattainment, i.schoolname, i.course, i.certification, i.skills, i.jobfair, i.howdidyouknowtopserve, i.referrals, i.relativesworkingintopserve, i.relativeworkingindirectcompetitionoftopserve, i.invited, DateTime.Now, i.oncall, i.reliever);
            }
            return RedirectToAction("login", "user", new { applicantregistered = "successfully-registered" });
        }
    }
}