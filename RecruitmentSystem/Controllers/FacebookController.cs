using Facebook;
using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
using RecruitmentSystem.Recruitment.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class FacebookController : Controller
    {
        string AppId = SocialMediaAuth.Facebook.AppId;
        string AppSecret = SocialMediaAuth.Facebook.AppSecret;
        string Token = SocialMediaAuth.Facebook.AccessToken;
        string PageId = SocialMediaAuth.Facebook.PageId;
        FacebookClient client;
        public FacebookController()
        {
            client = new FacebookClient();
        }
        public ActionResult ReturnUrl(string mrfid, string ReturnUrl)
        {
            string ErrorMessage = null;
            string SuccessMessage = null;
            try
            {
                Post(Token, mrfid);
                SuccessMessage = mrfid+" has been shared";
                    
            }
            catch (FacebookOAuthException ex)
            {
                ErrorMessage = ex.Message;
            }
            return RedirectToAction("dashboard", "recruitmentprocess", new {mrfid=mrfid, ErrorMessage = ErrorMessage, SuccessMessage = SuccessMessage });
        }
        public ActionResult FacebookPost(string mrfid, string ReturnUrl)
        {
            return Redirect(CallBack(mrfid, ReturnUrl));
        }
        public void Post(string token, string mrfid)
        {

            SocialMediaViewModel model = new SocialMediaViewModel() { mrfid = mrfid };
            client.AccessToken = token;
            dynamic parameters = new ExpandoObject();
            parameters.message = model.ManpowerDetails();
            var result = client.Post(PageId + "/feed", parameters);


        }
        public string CallBack(string mrfid, string ReturnUrl)
        {

            client.AppId = AppId;
            client.AppSecret = AppSecret;
            dynamic token = client.Get("oauth/access_token", new
            {
                client_id = AppId,
                client_secret = AppSecret,
                grant_type = "client_credentials",
                fields = "public_actions,public_profile",
            });
            var redirect_uri = new System.UriBuilder(Request.Url.AbsoluteUri)
            {
                Path = Url.Action("ReturnUrl", "facebook"),
                Query = null,
            };
            var url = client.GetLoginUrl(new
            {
                client_id = client.AppId,
                client_secret = client.AppSecret,
                redirect_uri = redirect_uri.Uri.AbsoluteUri + "?token=" + Token + "&mrfid=" + mrfid + "&returnurl=" + ReturnUrl

            }).AbsoluteUri;
            return url;
        }

    }
}