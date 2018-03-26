using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Recruitment.Class
{
    public static class Tools
    {
        public static string GeneratePassword()
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var password = "";
            Random rnd = new Random();
            for (int i = 0; i <= 8; i++)
            {
                password += str[rnd.Next(0, str.Length - 1)];
            }
            return password;
        }
        public static decimal? ParseDivideToZero(decimal? first, decimal? second)
        {
            decimal? retval = 0.0M;
            try
            {
                retval = first / second;
            }
            catch (Exception)
            {
            }
            return retval * 100.0M;
        }

        public static DateTime ToDateTime(object datetime)
        {
            DateTime retval = DateTime.Now;
            try
            {
                retval = Convert.ToDateTime(datetime);
            }
            catch (Exception)
            {

                retval = DateTime.Now;
            }
            return retval;
        }
        public static DateTime? ToDateTimeNull(object datetime)
        {
            DateTime? retval = DateTime.Now;
            try
            {
                retval = Convert.ToDateTime(datetime);
            }
            catch (Exception)
            {

                retval = null;
            }
            return retval;
        }
        public static Decimal? ToDecimalNull(object obj)
        {
            Decimal? retval = null;
            try
            {
                retval = Convert.ToDecimal(obj);
            }
            catch (Exception)
            {

                retval = null;
            }
            return retval;
        }
        public static int? ToIntNull(object obj)
        {
            int? retval = null;
            try
            {
                retval = Convert.ToInt32(obj);
            }
            catch (Exception)
            {

                retval = null;
            }
            return retval;
        }
        public static Boolean ToBoolean(object boolean)
        {
            Boolean retval = false;
            try
            {
                retval = Convert.ToBoolean(boolean);
            }
            catch (Exception)
            {

                retval = false;
            }
            return retval;
        }
        public static int ToInteger(object integer)
        {
            int retval = 0;
            try
            {
                retval = Convert.ToInt32(integer);
            }
            catch (Exception)
            {

                retval = 0;
            }
            return retval;
        }
        public static string ToDecimal(object obj, string Format)
        {
            decimal retval = 0.0M;
            try
            {
                retval = Convert.ToDecimal(obj);
            }
            catch (Exception)
            {

                retval = 0.0M;
            }
            return retval.ToString(Format);
        }
        public static decimal ToDecimal(object obj)
        {
            decimal retval = 0.0M;
            try
            {
                retval = Convert.ToDecimal(obj);
            }
            catch (Exception)
            {

                retval = 0.0M;
            }
            return retval;
        }
        public static string LoadingImage()
        {
            string retval = "";
            return retval;
        }
        public static string RandomFileName(HttpPostedFileBase file)
        {
            string extention = System.IO.Path.GetExtension(file.FileName);
            int retval = new Random().Next(1, 1000000000);
            return "applicant-list-" + retval.ToString("D9") + extention;
        }

        public static string ReturnEmptyString(string str)
        {
            string retval = (str == null) ? "" : str;
            return retval;
        }
        public static bool YesToBoolean(object boolean)
        {
            return (boolean.ToString().ToString().ToLower() == "y") ? true : false;
        }
        public static string getdate(int? min)
        {
            string intervals = min + " minute(s)";

            if ((min / 60) >= 24)
            {
                intervals = (min / 60) / 24 + " day(s)";

            }
            else if (min >= 60)
            {
                intervals = min / 60 + " hr(s)";
            }

            return intervals;
        }
        public static string GetManpowerType(string mrfid)
        {
            string retval = "Reserve";
            retval = (mrfid.ToLower().Contains("reserve")) ? "Reserve" : "ManpowerRequest";
            return retval;
        }
        public static string URLReplace(string retval)
        {
            var _retval = retval.ToLower().Replace("a", "%61")
                     .Replace("b", "%62")
                     .Replace("c", "%63")
                     .Replace("d", "%64")
                     .Replace("e", "%65")
                     .Replace("f", "%66")
                     .Replace("g", "%67")
                     .Replace("h", "%68")
                     .Replace("i", "%69")
                     .Replace("j", "%6A")
                     .Replace("k", "%6B")
                     .Replace("l", "%6C")
                     .Replace("m", "%6D")
                     .Replace("n", "%6E")
                     .Replace("o", "%6F")
                     .Replace("p", "%70")
                     .Replace("q", "%71")
                     .Replace("r", "%72")
                     .Replace("s", "%73")
                     .Replace("t", "%74")
                     .Replace("u", "%75")
                     .Replace("v", "%76")
                     .Replace("w", "%77")
                     .Replace("x", "%78")
                     .Replace("y", "%79")
                     .Replace("z", "%7A")
                     .Replace("&", "%26")
                     .Replace(":", "%3A")
                     .Replace(",", "%2C")
                     .Replace(".", "%2E")
                     .Replace("\r\n", "%0A");
            return _retval;
        }
        public static string ToTitleCase(object obj)
        {
            try
            {
                return new CultureInfo("en-US", false).TextInfo.ToTitleCase(obj.ToString());

            }
            catch (Exception)
            {

                return (obj == null) ? "" : obj.ToString();
            }
        }
        public static int ParseToInt(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public static decimal ParseToDecimal(this object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return 0.0M;

            }

        }

    }
}
