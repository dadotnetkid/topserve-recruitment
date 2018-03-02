using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class ManpowerStatusSummaryViewModel
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();

        public string accountmanager { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public DateTime? datefrom { get; set; }
        public DateTime? dateto { get; set; }
        public string BranchId { get; set; }
        public List<sp_manpower_status_summaryResult> ManpowerStatusSummary()
        {
            var list = new List<sp_manpower_status_summaryResult>();

            if (datefrom != null && dateto != null)
            {
                list = db.sp_manpower_status_summary(ConvertionHelper.NulltoEmptyString(accountmanager), ConvertionHelper.NulltoEmptyString(BranchId), datefrom, dateto).ToList();

            }
            return (list);
        }
        public List<sp_manpower_status_summary_new_per_total_requirementResult> ManpowerStatusSummaryNewPerTotalRequirement()
        {
            var list = new List<sp_manpower_status_summary_new_per_total_requirementResult>();
            if (datefrom != null && dateto != null)
            {
                list = db.sp_manpower_status_summary_new_per_total_requirement(ConvertionHelper.NulltoEmptyString(accountmanager), ConvertionHelper.NulltoEmptyString(BranchId), datefrom, dateto).ToList();
            }
            return list;
        }
        public List<sp_manpower_status_summary_closed_per_total_requirementResult> ManpowerStatusSummaryClosedPerTotalRequirement()
        {
            var list = new List<sp_manpower_status_summary_closed_per_total_requirementResult>();
            if (datefrom != null && dateto != null)
            {
                list = db.sp_manpower_status_summary_closed_per_total_requirement(ConvertionHelper.NulltoEmptyString(accountmanager), ConvertionHelper.NulltoEmptyString(BranchId), datefrom, dateto).ToList();
            }
            return (list);
        }
        public List<sp_manpower_status_summary_newreplacementcancelled_per_total_requirementResult> ManpowerStatusSummaryNewReplacementCancelledPerTotalRequirement()
        {
            var list = new List<sp_manpower_status_summary_newreplacementcancelled_per_total_requirementResult>();
            if (datefrom != null && dateto != null)
            {
                list = db.sp_manpower_status_summary_newreplacementcancelled_per_total_requirement(ConvertionHelper.NulltoEmptyString(accountmanager), ConvertionHelper.NulltoEmptyString(BranchId), datefrom, dateto).ToList();
            }
            return (list);
        }
        public List<sp_manpower_status_summary_Oncall_per_total_requirementResult> ManpowerStatusSummaryOncallPerTotalRequirement()
        {
            var list = new List<sp_manpower_status_summary_Oncall_per_total_requirementResult>();
            if (datefrom != null && dateto != null)
            {
                list = db.sp_manpower_status_summary_Oncall_per_total_requirement(ConvertionHelper.NulltoEmptyString(accountmanager), ConvertionHelper.NulltoEmptyString(BranchId), datefrom, dateto).ToList();
            }
            return (list);
        }
    }
}