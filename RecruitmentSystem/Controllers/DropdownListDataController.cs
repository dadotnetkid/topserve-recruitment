using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class DropdownListDataController : Controller
    {
        DatabaseModelDataContext db = new DatabaseModelDataContext();
        public ActionResult PositionSearch(string Position = "")
        {

            var list = db.sp_position_search().ToList();
            list = list.Where(m => m.requiredposition.ToLower().Contains(Position.ToLower())).ToList();
            var retval = new List<sp_position_searchResult>();
            foreach (var i in list)
            {
                
                    var res = i.requiredposition.Split(',');
               
                    foreach(var str in res)
                    {
                       foreach(var _str in str.Split('/'))
                       {
                           if (retval.Where(m => m.requiredposition.ToLower() == _str.ToLower().Trim()).Count() == 0)
                           {
                               retval.Add(new sp_position_searchResult()
                               {
                                   requiredposition = _str.ToUpper().Trim()
                               });
                           }
                       }
                      
                    }
                  
                
            }
            return View(retval.Skip(10 * 0).Take(10).OrderBy(m=>m.requiredposition));
        }
    }
}