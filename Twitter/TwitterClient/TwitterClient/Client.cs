using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace TwitterClient
{
    public static class Client
    {
        private static int sinceId;

        static Client()
        {
            sinceId = 0;
        }

        public static List<Tweet> GetTweets(string hashtag, int count)
        {
            var accessToken = "968195499697278977-h8LRnSDbGnVziwazVvgq8OCQyuvcaLH";// ConfigurationManager.AppSettings["accessToken"];
            var accessTokenSecret = "tweUelFi3qD7NCcpp8pfJ8o4G9aT73xvKsaJ6tGOXQGp4";// ConfigurationManager.AppSettings["accessTokenSecret"];
            var consumerKey = "FVGwqaJyk1gy5OitrktPCjNiF";// ConfigurationManager.AppSettings["consumerKey"];
            var consumerSecret = "qQHZT0ngPUaKE6WCrkIZh1zbzhYHbQ1sW7sn0QDmL0ZTzMgxBE";// ConfigurationManager.AppSettings["consumerSecret"];

            var api = new API(accessToken, accessTokenSecret, consumerKey, consumerSecret);

            sinceId += count;

            var response = api.Get("https://api.twitter.com/1.1/search/tweets.json?q=" + hashtag + "&count=" + count + "&since_id=" + sinceId);

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
