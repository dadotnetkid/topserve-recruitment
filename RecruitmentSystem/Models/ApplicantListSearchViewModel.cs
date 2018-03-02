using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Models
{
    public class ApplicantListSearchViewModel
    {
        public ApplicantListSearchViewModel()
        {
            Position = "";
            Gender = "";
            Location = "";
            EducationalAttainment = "";

            skills = "";
            Certification = "";
            schoolname = "";
            course = "";
            religion = "";
        }
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        [Required(ErrorMessage = "this field is required")]
        public string Position { get; set; }

        public string Gender { get; set; }

        public string Location { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public string EducationalAttainment { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public int agerequirementto { get; set; }

        [Required(ErrorMessage = "this field is required")]
        public int agerequirementfrom { get; set; }


        public string skills { get; set; }

        public string Certification { get; set; }

        public string schoolname { get; set; }

        public string course { get; set; }

        public string religion { get; set; }

    }
}