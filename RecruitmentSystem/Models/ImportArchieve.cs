using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class ImportArchieve
    {
        public class ManpowerArchieve
        {

            public string TempMRFID { get; set; }
            public DateTime DateRequested { get; set; }
            public DateTime DateofDeployment { get; set; }
            public string RequiredPosition { get; set; }
            public int RequiredNumber { get; set; }
            public string EducationAttainment { get; set; }
            public string Course { get; set; }
            public string Gender { get; set; }
            public string AgeRequirement { get; set; }
            public string SkillType { get; set; }
            public string SpecificSkill { get; set; }
            public string Certification { get; set; }
            public string CostCenter { get; set; }
            public string Department { get; set; }
            public string JobDescription { get; set; }
            public string SalaryDetails { get; set; }
            public decimal Basicpay { get; set; }
            public decimal Cola { get; set; }
            public decimal Skilled { get; set; }
            public decimal Meal { get; set; }
            public decimal Transportation { get; set; }
            public decimal Gas { get; set; }
            public decimal Communication { get; set; }
            public decimal Motorcycle { get; set; }
            public decimal Clothing { get; set; }
            public decimal Medical { get; set; }
            public string PayoutDate { get; set; }
            public string WhoToLookFor { get; set; }
            public string Establishment { get; set; }
            public string OfficeAddresstoReport { get; set; }
            public string LocationofDeployment { get; set; }
            public string BusinessUnit { get; set; }
            public string Classification { get; set; }
            public string CompanyRequested { get; set; }
            public string Requestor { get; set; }
            public string RequestorContactNumber { get; set; }
            public string RequestorEmailAddress { get; set; }
            public string Coordinator { get; set; }
            public string Recruiter { get; set; }
            public string RecruitmentSupervisor { get; set; }
            public DateTime? DateCompleted { get; set; }
            public string Status { get; set; }
            public string AccountManager { get; set; }
            public int OnProcess { get; set; }
            public int Pending { get; set; }
            public int Cancel { get; set; }
            public int Closed { get; set; }
        }
        public class ApplicantArchieve
        {
            public string DummyId { get; set; }
            public string TempId { get; set; }
        }
    }
}