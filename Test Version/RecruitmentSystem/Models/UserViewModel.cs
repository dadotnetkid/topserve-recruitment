
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Models
{
    public class UserViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public UserViewModel()
        {
            password = new Tools().GeneratePassword();
        }
        public string userid { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string email { get; set; }
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string password { get; set; }

        [Required(ErrorMessage = "first Name is required")]
        public string fname { get; set; }
        [Required(ErrorMessage = "Middle Name is required")]
        public string mname { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string lname { get; set; }
        [Required(ErrorMessage = "Contact Number is Required")]
        public string contact_number { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Office Address is Required")]
        public string office_address { get; set; }
        [Required(ErrorMessage = "Company is Required")]
        public string company_id { get; set; }
        [Required(ErrorMessage = "Position is Required")]
        public string position_name { get; set; }

        [Required(ErrorMessage = "Account Manager is Required")]
        public string account_manager_2 { get; set; }
        [Required(ErrorMessage = "Account Manager is Required")]
        public string account_manager_1 { get; set; }
        [Required(ErrorMessage = "Industry is Required")]
        public string industry_id { get; set; }

        [Required(ErrorMessage = "Branch is required")]
        public string branch { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public string department { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string userlevel { get; set; }
        public bool IsInRole(params string[] Roles)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = db.sp_user_roles(userid).ToList();
            var retval = false;
            foreach (var i in Roles)
            {
                if ((from m in list where m.Role.Contains(i) select m).Count() > 0)
                {
                    retval = true;
                }
            }
            return retval;
        }

        public List<SelectListItem> Rolelist()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var list = db.Roles.ToList();
            var sl = new List<SelectListItem>();

            foreach (var i in list)
            {
                sl.Add(new SelectListItem() { Text = i.Name, Value = i.Name });

            }
            return sl;
        }
        public List<SelectListItem> industrylist()
        {

            var db = new DatabaseModelDataContext();
            var sl = new List<SelectListItem>();
            var list = db.sp_industry_list("", true).ToList();
            foreach (var i in list)
            {
                sl.Add(new SelectListItem() { Text = i.industry_name, Value = i.industry_id });

            }
            return sl;
        }
        public List<SelectListItem> departmentlist()
        {
            var db = new DatabaseModelDataContext();
            var list = db.sp_department_list("", true).ToList();
            var sl = new List<SelectListItem>();
            foreach (var i in list)
            {
                sl.Add(new SelectListItem() { Text = i.department_name, Value = i.department_id });

            }
            return sl;
        }
        public List<SelectListItem> companylist()
        {
            var db = new DatabaseModelDataContext();
            var list = db.sp_company_list("", true).ToList();
            var sl = new List<SelectListItem>();
            foreach (var i in list)
            {
                sl.Add(new SelectListItem() { Text = i.company_id, Value = i.company_name });

            }
            return sl;
        }
        public List<SelectListItem> branchlist()
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
        public List<SelectListItem> genderlist()
        {

            var sl = new List<SelectListItem>();
            sl.Add(new SelectListItem() { Text = "Male", Value = "Male" });
            sl.Add(new SelectListItem() { Text = "Female", Value = "Female" });

            return sl;
        }
        public List<SelectListItem> account_manager_list()
        {
            var db = new DatabaseModelDataContext();
            var list = db.sp_account_manager_list().ToList();
            var sl = new List<SelectListItem>();
            foreach (var i in list)
            {
                sl.Add(new SelectListItem() { Text = i.account_manager, Value = i.user_id });

            }
            return sl;
        }
    }
    public class forgotpasswordViewModel
    {
        [Remote("checkemail", "validation")]
        [Required]
        public string email { get; set; }
    }
    public class loginviewmodel
    {
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }

}
