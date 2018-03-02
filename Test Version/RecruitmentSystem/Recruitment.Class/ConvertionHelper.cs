using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentSystem.Recruitment.Class
{
    public class ConvertionHelper
    {
        public static string NulltoEmptyString(string obj)
        {
            if (obj == null)
            {
                obj = "";
            }
            return obj;
        }
    }
}