using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using RecruitmentSystem.Recruitment.Data;
using RecruitmentSystem.Recruitment.Report.Contracts.FixedPeriod;
using RecruitmentSystem.Recruitment.Report.Contracts.ProjectBased;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using RecruitmentSystem.Recruitment.Report.Contracts.Corestaff;
namespace RecruitmentSystem.Recruitment.Report.Contracts
{
    public class ReportContract
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public Stream Contract(string mrfid, string applicants, string classification)
        {
            Stream stream = null;
            //string classification = db.fn_get_manpower_classification(mrfid);

            switch (classification)
            {
                case "Probationary":
                    stream = ProbitionaryReport(mrfid, applicants);
                    break;
                case "On Call":
                    stream = OncallReport(mrfid, applicants);
                    break;
                case "FT":
                    stream = FixedTermReport(mrfid, applicants);
                    break;
                case "PB":
                    stream = ProjectBasedReport(mrfid, applicants);
                    break;
                case "NEW":
                    stream = ProbitionaryReport(mrfid, applicants);
                    break;
                case "Seasonal":
                    stream = FixedTermReport(mrfid, applicants);
                    break;
                default:
                    stream = ProbitionaryReport(mrfid, applicants);
                    break;
            }
            return stream;
        }
        Stream OncallReport(string mrfid, string applicants)
        {
            DataTable dt = new Contracts.sp_print_contractDataTable();
            foreach (var applicant in applicants.Split(','))
            {
                foreach (var i in db.sp_print_contract(mrfid, applicant))
                {
                    dt.Rows.Add(i.surname, i.firstname, i.middleinitial, i.address, i.ProjectName, i.BusinessUnit, i.Classification, i.position_name, i.company_name, i.branch, i.officeaddress);
                }
            }
            var BusinessUnit = mrfid.Split('-')[0];

            var report = new ReportDocument();
            if (BusinessUnit == "CSI")
            {
                report = new Corestaff.OnCall.OncallCorestaffA();
            }
            else if (BusinessUnit == "TOPSERVE")
            {
                report = new Oncall.OncallA();
            }
            else if (BusinessUnit == "IWSC")
            {
                report = new IWSC.Oncall.OnCallIWSCA();
            }
                     
            report.SetDataSource(dt);
            return report.ExportToStream(ExportFormatType.WordForWindows);
        }
        Stream ProbitionaryReport(string mrfid, string applicants)
        {
            DataTable dt = new Contracts.sp_print_contractDataTable();
            foreach (var applicant in applicants.Split(','))
            {
                foreach (var i in db.sp_print_contract(mrfid, applicant))
                {
                    dt.Rows.Add(i.surname, i.firstname, i.middleinitial, i.address, i.ProjectName, i.BusinessUnit, i.Classification, i.position_name, i.company_name, i.branch, i.officeaddress);
                }
            }
            var BusinessUnit = mrfid.Split('-')[0];

            var report = new ReportDocument();

            if (BusinessUnit == "CSI")
            {
                report = new Corestaff.Probationary.ProbationaryCorestaffA();
            }
            else if (BusinessUnit == "TOPSERVE")
            {
                report = new Probitionary();
            }
            else if (BusinessUnit == "IWSC")
            {
                report = new IWSC.Probationary.ProbationaryIWSCA();
            }

            report.SetDataSource(dt);


            return report.ExportToStream(ExportFormatType.WordForWindows);
        }
        Stream FixedTermReport(string mrfid, string applicants)
        {
            DataTable dt = new Contracts.sp_print_contractDataTable();
            foreach (var applicant in applicants.Split(','))
            {
                foreach (var i in db.sp_print_contract(mrfid, applicant))
                {
                    dt.Rows.Add(i.surname, i.firstname, i.middleinitial, i.address, i.ProjectName, i.BusinessUnit, i.Classification, i.position_name, i.company_name, i.branch, i.officeaddress);
                }
            }
            var report = new ReportDocument();
            var BusinessUnit = mrfid.Split('-')[0];
            if (BusinessUnit == "TOPSERVE")
            {
                report = new FixedPeriodA();
            }
            else if (BusinessUnit == "CSI")
            {
                report = new Corestaff.FixedPeriod.FixedTermCorestaffA();
            }
            else if (BusinessUnit == "IWSC")
            {
                report = new IWSC.FixedTerm.FixedTermIWSCA();
            }


            report.SetDataSource(dt);

            return report.ExportToStream(ExportFormatType.WordForWindows);
        }
        Stream ProjectBasedReport(string mrfid, string applicants)
        {
            DataTable dt = new Contracts.sp_print_contractDataTable();
            foreach (var applicant in applicants.Split(','))
            {
                foreach (var i in db.sp_print_contract(mrfid, applicant))
                {
                    dt.Rows.Add(i.surname, i.firstname, i.middleinitial, i.address, i.ProjectName, i.BusinessUnit, i.Classification, i.position_name, i.company_name, i.branch, i.officeaddress);
                }
            }
            var report = new ReportDocument();
            var BusinessUnit = mrfid.Split('-')[0];
            if (BusinessUnit == "TOPSERVE")
            {
                report = new ProjectBasedA();
            }
            else if (BusinessUnit == "CSI")
            {
                report = new Corestaff.ProjectBased.ProjectBaseCorestaffA();
            }
            else if (BusinessUnit == "IWSC")
            {
                report = new IWSC.ProjectBased.ProjectBasedIWSCA();
            }
            report.SetDataSource(dt);

            return report.ExportToStream(ExportFormatType.WordForWindows);
        }
    }
}