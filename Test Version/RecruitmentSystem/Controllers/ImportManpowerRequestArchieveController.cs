using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace RecruitmentSystem.Controllers
{
    public class ImportManpowerRequestArchieveController : AccountManager
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ActionResult ImportManpowerRequestAchieveIndex()
        {
            return View(db.sp_archieved_list().ToList());
        }
        [HttpPost]
        public ActionResult ImportManpowerRequestAchieveIndex(HttpPostedFileBase file)
        {
            string filename = Server.MapPath("~/excel/manpower-archieved-" + DateTime.Now.ToString("MM-dd-yy_mm-hh-ss-tt") + ".xlsx");
            file.SaveAs(filename);
            var dt = ExcelConnection.Datasource("select * from sheet", filename);
            List<RecruitmentSystem.Models.ImportArchieve.ManpowerArchieve> list = new List<Models.ImportArchieve.ManpowerArchieve>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0] == null || dr[0].ToString() == "")
                {
                    break;
                }
                list.Add(new Models.ImportArchieve.ManpowerArchieve()
                {
                    TempMRFID = dr[0].ToString(),
                    DateRequested = Tools.ToDateTime(dr[1]),
                    DateofDeployment = Tools.ToDateTime(dr[2]),
                    RequiredPosition = dr[3].ToString(),
                    RequiredNumber = Tools.ToInteger(dr[4]),
                    EducationAttainment = dr[5].ToString(),
                    Course = dr[6].ToString(),
                    Gender = dr[7].ToString(),
                    AgeRequirement = dr[8].ToString(),
                    SkillType = dr[9].ToString(),
                    SpecificSkill = dr[10].ToString(),
                    Certification = dr[11].ToString(),
                    CostCenter = dr[12].ToString(),
                    Department = dr[13].ToString(),
                    JobDescription = dr[14].ToString(),
                    SalaryDetails = dr[15].ToString(),
                    Basicpay = Tools.ToDecimal(dr[16].ToString()),
                    Cola = Tools.ToDecimal(dr[17].ToString()),
                    Skilled = Tools.ToDecimal(dr[18].ToString()),
                    Meal = Tools.ToDecimal(dr[19].ToString()),
                    Transportation = Tools.ToDecimal(dr[20].ToString()),
                    Gas = Tools.ToDecimal(dr[21].ToString()),
                    Communication = Tools.ToDecimal(dr[22].ToString()),
                    Motorcycle = Tools.ToDecimal(dr[23].ToString()),
                    Clothing = Tools.ToDecimal(dr[24].ToString()),
                    Medical = Tools.ToDecimal(dr[25].ToString()),
                    PayoutDate = dr[26].ToString(),
                    WhoToLookFor = dr[27].ToString(),
                    Establishment = dr[28].ToString(),
                    LocationofDeployment = dr[29].ToString(),
                    OfficeAddresstoReport = dr[30].ToString(),
                    BusinessUnit = dr[31].ToString(),
                    Classification = dr[32].ToString(),
                    CompanyRequested = dr[33].ToString(),
                    Requestor = dr[34].ToString(),
                    RequestorContactNumber = dr[35].ToString(),
                    RequestorEmailAddress = dr[36].ToString(),
                    Recruiter = UserManager.FindByEmail(dr[38].ToString()).Id,
                    Coordinator = UserManager.FindByEmail(dr[39].ToString()).Id,
                    RecruitmentSupervisor = UserManager.FindByEmail("myasuncion@topserve.com.ph").Id,
                    DateCompleted = Tools.ToDateTimeNull(dr[40]),
                    Status = (Tools.ToDateTimeNull(dr[40]) == null ? "Inviting Applicants" : "Completed"),
                    AccountManager = UserManager.FindByEmail(dr[37].ToString()).Id,
                    Pending = Convert.ToInt32(dr[41]),
                    Closed = Convert.ToInt32(dr[42]),
                    Cancel = Convert.ToInt32(dr[43]),
                    OnProcess = Convert.ToInt32(dr[44])
                });

            }
            foreach (var i in list)
            {
                db.sp_import_mrf_request_archieve(i.Coordinator, i.TempMRFID, i.DateRequested, i.DateofDeployment, i.RequiredPosition, i.RequiredNumber, i.EducationAttainment, i.Course, i.Gender, i.AgeRequirement, i.SkillType, i.SpecificSkill, i.Certification, i.CostCenter, i.Department, i.JobDescription, i.SalaryDetails, i.Basicpay, i.Cola, i.Skilled, i.Meal, i.Transportation, i.Gas, i.Communication, i.Motorcycle, i.Clothing, i.Medical, i.PayoutDate, i.WhoToLookFor, i.Establishment, i.OfficeAddresstoReport, i.LocationofDeployment, i.BusinessUnit, i.Classification, i.CompanyRequested, i.Requestor, i.RequestorContactNumber, i.RequestorEmailAddress, i.Recruiter, i.RecruitmentSupervisor, i.DateCompleted, i.Status, i.AccountManager, i.Cancel);
                for (var a = 1; a <= i.Closed; a++)
                {
                    db.sp_import_applicant_archieve(i.TempMRFID, Guid.NewGuid().ToString(), 1);
                }
                for (var a = 1; a <= i.OnProcess; a++)
                {
                    db.sp_import_applicant_archieve(i.TempMRFID, Guid.NewGuid().ToString(), 0);
                }

            }
            return RedirectToAction("ImportManpowerRequestAchieveIndex");
        }
        [HttpPost]
        public ActionResult ImportApplicantArchieve(HttpPostedFileBase fileapplicant)
        {
            string path = Server.MapPath("~/excel/applicant-archieve" + new Random().Next(1, 1000000) + ".xlsx");
            fileapplicant.SaveAs(path);
            var dt = ExcelConnection.Datasource("select * from sheet", path);
            List<RecruitmentSystem.Models.ImportArchieve.ApplicantArchieve> list = new List<Models.ImportArchieve.ApplicantArchieve>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0] == null || dr[0].ToString() == "")
                {
                    break;
                }
                list.Add(new Models.ImportArchieve.ApplicantArchieve()
                {
                    DummyId = dr[0].ToString(),
                    TempId = dr[1].ToString()
                });

            }
            foreach (var i in list)
            {
                db.sp_import_applicant_archieve(i.TempId, i.DummyId, 1);
            }
            return RedirectToAction("ImportManpowerRequestAchieveIndex");
        }
    }
}