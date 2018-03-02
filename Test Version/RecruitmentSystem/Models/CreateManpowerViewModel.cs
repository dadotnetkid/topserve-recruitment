using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RecruitmentSystem.Models
{
    public class CreateManpowerViewModel
    {
        public CreateManpowerViewModel()
        {
            DateRequested = DateTime.Now;
            DateofDeployment = DateTime.Now;
            RequiredNumber = 0;
        }
        [Required(ErrorMessage = "Date Requested is required")]
        public DateTime DateRequested { get; set; }

        public DateTime DateofDeployment { get; set; }
        [Required(ErrorMessage = "Position is required")]
        public string RequiredPosition { get; set; }
        [Required(ErrorMessage = "Number of Required is required")]

        public int RequiredNumber { get; set; }
        [Required(ErrorMessage = "Education Attainment is required")]
        public string EducationalAttainment { get; set; }
        public string Course { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Age Requirement is required")]
        public string AgeRequirement { get; set; }
        [Required(ErrorMessage = "Skill Type is required")]
        public string SkillType { get; set; }
        public string SpecificSkill { get; set; }
        public string Certification { get; set; }
        public string CostCenter { get; set; }
        public string Department { get; set; }
        [Required(ErrorMessage = "Job Description is required")]
        public string JobDescription { get; set; }
        [Required(ErrorMessage = "Salary Details is required")]
        public string SalaryDetails { get; set; }
        [Required(ErrorMessage = "Basic Pay is required")]
        public decimal BasicPay { get; set; }
        public decimal Cola { get; set; }
        public decimal Skilled { get; set; }
        public decimal Meal { get; set; }
        public decimal Transportation { get; set; }
        public decimal Gas { get; set; }
        public decimal Communication { get; set; }
        public decimal Motorcycle { get; set; }
        public decimal Clothing { get; set; }
        public decimal Medical { get; set; }
        [Required(ErrorMessage = "Payout Date is required")]
        public string PayoutDate { get; set; }
        [Required(ErrorMessage = "Who to look for is required")]
        public string WhotoLookfor { get; set; }
        [Required(ErrorMessage = "Establishment is required")]
        public string Establishment { get; set; }
        [Required(ErrorMessage = "Office address to report is required")]
        public string OfficeAddresstoReport { get; set; }
        [Required(ErrorMessage = "Location of deployment is required")]
        public string LocationofDeployment { get; set; }
        [Required(ErrorMessage = "Business Unit is required")]
        public string BusinessUnit { get; set; }
        [Required(ErrorMessage = "Classification is required")]
        public string Classification { get; set; }
        [Required(ErrorMessage = "Employment Status is required")]
        public string EmploymentStatus { get; set; }
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Company requested is required")]
        public string CompanyRequested { get; set; }
        [Required(ErrorMessage = "Requestor is required")]
        public string Requestor { get; set; }
        [Required(ErrorMessage = "Requestor contact number is required")]
        public string RequestorContactNumber { get; set; }
        public string RequestorEmailAddress { get; set; }


    }
}