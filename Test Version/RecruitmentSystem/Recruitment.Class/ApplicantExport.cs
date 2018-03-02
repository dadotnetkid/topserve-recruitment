using ClosedXML.Excel;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Recruitment.Class
{
    public class ApplicantExport
    {
        XLWorkbook wb = new XLWorkbook();
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        #region RecruitmentProcess
         public MemoryStream ExportInvitedApplicant(string mrfid)
        {
            MemoryStream ms = new MemoryStream();
            var list = db.sp_export_invited_applicant(mrfid);
            var dt = ExportApplicantTemplate();
            foreach (var i in list)
            {
                dt.Rows.Add(i.mrfid, i.applicant_id, i.applicant_name, i.age, i.educationattainment, i.schoolname, i.course, i.certification, i.skills, "");
            }
            wb.Worksheets.Add(dt);
            wb.SaveAs(ms);
            return ms;
        }
        public MemoryStream ExportConfirmedApplicant(string mrfid)
        {
            MemoryStream ms = new MemoryStream();
            var list = db.sp_export_confirmed_applicant(mrfid);
            var dt = ExportApplicantTemplate();
            foreach (var i in list)
            {
                dt.Rows.Add(i.mrfid, i.applicant_id, i.applicant_name, i.age, i.educationattainment, i.schoolname, i.course, i.certification, i.skills, "");
            }
            wb.Worksheets.Add(dt);
            wb.SaveAs(ms);
            return ms;
        }
        public MemoryStream ExportShortlistApplicant(string mrfid)
        {
            MemoryStream ms = new MemoryStream();
            var list = db.sp_export_shortlist_applicant(mrfid);
            var dt = ExportApplicantTemplate();
            foreach (var i in list)
            {
                dt.Rows.Add(i.mrfid, i.applicant_id, i.applicant_name, i.age, i.educationattainment, i.schoolname, i.course, i.certification, i.skills, "");
            }
            wb.Worksheets.Add(dt);
            wb.SaveAs(ms);
            return ms;
        }
        public MemoryStream ExportForRequirementApplicant(string mrfid)
        {
            MemoryStream ms = new MemoryStream();
            var list = db.sp_export_for_requirement_applicant(mrfid);
            var dt = ExportApplicantForRequirementTemplate();
            foreach (var i in list)
            {
                dt.Rows.Add(i.mrfid, i.applicant_id, i.applicant_name, convert(i.nbi), convert(i.birthcertificate), convert(i.diploma), convert(i.marriage),
                 convert( i.certifcateofemployment),   convert( i.medicalexamination),convert( i.basic5), convert(i.basic7), convert(i.basic3), convert(i.drugtest), 
                   convert(i.dependents_birth_certificate), convert(i.resume), i.sss, i.philhealth, i.pagibig, i.tin, "");
            }
            wb.Worksheets.Add(dt);
            wb.SaveAs(ms);
            return ms;
        }
        public MemoryStream ExportAcceptedApplicant(string mrfid)
        {
            MemoryStream ms = new MemoryStream();
            var list = db.sp_export_accepted_applicant(mrfid);
            var dt = ExportAcceptedTemplate();
            foreach (var i in list)
            {
                dt.Rows.Add(mrfid,i.applicant_id,i.surname,i.firstname,i.middleinitial,i.sss,i.pagibig,i.philhealth,i.tin);
            }
            wb.Worksheets.Add(dt);
            wb.SaveAs(ms);
            return ms;
        }
        #endregion
        #region ReserveApplicant
      
        #endregion
        #region Datatable
        DataTable ExportApplicantForRequirementTemplate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Applicant";
            dt.Columns.Add("MRFID");
            dt.Columns.Add("APPLICANT ID");
            dt.Columns.Add("APPLICANT NAME");
            dt.Columns.Add("NBI");
            dt.Columns.Add("BIRTH CERTIFICATE");
            dt.Columns.Add("DIPLOMA");
            dt.Columns.Add("MARRIAGE");
            dt.Columns.Add("CERTIFICATE OF EMPLOYMENT");
            dt.Columns.Add("MEDICAL EXAMINATION");
            dt.Columns.Add("BASIC 5");
            dt.Columns.Add("BASIC 7");
            dt.Columns.Add("BASIC 3");
            dt.Columns.Add("DRUG TEST");
            dt.Columns.Add("DEPENDENTS BIRTH CERTIFICATE");
            dt.Columns.Add("RESUME");
            dt.Columns.Add("SSS");
            dt.Columns.Add("PHILHEALTH");
            dt.Columns.Add("PAGIBIG");
            dt.Columns.Add("TIN");
            dt.Columns.Add("PASSED(Y/N)");
            dt.Columns.Add("SEND SMS(Y/N)");
            return dt;
        }
        DataTable ExportApplicantTemplate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Applicant";
            dt.Columns.Add("MRFID");
            dt.Columns.Add("APPLICANT ID");
            dt.Columns.Add("APPLICANT NAME");
            dt.Columns.Add("AGE");
            dt.Columns.Add("EDUCATIONAL ATTAINMENT");
            dt.Columns.Add("SCHOOL");
            dt.Columns.Add("COURSE");
            dt.Columns.Add("CERTIFICATION");
            dt.Columns.Add("SKILLS");
            dt.Columns.Add("PASSED(Y/N)");
            dt.Columns.Add("Send Sms(Y/N)");
            return dt;
        }
        DataTable ExportAcceptedTemplate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "PLACEMENT REPORT - " + DateTime.Now.ToString("MM-dd-yyyy");
            dt.Columns.Add("MRFID");
            dt.Columns.Add("APPLICANT ID");
            dt.Columns.Add("LAST NAME");
            dt.Columns.Add("FIRST NAME");
            dt.Columns.Add("MIDDLE INITIAL");
            dt.Columns.Add("SSS NO");
            dt.Columns.Add("PAGIBIG NO");
            dt.Columns.Add("PHILHEALTH NO");
            dt.Columns.Add("TIN NO");
            dt.Columns.Add("Send Sms(Y/N)");
            return dt;
        }
        string convert(int ? boolean)
        {
            return (boolean == 1) ? "Y" : "N";
        }
        #endregion
       
    }
}