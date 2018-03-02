using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using RecruitmentSystem.Recruitment.Class;
using System.ComponentModel;
using System.Threading.Tasks;
namespace RecruitmentSystem.Models
{
    public class ManpowerDashboardViewModel
    {
        public ManpowerDashboardViewModel()
        {
            User = HttpContext.Current.User;
            Classification = (Classification == null ? "" : Classification);
            BusinessUnit = (BusinessUnit == null ? "" : BusinessUnit);
            Status = (Status == null ? "" : Status);
        }
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ErrorHandlers ErrorHandlers { get; set; }
        public string Search { get; set; }
        [DefaultValue("")]
        public string Classification { get; set; }
        public IPrincipal User { get; set; }
        [DefaultValue("")]
        public string BusinessUnit { get; set; }
        [DefaultValue("")]
        public string Status { get; set; }
        [DefaultValue("")]
        public string Filter { get; set; }

        public List<sp_manpower_listResult> ManpowerRequestList()
        {
            var userid = User.Identity.GetUserId();
            var role = db.fn_get_user_role(userid);
            var list = db.sp_manpower_list(Search, Classification, Status, BusinessUnit, Filter, User.IsInRole("Admin") ? "" : userid, role).ToList();

            return list;
        }
    }
}