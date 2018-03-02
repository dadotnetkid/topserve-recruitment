using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class ManpowerReserveViewModel
    {
        [Required]
        public string RequiredPosition { get; set; }
        [Required]
        public string EducationalAttainment { get; set; }
     
        public string course { get; set; }
      
        public string gender { get; set; }
 
        public string agerequirement { get; set; }
        [Required]
        public string  skilltype { get; set; }

        public string specificskill { get; set; }

        public string certification { get; set; }
        
    }
}