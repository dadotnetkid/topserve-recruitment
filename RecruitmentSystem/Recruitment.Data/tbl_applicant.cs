//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecruitmentSystem.Recruitment.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_applicant
    {
        public int id { get; set; }
        public string applicant_id { get; set; }
        public string surname { get; set; }
        public string firstname { get; set; }
        public string middleinitial { get; set; }
        public string address { get; set; }
        public string requiredposition { get; set; }
        public string location { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public Nullable<int> age { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public string sssnumber { get; set; }
        public string philhealth { get; set; }
        public string pagibignumber { get; set; }
        public string tinnumber { get; set; }
        public string contactnumber { get; set; }
        public string emailaddress { get; set; }
        public string educationattainment { get; set; }
        public string schoolname { get; set; }
        public string course { get; set; }
        public string certification { get; set; }
        public string skills { get; set; }
        public string jobfair { get; set; }
        public string howdidyouknowtopserve { get; set; }
        public string referrals { get; set; }
        public string relativesworkingintopserve { get; set; }
        public string relativeworkingindirectcompetitionoftopserve { get; set; }
        public string invited { get; set; }
        public string dateinvited { get; set; }
        public Nullable<bool> oncall { get; set; }
        public Nullable<bool> reliever { get; set; }
        public Nullable<System.DateTime> date_time_imported { get; set; }
    }
}
