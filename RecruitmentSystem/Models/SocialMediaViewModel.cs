using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecruitmentSystem.Recruitment.Class;
namespace RecruitmentSystem.Models
{
    public class SocialMediaViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public string mrfid { get; set; }
        public string ManpowerDetails()
        {
            var list = db.sp_manpower_detail(mrfid).ToList();
            var retval="";
            foreach(var i in list)
            {
                retval = string.Format("Apply Now! \nJob Position:{0}, \n Education Attainment:{1}, \n Age Required:{2}, \n Gender:{3},\n Location of Deployment:{6},  \n Recruiter Name:{4}, \n Recruiter Contact Number:{5}", i.position_name, i.EducationalAttainment, i.AgeRequirement, i.Gender, Users.Fullname(i.recruiter_id), i.recruiternumber, i.location_name);
            }
            return retval;
        }
    }
}