using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Models
{
    public class RecruitmentManpowerProcessViewModel
    {
        public string mrfid { get; set; }
        public string Approved { get; set; }
        public string Cancel { get; set; }
        public string Reject { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public int tab { get; set; }
        public string Active(int tabmenu)
        {
            string retval = tabmenu==tab?"active":"";
            return retval;
        }
        
    }
}