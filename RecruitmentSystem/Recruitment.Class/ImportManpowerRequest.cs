using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentSystem.Recruitment.Class
{
    public class ImportManpower
    {


        public async Task<ErrorHandlers> ImportManpowerRequest(string filename, string Userid, string urlam1, string urlam2)
        {

            var dt = ExcelConnection.Datasource("select * from sheet ", filename);
            ErrorHandlers errorHandlers = new ErrorHandlers();

            return await Import(Userid, dt, urlam1, urlam2);

        }
        async Task<ErrorHandlers> Import(string Userid, DataTable dt, string urlam1, string urlam2)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            List<ImportManpowerRequestViewModel> ManpowerList = new List<ImportManpowerRequestViewModel>();
            var am = db.sp_get_account_manager_id(Userid).FirstOrDefault();
            var message = "Imported file is empty";
            var _class = "alert alert-danger";
            bool Error = false;
            ErrorHandlers errorHandlers = new ErrorHandlers();
            int row = 1;
            foreach (DataRow dr in dt.Rows)
            {
                if(dr[0].ToString()=="")
                {
                    break;
                }
                var i = new ImportManpowerRequestViewModel()
                    {

                        DateRequested = Tools.ToDateTimeNull(dr[0]),
                        DateofDeployment = Tools.ToDateTimeNull(dr[1]),
                        Position = dr[2].ToString(),
                        RequiredNumber = Tools.ToIntNull(dr[3]),
                        EducationalAttainment = dr[4].ToString(),
                        Course = dr[5].ToString() == "" ? null : dr[5].ToString(),
                        Gender = dr[6].ToString(),
                        AgeRequirement = dr[7].ToString(),
                        SkillType = dr[8].ToString(),
                        SpecificSkill = dr[9].ToString() == "" ? null : dr[9].ToString(),
                        Certification = dr[10].ToString(),
                        CostCenter = dr[11].ToString(),
                        Department = dr[12].ToString(),
                        JobDescription = dr[13].ToString(),
                        SalaryDetails = dr[14].ToString(),
                        BasicPay = Tools.ToDecimalNull(dr[15]),
                        COLA = Tools.ToDecimalNull(dr[16]),
                        Skilled = Tools.ToDecimalNull(dr[17]),
                        Meal = Tools.ToDecimalNull(dr[18]),
                        Transportation = Tools.ToDecimalNull(dr[19]),
                        Gas = Tools.ToDecimalNull(dr[20]),
                        Communication = Tools.ToDecimalNull(dr[21]),
                        Motorcycle = Tools.ToDecimalNull(dr[22]),
                        Clothing = Tools.ToDecimalNull(dr[23]),
                        Medical = Tools.ToDecimalNull(dr[24]),
                        PayoutDate = dr[25].ToString(),
                        Whotolook = dr[26].ToString(),
                        Establishment = dr[27].ToString(),
                        Officeaddresstoreport = dr[28].ToString(),
                        LocationofDeployment = dr[29].ToString() == "" ? null : dr[29].ToString(),
                        BusinessUnit = dr[30].ToString(),
                        Classification = dr[31].ToString(),
                        EmploymentStatus = dr[32].ToString(),

                        ClientRequested = dr[33].ToString() == "" ? null : dr[33].ToString(),
                        Branch = dr[34].ToString(),
                        Brand = dr[35].ToString(),
                        ProjectName = dr[36].ToString(),
                        Requestor = dr[37].ToString(),
                        RequestorContactNumber = dr[38].ToString(),
                        RequestorEmailAddress = dr[39].ToString(),


                    };
                if (i.BusinessUnit.ToLower() != "topserve" && i.BusinessUnit.ToLower() != "csi" && i.BusinessUnit.ToLower() != "iwsc")
                {
                    message = "Invalid entry on the Business Unit field at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.DateofDeployment == null)
                {
                    message = "Date of Deployment* field is required at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.DateRequested == null)
                {
                    message = "Position* field is required at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.RequiredNumber == null)
                {
                    message = "Required Number* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.EducationalAttainment == "")
                {
                    message = "Educational Attainment* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }

                if (i.Gender == "")
                {
                    message = "Gender* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.AgeRequirement == "")
                {
                    message = "Age Requirement* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.SkillType == "")
                {
                    message = "Skill Type* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.JobDescription == "")
                {
                    message = "Job Description* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.SalaryDetails == "")
                {
                    message = "Salary Details* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.BasicPay == null)
                {
                    message = "Basic Pay* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.PayoutDate == "")
                {
                    message = "Payout Date* field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.Whotolook == "")
                {
                    message = "Who to look * field is required at row " + row; ;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.Establishment == "")
                {
                    message = "Establishment* field is required	at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.Officeaddresstoreport == "")
                {
                    message = "Office address to report* field is required	at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.LocationofDeployment == "")
                {
                    message = "Location of Deployment* field is required at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.BusinessUnit == "")
                {
                    message = "Business Unit* field is required	at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.Classification == "")
                {
                    message = "Classification* field is required at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.EmploymentStatus == "")
                {
                    message = "Employment Status* field is required	at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.ClientRequested == "")
                {
                    message = "Client Requested* field is required	at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.Branch == "")
                {
                    message = "Branch* field is required	at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }

                if (i.Requestor == "")
                {
                    message = "Requestor* field is required	at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.RequestorContactNumber == "")
                {
                    message = "Requestor Contact Number* field is required at row" + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.Classification != "On Call" && i.Classification != "New" && i.Classification != "Replacement")
                {
                    message = "Invalid entry on the Classification field at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }



              
                i.Position = db.fn_get_position_id(i.Position);
                i.LocationofDeployment = db.fn_get_location_id(i.LocationofDeployment);
                i.ClientRequested = db.fn_get_company_id_import(i.ClientRequested, i.Branch, i.Brand);
                if (i.Position == "" || i.Position == null)
                {
                    message = "Invalid entry on the Position field at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.Course != "" && i.Course != null)
                {
                    i.Course = db.fn_get_course_id(i.Course);
                    if (i.Course == "" || i.Course == null)
                    {
                        message = "Invalid entry on the Course field at row " + row;
                        _class = "alert alert-danger";
                        Error = true;
                        break;
                    }
                }
                if (i.SpecificSkill != "" && i.SpecificSkill != null)
                {
                    i.SpecificSkill = db.fn_get_skills_id(i.SpecificSkill);
                    if (i.SpecificSkill == "" || i.SpecificSkill == null)
                    {
                        message = "Invalid entry on the Specific Skill fieldat row " + row;
                        _class = "alert alert-danger";
                        Error = true;
                        break;
                    }
                }

                if (i.LocationofDeployment == "" || i.LocationofDeployment == null)
                {
                    message = "Invalid entry on the Location of Deployment field at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }
                if (i.ClientRequested == "" || i.ClientRequested == null)
                {
                    message = "Invalid entry on the Client Requested field at row " + row;
                    _class = "alert alert-danger";
                    Error = true;
                    break;
                }

                row++;
                ManpowerList.Add(i);



                //db.sp_create_mrf_request(Userid,mrfid,Tools.ToDateTime( dr[0]),Tools.ToDateTime( dr[1]),);
            }
            row = 1;
            if (Error == false && ManpowerList.Count() > 0)
            {
                foreach (var i in ManpowerList)
                {

                    var businessunit = i.BusinessUnit;
                    var mrfid = db.fn_generate_mrf_id(Userid, businessunit).ToUpper();
                    db.sp_create_mrf_request(Userid, mrfid, i.DateRequested, i.DateofDeployment, i.Position, i.RequiredNumber, i.EducationalAttainment, i.Course, i.Gender, i.AgeRequirement, i.SkillType, i.SpecificSkill, i.Certification, i.CostCenter, i.Department, i.JobDescription, i.SalaryDetails, i.BasicPay, i.COLA, i.Skilled, i.Meal, i.Transportation, i.Gas, i.Communication, i.Motorcycle, i.Clothing, i.Medical, i.PayoutDate, i.Whotolook, i.Establishment, i.Officeaddresstoreport, i.LocationofDeployment, i.BusinessUnit, i.Classification, i.EmploymentStatus, i.ProjectName, i.ClientRequested, i.Requestor, i.RequestorContactNumber, i.RequestorEmailAddress);


                    EmailSender email = new EmailSender();
                    await email.EmailNotificationImport(urlam1, mrfid, am.account_manager_1);
                    await email.EmailNotificationImport(urlam2, mrfid, am.account_manager_1);
                    //Notification
                    var User_fullname = Users.Fullname(Userid);
                    db.sp_add_notification(Userid, am.account_manager_1, string.Format("{0} sent {1} to {2} for approval", User_fullname, mrfid, Users.Fullname(am.account_manager_1)), mrfid);
                    db.sp_add_notification(Userid, am.account_manager_2, string.Format("{0} sent {1} to {2} for approval", User_fullname, mrfid, Users.Fullname(am.account_manager_2)), mrfid);
                    //notify admins
                    foreach (var admin in db.sp_admin_list().ToList())
                    {
                        db.sp_add_notification(Userid, admin.UserId, string.Format("{0} sent {1} to {2} for approval", User_fullname, mrfid, Users.Fullname(am.account_manager_1)), mrfid);
                        db.sp_add_notification(Userid, admin.UserId, string.Format("{0} sent {1} to {2} for approval", User_fullname, mrfid, Users.Fullname(am.account_manager_2)), mrfid);
                    }
                }
                message = "Successfully Imported MRF(s)";
                _class = "alert alert-success";
            }

            errorHandlers.Message = message;
            errorHandlers._Class = _class;
            return errorHandlers;

        }


    }
}