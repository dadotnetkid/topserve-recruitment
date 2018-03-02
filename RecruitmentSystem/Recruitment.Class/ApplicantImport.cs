using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.Recruitment.Data;
namespace RecruitmentSystem.Recruitment.Class
{
    public class ApplicantImport
    {
        public static List<sp_applicant_listResult> ImportApplicantList(string filename)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            DataTable dt = ExcelConnection.Datasource("select * from sheet ", filename);
            List<sp_applicant_listResult> list = new List<sp_applicant_listResult>();
            SmsBlaster sms = new SmsBlaster();
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (dr[0].ToString() == null || dr[0].ToString() == "")
                    {
                        break;
                    }
                    if (db.fn_check_applicant_duplicate(dr[9].ToString(), dr[12].ToString(), dr[11].ToString(), dr[10].ToString(), dr[13].ToString(), dr[1].ToString(), dr[2].ToString(), dr[0].ToString()) == 0)
                    {
                        list.Add(new sp_applicant_listResult()
                        {
                            surname = dr[0].ToString(),
                            firstname = dr[1].ToString(),
                            middleinitial = dr[2].ToString(),
                            address = dr[3].ToString(),
                            location = dr[4].ToString(),
                            requiredposition = dr[5].ToString(),
                            birthday = Tools.ToDateTime(dr[6]),
                            age = DateTime.Now.Year - Tools.ToDateTime(dr[6]).Year,
                            gender = dr[7].ToString(),
                            religion = dr[8].ToString(),
                            sssnumber = dr[9].ToString(),
                            philhealth = dr[10].ToString(),
                            pagibignumber = dr[11].ToString(),
                            tinnumber = dr[12].ToString(),
                            contactnumber = dr[13].ToString(),
                            emailaddress = dr[14].ToString(),
                            educationattainment = dr[15].ToString(),
                            schoolname = dr[16].ToString(),
                            course = dr[17].ToString(),
                            certification = dr[18].ToString(),
                            skills = dr[19].ToString(),
                            jobfair = dr[20].ToString(),
                            howdidyouknowtopserve = dr[26].ToString(),
                            referrals = dr[27].ToString(),
                            relativesworkingintopserve = dr[28].ToString(),
                            relativeworkingindirectcompetitionoftopserve = dr[29].ToString(),
                            invited = dr[30].ToString(),
                            dateinvited = dr[31].ToString(),
                            oncall = Tools.ToBoolean(dr[32]),
                            reliever = Tools.ToBoolean(dr[33])

                        });
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            System.IO.File.Delete(filename);
            return list;
        }
        public static void ImportConfirmedInvitedApplicant(string filename, string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            DataTable dt = ExcelConnection.Datasource("select * from sheet", filename);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[9].ToString().ToLower() == "y" || dr[9].ToString().ToLower() == "yes")
                {
                    db.sp_import_confirm_applicant(dr[0].ToString(), dr[1].ToString());
                }
            }
            System.IO.File.Delete(filename);
        }
        public static async void ImportConfirmedApplicant(string filename, string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            SmsBlaster sms = new SmsBlaster();
            DataTable dt = ExcelConnection.Datasource("select * from sheet", filename);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[9].ToString().ToLower() == "y" || dr[9].ToString().ToLower() == "yes")
                {
                    db.sp_import_shortlist_applicant(dr[0].ToString(), dr[1].ToString());
                    if (dr[10].ToString().ToLower() == "y" || dr[10].ToString().ToLower() == "yes")
                    {
                        await sms.SendBlastShortlistImport(userid, dr[1].ToString(), false);
                    }
                }
            }
            System.IO.File.Delete(filename);
        }
        public static async void ImportShortlistApplicant(string filename, string userid)
        {
            SmsBlaster sms = new SmsBlaster();
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            DataTable dt = ExcelConnection.Datasource("select * from sheet", filename);


            foreach (DataRow dr in dt.Rows)
            {
                if (dr[9].ToString().ToLower() == "y" || dr[9].ToString().ToLower() == "yes")
                {
                    db.sp_import_for_requirement_applicant(dr[0].ToString(), dr[1].ToString());
                    if (dr[10].ToString().ToLower() == "y" || dr[10].ToString().ToLower() == "yes")
                    {
                        await sms.SendBlastShortlistImport(userid, dr[1].ToString(), false);
                    }

                }
            }

            System.IO.File.Delete(filename);
        }
        public static async void ImportForRequirementApplicant(string filename, string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            SmsBlaster sms = new SmsBlaster();
            DataTable dt = ExcelConnection.Datasource("select * from sheet", filename);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[19].ToString().ToLower() == "y" || dr[19].ToString().ToLower() == "yes")
                {
                    db.sp_applicant_requirement(dr[1].ToString(), Tools.YesToBoolean(dr[3]), Tools.YesToBoolean(dr[4]), Tools.YesToBoolean(dr[5]), Tools.YesToBoolean(dr[6]), Tools.YesToBoolean(dr[7]), Tools.YesToBoolean(dr[8]), Tools.YesToBoolean(dr[9]), Tools.YesToBoolean(dr[10]), Tools.YesToBoolean(dr[11]), Tools.YesToBoolean(dr[12]), Tools.YesToBoolean(dr[13]), Tools.YesToBoolean(dr[14]), dr[15].ToString(), dr[16].ToString(), dr[17].ToString(), dr[18].ToString());
                    db.sp_import_accept_applicant(dr[0].ToString(), dr[1].ToString());
                    if (dr[20].ToString().ToLower() == "y" || dr[20].ToString().ToLower() == "yes")
                    {
                        await sms.SendBlastShortlistImport(userid, dr[1].ToString(), false);
                    }

                }
            }
            System.IO.File.Delete(filename);
        }
        public static void ImportPassOnApplicant(string filename, string Mrfid, string userid)
        {
            DatabaseModelDataContext db = new DatabaseModelDataContext();
            DataTable dt = ExcelConnection.Datasource("select * from sheet", filename);
            List<sp_applicant_listResult> list = new List<sp_applicant_listResult>();
            foreach (DataRow dr in dt.Rows.Cast<DataRow>())
            {
                if (dr[0].ToString() == "")
                {
                    break;
                }
                list.Add(new sp_applicant_listResult()
                {
                    surname = dr[0].ToString(),
                    firstname = dr[1].ToString(),
                    middleinitial = dr[2].ToString(),
                    address = dr[3].ToString(),
                    location = dr[4].ToString(),
                    requiredposition = dr[5].ToString(),
                    birthday = Tools.ToDateTime(dr[6]),
                    age = DateTime.Now.Year - Tools.ToDateTime(dr[6]).Year,
                    gender = dr[7].ToString(),
                    religion = dr[8].ToString(),
                    sssnumber = dr[9].ToString(),
                    philhealth = dr[10].ToString(),
                    pagibignumber = dr[11].ToString(),
                    tinnumber = dr[12].ToString(),
                    contactnumber = "63" + dr[13].ToString(),
                    emailaddress = dr[14].ToString(),
                    educationattainment = dr[15].ToString(),
                    schoolname = dr[16].ToString(),
                    course = dr[17].ToString(),
                    certification = dr[18].ToString(),
                    skills = dr[19].ToString(),
                    jobfair = dr[20].ToString(),
                    howdidyouknowtopserve = dr[26].ToString(),
                    referrals = dr[27].ToString(),
                    relativesworkingintopserve = dr[28].ToString(),
                    relativeworkingindirectcompetitionoftopserve = dr[29].ToString(),
                    invited = dr[30].ToString(),
                    dateinvited = dr[31].ToString(),
                    oncall = Tools.ToBoolean(dr[32]),
                    reliever = Tools.ToBoolean(dr[33])
                });
            }
            if (list.Count > 0)
            {
                foreach (var i in list)
                {
                    // db.sp_import_applicant_list(i.surname, i.firstname, i.middleinitial, i.address, i.requiredposition, i.location, i.birthday, i.age, i.gender, i.religion, i.sssnumber, i.philhealth, i.pagibignumber, i.tinnumber, i.contactnumber, i.emailaddress, i.educationattainment, i.schoolname, i.course, i.certification, i.skills, i.jobfair, i.howdidyouknowtopserve, i.referrals, i.relativesworkingintopserve, i.relativeworkingindirectcompetitionoftopserve, i.invited, Tools.ToDateTime(i.dateinvited), i.oncall, i.reliever);
                    //    db.sp_import_for_requirement_applicant(Mrfid, i.applicant_id);
                    if (db.fn_check_applicant_duplicate(i.sssnumber, i.tinnumber, i.pagibignumber, i.philhealth, i.contactnumber,i.firstname,i.middleinitial,i.surname) <= 0)
                    {
                        db.sp_import_passon_applicant(Mrfid, i.surname, i.firstname, i.middleinitial, i.address, i.requiredposition, i.location, i.birthday, i.age, i.gender, i.religion, i.sssnumber, i.philhealth, i.pagibignumber, i.tinnumber, i.contactnumber, i.emailaddress, i.educationattainment, i.schoolname, i.course, i.certification, i.skills, i.jobfair, i.howdidyouknowtopserve, i.referrals, i.relativesworkingintopserve, i.relativeworkingindirectcompetitionoftopserve, i.invited, Tools.ToDateTime(i.dateinvited), i.oncall, i.reliever);
                    }
                }
            }

            System.IO.File.Delete(filename);
        }
    }
}