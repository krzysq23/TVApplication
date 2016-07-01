using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TVApi.Controllers
{
    public class TraktController : ApiController
    {
        private class Code
        {
            public string device_code { get; set; }
            public string user_code { get; set; }
            public string verification_url { get; set; }
            public string expires_in { get; set; }
            public string interval { get; set; }
        }

        public async void GetAccessTokenForDevice()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://trakt.tv/");
            var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", "2e64662d1006d78d0c4030491b2ad5ae47cd194dfc2b2702a508b3451c91e8ec"),
                    new KeyValuePair<string, string>("client_secret", "070f0581f0933b58605524f9aad6affb520777d43891e8b98294a7c14334ea18"),
                    new KeyValuePair<string, string>("redirect_uri", "http://localhost:7620/api/Movie"),
                });
            using (var response = await client.PostAsync("/oauth/device/code", content))
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var code = JsonConvert.DeserializeObject<Code>(responseData);


                var content2 = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", "2e64662d1006d78d0c4030491b2ad5ae47cd194dfc2b2702a508b3451c91e8ec"),
                    new KeyValuePair<string, string>("client_secret", "070f0581f0933b58605524f9aad6affb520777d43891e8b98294a7c14334ea18"),
                    new KeyValuePair<string, string>("code", code.device_code)
                });
                HttpClient client2 = new HttpClient();
                client2.BaseAddress = new Uri("https://trakt.tv/");
                using (var response2 = await client2.PostAsync("/oauth/device/token", content2))
                {
                    string responseData2 = await response2.Content.ReadAsStringAsync();

                }
            }

        }

        public IHttpActionResult GetAccessTokenForWeb()
        {
            return Redirect("https://trakt.tv/oauth/authorize?response_type=code&client_id=2e64662d1006d78d0c4030491b2ad5ae47cd194dfc2b2702a508b3451c91e8ec&redirect_uri=http://localhost:7620/api/Movie&state=state");
        }

        private class TraktAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
            public string created_at { get; set; }
        }

        public async void GetAccessTokenForWeb(string code)
        {
            var baseAddress = new Uri("https://trakt.tv/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("client_id", "2e64662d1006d78d0c4030491b2ad5ae47cd194dfc2b2702a508b3451c91e8ec"),
                    new KeyValuePair<string, string>("client_secret", "070f0581f0933b58605524f9aad6affb520777d43891e8b98294a7c14334ea18"),
                    new KeyValuePair<string, string>("redirect_uri", "http://localhost:7620/api/Movie"),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                });
                {
                    using (var response = await httpClient.PostAsync("oauth/token", content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        var accessToken = JsonConvert.DeserializeObject<TraktAccessToken>(responseData);

                    }
                }
            }
        }
    }
}
