using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Recruitment.Data
{
    public class Dropdown_list_data
    {


        public static List<SelectListItem> PositionList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_positions_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Text = i.position_name,
                    Value = i.position_id
                });
            }
            return list;
        }
        public static List<SelectListItem> EducationalAttainmentList()
        {
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "Post Graduate", Value = " Post Graduate" });
            list.Add(new SelectListItem { Text = "College Graduate", Value = "College Graduate" });
            list.Add(new SelectListItem { Text = "College Level", Value = "College Level" });
            list.Add(new SelectListItem { Text = "Graduate of Certificate Course", Value = "Graduate of Certificate Course" });
            list.Add(new SelectListItem { Text = "Graduate of Vocational Course", Value = "Graduate of Vocational Course" });
            list.Add(new SelectListItem { Text = "High School Graduate", Value = "High School Graduate" });
            list.Add(new SelectListItem { Text = "Elementary Graduate", Value = "Elementary Graduate" });
            return list;
        }
        public static List<SelectListItem> GenderList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Male", Value = "M" });
            list.Add(new SelectListItem { Text = "Female", Value = "F" });
            list.Add(new SelectListItem { Text = "M/F", Value = "M/F" });
            return list;
        }

        public static List<SelectListItem> Gender()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Male", Value = "M" });
            list.Add(new SelectListItem { Text = "Female", Value = "F" });

            return list;
        }
        public static List<SelectListItem> HowDidYouKnowTopserve()
        {
            var list = new List<SelectListItem>();
            foreach (var i in "News Paper,Brochures,Walk-in,Social Media".Split(','))
            {
                list.Add(new SelectListItem { Text = i, Value = i });
            }


            return list;
        }
        public static List<SelectListItem> SkillTypeList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "NS", Text = "Non-Skilled" });
            list.Add(new SelectListItem { Value = "S", Text = "Skilled" });
            list.Add(new SelectListItem { Value = "SW", Text = "Skilled w/ Certificate" });
            return list;
        }
        public static List<SelectListItem> SalaryDetailList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "Hourly Rated", Text = "Hourly Rated" });
            list.Add(new SelectListItem { Value = "Daily Rated", Text = "Daily Rated" });
            list.Add(new SelectListItem { Value = "Monthly Rated", Text = "Monthly Rated" });
            list.Add(new SelectListItem { Value = "Piece Rated", Text = "Piece Rated" });
            return list;
        }
        public static List<SelectListItem> PayoutDateList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "20th-5th", Text = "20th-5th" });
            list.Add(new SelectListItem { Value = "22th-7th", Text = "22th-7th" });
            list.Add(new SelectListItem { Value = "25th-10th", Text = "25th-10th" });
            list.Add(new SelectListItem { Value = "30th/31st (end of the month)-15th", Text = "30th/31st (end of the month)-15th" });
            list.Add(new SelectListItem { Value = "Monday of the following week", Text = "Monday of the following week" });
            list.Add(new SelectListItem { Value = "Tuesday of the following week", Text = "Tuesday of the following week" });
            list.Add(new SelectListItem { Value = "Friday  of the following week", Text = "Friday  of the following week" });
            list.Add(new SelectListItem { Value = "5th-20th", Text = "5th-20th" });
            list.Add(new SelectListItem { Value = "10th-25th", Text = "10th-25th" });
            return list;
        }
        public static List<SelectListItem> BusinessUnitList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "TOPSERVE", Text = "TOPSERVE" });
            list.Add(new SelectListItem { Value = "IWSC", Text = "IWSC" });
            list.Add(new SelectListItem { Value = "CSI", Text = "CSI" });
            return list;
        }
        public static List<SelectListItem> ClassificationList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "NEW", Text = "New" });
            list.Add(new SelectListItem { Value = "Replacement", Text = "Replacement" });
            list.Add(new SelectListItem { Value = "On Call", Text = "On Call" });
            return list;
        }
        public static List<SelectListItem> EmploymentStatus()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "Probationary", Text = "Probationary" });
            list.Add(new SelectListItem { Value = "Fixed Term", Text = "Fixed Term" });
            list.Add(new SelectListItem { Value = "Project Based", Text = "Project Based" });
            list.Add(new SelectListItem { Value = "Reliever", Text = "Reliever" });
            list.Add(new SelectListItem { Value = "Continuous", Text = "Continuous" });
            list.Add(new SelectListItem { Value = "Seasonal", Text = "Seasonal" });
            list.Add(new SelectListItem { Value = "One Time Placement", Text = "One Time Placement" });
            return list;
        }
        public static List<SelectListItem> branchlist()
        {
            var db = new DatabaseModelDataContext();
            var list = db.sp_branch_list("", true).ToList();
            var sl = new List<SelectListItem>();
            foreach (var i in list)
            {
                sl.Add(new SelectListItem() { Text = i.branch_name, Value = i.branch_id });

            }
            return sl;
        }
        public static List<SelectListItem> LocationList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_location_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Text = i.location_name,
                    Value = i.location_id
                });
            }
            return list;
        }
        public static List<SelectListItem> CourseList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_course_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Text = i.course_name,
                    Value = i.course_id
                });
            }
            return list;
        }
        public static List<SelectListItem> CourseApplicant()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_course_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Text = i.course_name,
                    Value = i.course_name
                });
            }
            return list;
        }
        public static List<SelectListItem> CourseListSearch()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_course_list_search())
            {
                if (i.course != "" && i.course != null)
                {
                    list.Add(new SelectListItem
                    {
                        Text = i.course,
                        Value = i.course
                    });
                }

            }
            return list;
        }
        public static List<SelectListItem> CompanyList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_company_list("", true).ToList().OrderBy(m => m.company_name))
            {
                list.Add(new SelectListItem
                {
                    Value = i.company_id,
                    Text = i.company_name + "-" + i.branch + "-" + i.brand
                });
            }
            return list;
        }
        public static List<SelectListItem> SkillList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_skills_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Text = i.skill_name,
                    Value = i.skill_id
                });
            }
            return list;
        }
        public static List<SelectListItem> SkillApplicant()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_skills_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Text = i.skill_name,
                    Value = i.skill_name
                });
            }
            return list;
        }
        public static List<SelectListItem> CertificateApplicant()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_certificates_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Value = i.certificate_name,
                    Text = i.certificate_name
                });
            }
            return list;
        }
        public static List<SelectListItem> CertificateList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_certificates_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Value = i.certificate_id,
                    Text = i.certificate_name
                });
            }
            return list;
        }
        public static List<SelectListItem> IndustryList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_industry_list("", true))
            {
                list.Add(new SelectListItem()
                {
                    Text = i.industry_name,
                    Value = i.industry_id

                });
            }
            return list;
        }
        public static List<SelectListItem> OICRecruiterList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_oic_recruiter_list().ToList())
            {
                list.Add(new SelectListItem()
                {
                    Text = i.oic_recruiter_name + "-" + i.branch_name,
                    Value = i.user_id

                });
            }
            return list;
        }
        public static List<SelectListItem> RecruiterList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_recruiter_list().ToList())
            {
                list.Add(new SelectListItem()
                {
                    Text = i.recruiter_name + "-" + i.branch_name,
                    Value = i.user_id

                });
            }
            return list;
        }
        public static List<SelectListItem> RecruiterListByBranch(string CoordinatorId)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_recruiter_list_by_branch(CoordinatorId).ToList())
            {
                list.Add(new SelectListItem()
                {
                    Text = i.recruiter_name + "-" + i.branch_name,
                    Value = i.user_id

                });
            }
            return list;
        }
        public static List<SelectListItem> CoordinatorList()
        {
            var db = new DatabaseModelDataContext();
            var list = db.sp_coordinator_list("").ToList();
            var sl = new List<SelectListItem>();
            foreach (var i in list)
            {
                sl.Add(new SelectListItem() { Text = i.fullname, Value = i.user_id });
            }
            return sl;
        }
        public static List<SelectListItem> religionlist()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_religion_list().ToList())
            {
                list.Add(new SelectListItem()
                {
                    Text = i.religion,
                    Value = i.religion
                });
            } return list;
        }
        public static List<SelectListItem> Religion()
        {
            var religions = "AGLIPAYAN,APC,BAPTIST,BORN AGAIN,BORN AGAIN CHRISTIAN,CATHOLIC,CHRISTIAN,IGLESIA NI CRISTO,ISLAM,JEHOVAH'S WITNESS,ROMAN CATHOLIC,SEVENTH DAY ADVENTIST,UCCP";
            var list = new List<SelectListItem>();
            foreach (var i in religions.Split(','))
            {
                list.Add(new SelectListItem()
                {
                    Text = i,
                    Value = i
                });
            }
            return list;
        }
        public static List<SelectListItem> position_search()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            var _list = new List<sp_position_searchResult>();
            foreach (var i in db.sp_position_search().ToList())
            {
                foreach (var p in i.requiredposition.Split(','))
                {
                    _list.Add(new sp_position_searchResult()
                    {
                        requiredposition = p
                    });
                }

            }
            var l = _list.GroupBy(m => m.requiredposition).ToList();
            foreach (var i in l.ToList())
            {
                list.Add(new SelectListItem() { Text = i.Key, Value = i.Key });
            }
            return list;
        }

        public static List<SelectListItem> skill_search()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_skills_list_search().ToList())
            {
                list.Add(new SelectListItem
                {
                    Text = i.skills,
                    Value = i.skills
                });
            }
            return list;
        }
        public static List<SelectListItem> recruitment_supervisor_list()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_get_recruitment_supervisor_list().ToList())
            {
                list.Add(new SelectListItem
                {
                    Text = i.RecruitmentSupervisorName,
                    Value = i.UserId
                });
            }
            return list;
        }
        public static List<SelectListItem> certification_search()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_certificates_list("", true))
            {
                list.Add(new SelectListItem
                {
                    Value = i.certificate_name,
                    Text = i.certificate_name
                });
            }
            return list;
        }
        public static List<SelectListItem> schoollist()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_school_list())
            {
                list.Add(new SelectListItem
                {
                    Text = i.schoolname,
                    Value = i.schoolname
                });
            }
            return list;
        }
        public static List<SelectListItem> Location_search()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_location_list_search().ToList())
            {
                list.Add(new SelectListItem
                {
                    Text = i.location,
                    Value = i.location
                });
            }
            return list;
        }
        public static List<SelectListItem> AccountManagerList()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_account_manager_list().ToList())
            {
                list.Add(new SelectListItem()
                {
                    Text = i.account_manager,
                    Value = i.user_id
                });
            }
            return list;
        }
        public static List<SelectListItem> Yearly()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            for (int i = DateTime.Now.Year; i > DateTime.Now.AddYears(-10).Year; i--)
            {
                list.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
            return list;
        }
        public static List<SelectListItem> Monthly()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i)
                });
            }
            return list;
        }
        public static List<SelectListItem> SearchDateType()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Daily", Value = "Daily" });
            list.Add(new SelectListItem() { Text = "Monthly", Value = "Monthly" });
            list.Add(new SelectListItem() { Text = "Yearly", Value = "Yearly" });
            return list;
        }
        public static List<SelectListItem> Status()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Completed", Value = "Completed" });
            list.Add(new SelectListItem() { Text = "For AM Approval", Value = "For AM Approval" });
            list.Add(new SelectListItem() { Text = "For Recruitment Supervisor Review", Value = "For Recruitment Supervisor Review" });
            list.Add(new SelectListItem() { Text = "For Recruiter Allocation", Value = "For Recruiter Allocation" });
            list.Add(new SelectListItem() { Text = "Inviting Applicants", Value = "Inviting Applicants" });
            return list;
        }
        public static List<SelectListItem> HolidayYear()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = new List<SelectListItem>();
            foreach (var i in db.sp_holiday_year().ToList())
            {
                list.Add(new SelectListItem()
                {
                    Value = i.holiday_year.ToString(),
                    Text = i.holiday_year.ToString()
                });
            }
            return list;
        }
        public static List<SelectListItem> SearchByList()
        {
            var list = new List<SelectListItem>();
            foreach (var i in new string[] { "Daily","Monthly","Yearly" })
            {
                list.Add(new SelectListItem() { Text = i, Value = i });
            }
            return list;
        }
        public static List<SelectListItem> TATStatusList()
        {
            var list = new List<SelectListItem>();
            foreach (var i in new string[] { "On-Time", "Late", "Overdue" })
            {
                list.Add(new SelectListItem() { Text = i, Value = i });
            }
            return list;
        }
    }

}
