using RecruitmentSystem.Models;
using RecruitmentSystem.Recruitment.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitterizer;

namespace RecruitmentSystem.Controllers
{
    public class TwitterController : Controller
    {
        // GET: Twitter
        public string TweetPost(string mrfid, string ReturnUrl)
        {
            TwitterStatus twitter = new TwitterStatus();
            OAuthTokens tokens = new OAuthTokens() { AccessToken =  SocialMediaAuth.Twitter.AccessToken, AccessTokenSecret = SocialMediaAuth.Twitter.AccessTokenSecret, ConsumerKey = SocialMediaAuth.Twitter.ConsumerKey, ConsumerSecret = SocialMediaAuth.Twitter.ConsumerKey};
            SocialMediaViewModel model = new SocialMediaViewModel() { mrfid = mrfid };
            var response = TwitterStatus.Update(tokens, model.ManpowerDetails(), new StatusUpdateOptions() { UseSSL = true, APIBaseAddress = "http://api.twitter.com/1.1/"});
            if (response.Result == RequestResult.Success)
            {

            }
            else
            {
                Console.WriteLine(response.ErrorMessage);
            }
            return response.Content;
        }
    }
}