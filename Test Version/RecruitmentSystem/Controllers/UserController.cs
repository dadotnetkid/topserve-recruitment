using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
namespace RecruitmentSystem.Controllers
{
    public class UserController : AccountManager
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var user = UserManager.FindByName("recruiter");
            if (user == null)
            {
                user = new ApplicationUser() { UserName = "recruiter", Email = "recruiter@gmail.com" };
                var res = UserManager.Create(user, "hsgdev");
                if (res.Succeeded)
                {
                    UserManager.AddToRoles(user.Id, "Recruiter");
                }
            }
        }
        [HttpGet]
        public ActionResult Login(string forgotpassword, string returnUrl = "", string applicantregistered = null)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.applicantregistered = applicantregistered;
            if (User.Identity.IsAuthenticated)
            {
                if (returnUrl == "")
                {
                    return RedirectToAction("dashboard", "home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ViewBag.returnUrl = returnUrl;
                ViewBag.forgotpassword = forgotpassword;
                return View(new loginviewmodel());

            }


        }
        [HttpPost]
        public async Task<ActionResult> Login(loginviewmodel model, string returnUrl = "")
        {

            //   var res = await SignInAsync();

            var _login = await SignInAsync(model.username, model.password, true);
            if (_login)
            {
                if (returnUrl != "")
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("dashboard", "home");
                }
            }
            else
            {
                ViewBag.returnUrl = returnUrl;
                ViewBag.login = false;
                return View(model);
            }

            //if (res == Microsoft.AspNet.Identity.Owin.SignInStatus.Success)
            //{
            //    if (returnUrl == "" )
            //    {
            //        return RedirectToAction("dashboard", "home");
            //    }
            //    else
            //    {
            //        return Redirect(returnUrl);
            //    }
            //}
            //else
            //{
            //    ViewBag.returnUrl = returnUrl;
            //    ViewBag.login = false;
            //    return View(model);

            //}
        }

        public ActionResult logout()
        {
            logoff();

            return RedirectToAction("login");

        }
        [HttpGet]
        public ActionResult forgotemail()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> forgotemail(forgotpasswordViewModel model)
        {

            var res = await forgotpassword(model.email);
            if (!res.Succeeded)
            {
                return RedirectToAction("login");
            }
            else
            {
                return RedirectToAction("login", "user", new { @forgotpassword = res.ToString() });
            }
        }
        #region authenticated
        [HttpGet]
        public ActionResult UserList(string email = "")
        {
            ViewBag.user = email;
            ViewBag.SettingActive = "active";
            return View();
        }
        public ActionResult _UserList(string user = "", int page = 1)
        {
            var db = new DatabaseModelDataContext();
            var list = db.sp_user_list(user).ToList();
            page = (page <= 0) ? 1 : page;
            ViewBag.page = page;
            int pagesize = list.Count() / 10;
            ViewBag.pagesize = pagesize;

            //return View(list.Skip(10 * (page-1)).Take(10).ToList());
            return View(list);
        }

        [HttpGet]
        public ActionResult RegisterUser(string userlevel)
        {
            ViewBag.SettingActive = "active";
            UserViewModel model = new UserViewModel() { userlevel = userlevel };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> RegisterUser(UserViewModel model)
        {

            var res = await RegisterAsync(model);
            if (res.Succeeded)
            {
                ViewBag.Success = "Successfully ";
                var email = model.email.Split('@');
                return RedirectToAction("userlist", "user", new { email = email[0] });
            }
            else
            {
                ViewBag.error = res.Errors.FirstOrDefault();
                return View(model);
            }


        }
        [HttpGet]
        public ActionResult Userdetail(string userid)
        {
            ViewBag.SettingActive = "active";
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var list = db.sp_user_details(userid).First();
            return View(list);
        }
        [HttpGet]
        public ActionResult UpdateUser(string userid, string message)
        {
            ViewBag.SettingActive = "active";
            ViewBag.message = message;
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var i = db.sp_user_details(userid).First();
            UserViewModel model = new UserViewModel()
            {
                fname = i.fname,
                mname = i.mname,
                lname = i.lname,
                gender = i.gender,
                contact_number = i.contactnumber,
                branch = i.branch_id,
                account_manager_1 = i.account_manager_1,
                account_manager_2 = i.account_manager_2,
                password = "",
                userid = userid
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateUser(UserViewModel i)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            if (i.password != null)
            {
                var res = await changepassword(i.password, i.userid);
                if (res.Succeeded)
                {
                    db.sp_update_user(i.userid, i.fname, i.mname, i.lname, i.gender, i.contact_number, i.account_manager_1, i.account_manager_2, i.branch);
                    return RedirectToAction("UpdateUser", "user", new { userid = i.userid, message = "Successfully updated the account" });
                }
                else
                {
                    ViewBag.error = res.Errors.FirstOrDefault();
                }
            }
            else
            {
                db.sp_update_user(i.userid, i.fname, i.mname, i.lname, i.gender, i.contact_number, i.account_manager_1, i.account_manager_2, i.branch);
                return RedirectToAction("UpdateUser", new { userid = i.userid, message = "Successfully updated the account" });
            }

            return View(i);

        }

        #endregion
    }
}
