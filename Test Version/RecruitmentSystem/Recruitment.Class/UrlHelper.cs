using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Recruitment.Class
{
    public class Urls : UrlHelper
    {

        public override string Action(string actionName, string controllerName)
        {
            return base.Action(actionName, controllerName);
        }
        public override string Action(string actionName)
        {
            return base.Action(actionName);
        }
        public override string Action(string actionName, string controllerName, object routeValues)
        {
            return base.Action(actionName, controllerName, routeValues);
        }
        public override string Action(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            return base.Action(actionName, controllerName, routeValues);
        }
        public override string Action(string actionName, object routeValues)
        {
            return base.Action(actionName, routeValues);
        }
        public override string Action(string actionName, string controllerName, object routeValues, string protocol)
        {
            return base.Action(actionName, controllerName, routeValues, protocol);
        }
        public override string Action(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues, string protocol)
        {
            return base.Action(actionName, controllerName, routeValues, protocol);
        }
        public override string Action(string actionName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            return base.Action(actionName, routeValues);
        }
        public override string Action(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues, string protocol, string hostName)
        {
            return base.Action(actionName, controllerName, routeValues, protocol, hostName);
        }
       
    }
}