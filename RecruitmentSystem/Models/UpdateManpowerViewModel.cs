using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class UpdateManpowerViewModel
    {
        public string mrfid { get; set; }
        public DateTime DateDeployment { get; set; }
        public DateTime  DateRequested { get; set; }
        public string position { get; set; }
        public string agerequirement { get; set; }
        public string educationattainment { get; set; }
        public string course { get; set; }
        public string gender { get; set; }
        public int numberofrequired { get; set; }
        public string skilltype { get; set; }
        public string skills { get; set; }
        public string certification { get; set; }
        public string costcenter { get; set; }
        public string department { get; set; }
        public string jobdescription { get; set; }
        public string salarydetails { get; set; }
        public decimal basicpay { get; set; }
        public decimal cola { get; set; }
        public decimal skilled { get; set; }
        public decimal meal { get; set; }
        public decimal transportation { get; set; }
        public decimal gas { get; set; }
        public decimal communication { get; set; }
        public decimal motorcycle { get; set; }
        public decimal clothing { get; set; }
        public decimal medical { get; set; }
        public string payoutdate { get; set; }
        public string whotolookfor { get; set; }
        public string establishment { get; set; }
        public string officeaddresstoreport { get; set; }
        public string location { get; set; }
        public string classification { get; set; }
        public string projectname { get; set; }
        public string companyrequested { get; set; }
        public string requestor { get; set; }
        public string requestorcontactnumber { get; set; }
        public string requestoremailaddress { get; set; }
        public string employmentstatus { get; set; }
        public string businessunit { get; set; }
        public string Course { get; set; }
    }
}