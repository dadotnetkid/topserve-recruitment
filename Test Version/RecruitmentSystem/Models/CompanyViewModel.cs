using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RecruitmentSystem.Recruitment.Data;
using System.Data;
using RecruitmentSystem.Recruitment.Class;
namespace RecruitmentSystem.Models
{
    public class CompanyViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public string CompanyId { get; set; }
        [Required]
        public string company_name { get; set; }
        [Required]
        public string branch { get; set; }
        [Required]
        public string brand { get; set; }
        [Required]
        public string officeaddress { get; set; }
        [Required]
        public string industry { get; set; }
        public string Search { get; set; }
        public ErrorHandlers ErrorHandler { get; set; }




        public List<sp_company_listResult> CompanyList()
        {
            var list = db.sp_company_list(Search, true).ToList();
            return list;
        }
        public sp_company_detailResult CompanyDetail()
        {
            return db.sp_company_detail(CompanyId).FirstOrDefault();
        }
        public void UpdateCompany()
        {
            db.sp_update_company_detail(CompanyId, company_name, branch, brand, industry, officeaddress);
        }
        public ErrorHandlers ImportCompany(DataTable dt)
        {
            ErrorHandlers ErrorHandler = null;
            List<CompanyViewModel> list = new List<CompanyViewModel>();
            string Message = "Imported file is empty";
            string ClassName = "alert alert-danger";
            bool error = true;
            int row = 1;
            foreach (DataRow dr in dt.Rows)
            {
                CompanyViewModel model = new CompanyViewModel()
                {
                    company_name = Tools.ToTitleCase(dr[0].ToString()),
                    branch = Tools.ToTitleCase(dr[1].ToString()),
                    brand = Tools.ToTitleCase(dr[2].ToString()),
                    officeaddress = Tools.ToTitleCase(dr[3].ToString()),
                    industry = dr[4].ToString()
                };

                if (model.company_name == "")
                {
                    Message = "Client Name* field is required at row " + row;
                    ClassName = "alert alert-danger";
                    error = true;
                    break;
                }
                if (model.branch == "")
                {
                    Message = "Branch* field is required at row " + row;
                    ClassName = "alert alert-danger";
                    error = true;
                    break;
                }

                if (model.officeaddress == "")
                {
                    Message = "Office Address* field is required at row " + row;
                    ClassName = "alert alert-danger";
                    error = true;
                    break;
                }
                if (model.industry == "")
                {
                    Message = "Industry* field is required at row " + row;
                    ClassName = "alert alert-danger";
                    error = true;
                    break;
                }
                var IndustryName = Manpower.GetIndustryID(model.industry);
                if (IndustryName == "")
                {
                    Message = "Invalid entry on the Industry field at row " + row;
                    ClassName = "alert alert-danger";
                    error = true;
                    break;
                }
                else
                {
                    model.industry = IndustryName;
                }
                row++;
                list.Add(model);
            }
            if (error == false && list.Count() > 0)
            {

                foreach (var i in list)
                {
                    db.sp_add_companies(i.company_name, i.branch, i.branch, i.officeaddress, i.industry);
                }
                Message = "Successfully Import Client Information";
                ClassName = "alert alert-success";
            }
            ErrorHandler = new ErrorHandlers() { Message = Message, _Class = ClassName };
            //Manpower.GetIndustryID()
            return ErrorHandler;
        }
    }
}