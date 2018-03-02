using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
namespace RecruitmentSystem.Recruitment.Class
{
    public class Manpower
    {
        public static string Classification(string classification)
        {
            string retval = "";
            if (classification == "NEW")
            {
                retval = "New";
            }
            else if (classification == "FT")
            {
                retval = "New - Fixed Term";
            }
            else if (classification == "PB")
            {
                retval = "New - Project Term";
            }
            else if (classification == "Replacement")
            {
                retval = "Replacement";
            }
            else if (classification == "Reliever")
            {
                retval = "Reliever";
            }
            else if (classification == "On Call")
            {
                retval = "On Call";
            }
            else
            {
                retval = classification;
            }
            return retval;
        }
        public static String ConvertClassification(string classification)
        {
            string retval = "";
            if (classification == "FT")
            {
                retval = "Fixed Term";
            }
            else if (classification == "PB")
            {
                retval = "Project Based";
            }
            else
            {
                retval = classification;
            }
            return retval;
        }
        public static bool CheckCancelManpower(string mrfid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            return (bool)db.fn_check_cancel_mrf(mrfid);
        }
        public static bool CheckRejectedManpower(string mrfid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            return (bool)db.fn_check_rejected_manpower(mrfid);
        }
        public static String GetManpowerClassification(string mrfid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            return db.fn_get_manpower_classification(mrfid);
        }
        public static void AddAuditTrail(string userid, string Trail, string mrfid = "")
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            db.sp_add_audit_trail(userid, Trail, mrfid);
        }
        public static string GetIndustryID(string industryname)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var industry = db.fn_get_industry_id(industryname);
            return (industry == null) ? "" : industry;
        }
        public static string DashboardHomePanelClass()
        {
            var User = HttpContext.Current.User;
            var ClassName = "";
            if (User.IsInRole("Recruiter"))
            {
                ClassName = "col-lg-6";
            }
            else
            {
                ClassName = "col-lg-4";
            }
            return ClassName;
        }
        public static bool CheckManpowerCompleted(string mrfid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            var i = db.sp_manpower_detail(mrfid).FirstOrDefault();
            var total = i.RequiredNumber - (i.cancel_number_requirement + i.accepted);

            var retval = false;
            if (total <= 0 && i.date_completed == null && i.Classification == "Continuous" && i.RequiredNumber != 0)
            {
                retval = true;
            }
            else if (total <= 0 && i.date_completed == null && i.Classification != "Continuous" )
            {
                retval = true;
            }
            return retval;
        }
    }
}