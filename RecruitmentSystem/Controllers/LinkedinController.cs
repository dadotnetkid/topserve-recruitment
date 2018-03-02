using Newtonsoft.Json;
using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RecruitmentSystem.Controllers
{
    public class LinkedinController : Controller
    {
        public class AccessToken
        {
            public string access_token { get; set; }
            public string expires_in { get; set; }
        }

        string ClientId = SocialMediaAuth.LinkedIn.ClientId;
        string ClientSecret = SocialMediaAuth.LinkedIn.ClientSecret;
        public ActionResult LinkedIn(string Code, string Mrfid)
        {
            if (Code == null)
            {

                return Oauth(Mrfid);
            }
            else
            {
                return Post(GetAccessToken(Code, Mrfid), Mrfid);
            }
        }
        public RedirectResult Oauth(string Mrfid)
        {
            var redirect_uri = new System.UriBuilder(Request.Url.AbsoluteUri)
            {
                Path = Url.Action("linkedin", "linkedin"),
                Query = null,
            }.Uri.AbsoluteUri + "?mrfid=" + Mrfid; ;
            return Redirect(string.Format("https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=81744zy6azsoyl&redirect_uri={0}&state={1}&scope=r_basicprofile,w_share", redirect_uri, Guid.NewGuid()));
        }
        public string GetAccessToken(string Code, string Mrfid)
        {
            string parameters = "";
            var redirect_uri = new System.UriBuilder(Request.Url.AbsoluteUri)
          {
              Path = Url.Action("linkedin", "linkedin"),
              Query = null,
          }.Uri.AbsoluteUri + "?mrfid=" + Mrfid;
            using (WebClient wc = new WebClient())
            {
                System.Collections.Specialized.NameValueCollection postData =
                   new System.Collections.Specialized.NameValueCollection()
               {
                      { "grant_type","authorization_code" },  
                      { "code", Code },
                      { "redirect_uri", redirect_uri },
                      {"client_id",ClientId},
                      {"client_secret",ClientSecret}
               };
                parameters = Encoding.UTF8.GetString(wc.UploadValues("https://www.linkedin.com/oauth/v2/accessToken/", postData));
            }
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            var accessToken = JsonConvert.DeserializeObject<AccessToken>(parameters);
            return accessToken.access_token;
        }

        public RedirectToRouteResult Post(string AccessToken, string Mrfid)
        {
            var requestUrl = string.Format("https://api.linkedin.com/v1/people/~/shares?format=json&oauth2_access_token={0}", AccessToken);
            // var message = ("{\"comment\": \"" + "comment" + "\",\"content\": {\"title\":\"" + "title" + "\",\"description\": \"" + "description" + "\" },\"visibility\":{\"code\":\"anyone\"}}");
            var message = new
            {
                comment = new SocialMediaViewModel() { mrfid = Mrfid }.ManpowerDetails(),
                visibility = new { code = "anyone" }
            };

            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            client.Headers.Add("x-li-format", "json");
            var responseJson = client.UploadString(new Uri(requestUrl), JsonConvert.SerializeObject(message));
            var response = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(responseJson);
            return RedirectToAction("dashboard", "RecruitmentProcess", new { mrfid = Mrfid });
        }
    }
}