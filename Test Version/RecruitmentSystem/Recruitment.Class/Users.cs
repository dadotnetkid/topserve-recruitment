using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using RecruitmentSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
namespace RecruitmentSystem.Recruitment.Class
{
    public class Users
    {
        public static string Role(string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            return db.fn_get_user_role(userid);
        }
        public static string GetUserid()
        {
            var user = HttpContext.Current.User;
            return user.Identity.GetUserId();
        }
        public static Boolean IsInRole(params string[] Roles)
        {
            var user = HttpContext.Current.User;
            var retval = false;
            foreach(var role in Roles)
            {
                if(user.IsInRole(role))
                {
                    retval = true;
                    break;
                }
                
            }
            return retval;
        }
        public static string GetRoleByEmail(string Email)
        {
            var db = new ApplicationDbContext();
            UserManager<ApplicationUser> UserManager;
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user=UserManager.FindByEmail(Email);
            return new DatabaseModelDataContext().fn_get_user_role(user.Id);

        }
        public static string Fullname(string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            return db.fn_get_user_name(userid);
        }
        public static string EmailAddress(string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            return db.fn_get_user_email_address(userid);
        }
        public static int RecruitmentSupervisor()
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            return Convert.ToInt32(db.fn_count_recruitment_supervisor());
        }
        public static Boolean IsInRole(string roles)
        {
            var retval = false;
            foreach (var role in roles.Split(','))
            {
                if (HttpContext.Current.User.IsInRole(role))
                {
                    retval = true;
                    break;
                }
            }
            return retval;
        }
    }
}