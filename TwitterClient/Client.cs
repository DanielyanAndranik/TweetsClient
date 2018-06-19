using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace TwitterClient
{
    public static class Client
    {
        static List<Tweet> GetTweets(string urlAndParameters)
        {
            var accessToken = ConfigurationManager.AppSettings["accessToken"];
            var accessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"];
            var consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];

            var api = new API(accessToken, accessTokenSecret, consumerKey, consumerSecret);

            var response = api.Get(urlAndParameters);

            var tweets = new List<Tweet>();

            foreach (var jsonObject in (JArray)response[0].Get("statuses"))
            {
                tweets.Add(new Tweet
                {
                    Text = (string)jsonObject["text"],
                    Username = (string)jsonObject["user"]["screen_name"],
                    Image = (string)jsonObject["user"]["profile_image_url_https"]
                });
            }

            return tweets;
        }
    }
}
