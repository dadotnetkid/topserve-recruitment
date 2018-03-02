using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class ImportManpowerRequestViewModel
    {
        public string mrfid { get; set; }
        public DateTime? DateRequested { get; set; }
        public DateTime? DateofDeployment { get; set; }
        public string Position { get; set; }
        public int? RequiredNumber { get; set; }
        public string EducationalAttainment { get; set; }
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
        public Decimal? BasicPay { get; set; }
        public Decimal? COLA { get; set; }
        public Decimal? Skilled { get; set; }
        public Decimal? Meal { get; set; }
        public Decimal? Transportation { get; set; }
        public Decimal? Gas { get; set; }
        public Decimal? Communication { get; set; }
        public Decimal? Motorcycle { get; set; }
        public Decimal? Clothing { get; set; }
        public Decimal? Medical { get; set; }
        public string PayoutDate { get; set; }
        public string Whotolook { get; set; }
        public string Establishment { get; set; }
        public string Officeaddresstoreport { get; set; }
        public string LocationofDeployment { get; set; }
        public string BusinessUnit { get; set; }
        public string Classification { get; set; }
        public string EmploymentStatus { get; set; }
        public string ProjectName { get; set; }
        public string ClientRequested { get; set; }
        public string Branch { get; set; }
        public string Brand { get; set; }
        public string Requestor { get; set; }
        public string RequestorContactNumber { get; set; }
        public string RequestorEmailAddress { get; set; }

    }
}