using RecruitmentSystem.Recruitment.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RecruitmentSystem.Recruitment.Data;
using System.Threading.Tasks;
namespace RecruitmentSystem.Controllers
{
    public class ImportManpowerRequestController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();

        string generateurl(string emailaddress)
        {
            var url = "http://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("manpowerdetail", "manpoweremailnotification", new { email = emailaddress });
            return url;
        }
        [HttpPost]
        public async Task<ActionResult> ImportManpowerRequest()
        {
            ImportManpower import = new ImportManpower();
            var am = db.sp_get_account_manager_id(User.Identity.GetUserId()).FirstOrDefault();

            var files = Request.Files[0];
            var filename = Tools.RandomFileName(files);
            string path = Server.MapPath("~/excel/" + filename);
            files.SaveAs(path);

            ErrorHandlers errorHandlers = await import.ImportManpowerRequest(path, User.Identity.GetUserId(), generateurl(am.account_manager_1), generateurl(am.account_manager_2));
            return RedirectToAction("dashboard", "manpower", errorHandlers);

        }
        string Path()
        {
            var files = Request.Files[0];
            var filename = Tools.RandomFileName(files);
            string path = Server.MapPath("~/excel/" + filename);
            files.SaveAs(path);
            return path;
        }
    }
}