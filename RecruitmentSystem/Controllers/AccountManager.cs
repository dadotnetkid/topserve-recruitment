using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using RecruitmentSystem.Models;
using RecruitmentSystem;
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;

namespace RecruitmentSystem.Controllers
{
    public class AccountManager : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }
        public AccountManager()
        {

        }
        public AccountManager(ApplicationUserManager usermanager, ApplicationSignInManager signinmanager)
        {
            SignInManager = signinmanager;
            UserManager = usermanager;
        }
        ApplicationDbContext context = new ApplicationDbContext();
        public ApplicationUserManager _userManager;
        public ApplicationSignInManager _signInManager;
        private RoleManager<IdentityRole> _rolemanager;
        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            }
            set
            {
                _rolemanager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public async Task<SignInStatus> SignInAsync(string username, string password, Boolean rememberme, Boolean lockout = false)
        {
            var res = await SignInManager.PasswordSignInAsync(username, password, rememberme, lockout);

            return res;
        }
        public async Task<bool> SignInAsync(string username, string password, bool rememberme = false)
        {
            bool login = false;
            try
            {
                ApplicationUser user = new ApplicationUser();
                user = await UserManager.FindByEmailAsync(username);
                if (user == null)
                {
                    user = await UserManager.FindByNameAsync(username);
                }
                user = await UserManager.FindAsync(user.UserName, password);

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, false, true);
                    login = true;
                }
            }
            catch (Exception)
            {
                login = false;
            }
            return login;
        }
        public async Task<IdentityResult> RegisterAsync(UserViewModel model)
        {
            var db = new DatabaseModelDataContext();
            IdentityResult res = new IdentityResult();
            string username = model.email.Split('@')[0].ToString();
            var user = new ApplicationUser() { Email = model.email, UserName = username };
            var exist = false;
          /*   if (model.userlevel != "Recruitment Supervisor")
            {
                exist = false;
            }
           else if (model.userlevel == "Recruitment Supervisor" && db.fn_check_supervisor_exist_by_branch(model.branch) <= 0)
            {
                exist = false;
            }
            else if (db.fn_check_supervisor_exist_by_branch(model.branch) > 0 && model.userlevel == "Recruitment Supervisor")
            {
                exist = true;
            }*/
            if (exist == false)
            {
                res = await UserManager.CreateAsync(user, model.password);
                if (res.Succeeded)
                {
                    res = await UserManager.AddToRoleAsync(user.Id, model.userlevel);
                    if (res.Succeeded)
                    {
                        db.sp_register_user(user.Id, model.fname, model.mname, model.lname, model.gender, model.email, model.contact_number, model.office_address, model.company_id, model.position_name, model.account_manager_1, model.account_manager_2, model.industry_id, model.branch, User.Identity.GetUserId());
                        EmailSender email = new EmailSender();
                        await email.sendemail("test.smtp.hsgsoftware@gmail.com", string.Format("Password of  {0} for Topserve Recruitment System", model.email), string.Format("Your account for Topserve Recruitment System has been successfully created, here is your login details:<br /><br /> <strong>Username is {0} </strong><br /><strong>Password is {1} </strong><br /><br /><br /><br />This is a system-generated email, please do not reply.", model.email, model.password));
                        await email.sendemail(model.email, string.Format("Password of  {0} for Topserve Recruitment System", model.email), string.Format("Your account for Topserve Recruitment System has been successfully created, here is your login details:<br /><br /> <strong>Username is {0}</strong><br /><strong>Password is {1} </strong><br /><br /><br /><br />This is a system-generated email, please do not reply.", model.email, model.password));
                    }
                    foreach (var i in db.sp_admin_list().ToList())
                    {
                        db.sp_add_notification(User.Identity.GetUserId(), i.UserId, "Successfully Created Account for " + model.email, "");
                    }
                }
            }
            else
            {
                res = new IdentityResult("This user is already being used.");
            }
            return res;
        }
        public async Task<IdentityResult> createasyncrole(string role)
        {
            var res = await RoleManager.CreateAsync(new IdentityRole() { Name = role });
            return res;
        }
        public void logoff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
        public async Task<IdentityResult> forgotpassword(string userid)
        {
            var token = await UserManager.GeneratePasswordResetTokenAsync(userid);
            var password =  Tools.GeneratePassword();
            var res = await UserManager.ResetPasswordAsync(userid, token, password);
            if (res.Succeeded)
            {
                await new EmailSender().sendemail(userid, "Topserve Issue Tracking System password reset", "Your password has been reset, here is your new password <strong>" + password + "</strong><br /><br /><br /><br />This is a system-generated email, please do not reply.");
            }
            return res;
        }
        public async Task<IdentityResult> changepassword(string password, string userid)
        {
            var token = await UserManager.GeneratePasswordResetTokenAsync(userid);
            var res = await UserManager.ResetPasswordAsync(userid, token, password);
            return res;
        }
    }
}