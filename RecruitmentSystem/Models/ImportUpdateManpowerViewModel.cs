
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class ImportUpdateManpowerViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ErrorHandlers errorHandlers = new ErrorHandlers();
        public ImportUpdateManpowerViewModel()
        {
            importUpdateManpowerViewModel = new List<ImportUpdateManpowerViewModel>();
        }
        public string mrfid { get; set; }
        public string DateRequested { get; set; }
        public string CompanyName { get; set; }
        public string Branch { get; set; }
        public string PositionName { get; set; }
        public string Classification { get; set; }
        public string EmploymentStatus { get; set; }

        public HttpPostedFileBase file { get; set; }
        public List<ImportUpdateManpowerViewModel> importUpdateManpowerViewModel { get; set; }
        public List<ImportUpdateManpowerViewModel> importUpdateManpower()
        {
            List<ImportUpdateManpowerViewModel> list = new List<ImportUpdateManpowerViewModel>();

            foreach (DataRow dr in Excel().Rows)
            {
                list.Add(new ImportUpdateManpowerViewModel()
                {
                    mrfid = dr[0].ToString(),
                    DateRequested = dr[1].ToString(),
                    CompanyName = dr[2].ToString(),
                    Branch = dr[3].ToString(),
                    PositionName = dr[4].ToString(),
                    Classification = dr[5].ToString(),
                    EmploymentStatus = employmentStatus(dr[6].ToString())
                });
            }
            foreach (var i in list)
            {
                db.sp_import_update_manpower(i.mrfid, i.Classification, i.EmploymentStatus);
            }
            return list;
        }
        DataTable Excel()
        {
            var server = HttpContext.Current.Server;
            var extensionname = Path.GetExtension(file.FileName);
            var filename = server.MapPath("~/excel/" + new Random().Next(0, 1000000) + extensionname);
            file.SaveAs(filename);
            return new ExcelConnection().DataSourceTable("MRFID,[Date Requested],[Company Name],Branch,[Position Name],Classification,[Employment Status]", filename);
        }
        string employmentStatus(string status)
        {
            switch (status.ToLower())
            {
                case "ft":
                    status = "Fixed Term";
                    break;
                case "pb":
                    status = "Project Based";
                    break;
                default:
                    break;
            }
            return status;
        }
    }
}