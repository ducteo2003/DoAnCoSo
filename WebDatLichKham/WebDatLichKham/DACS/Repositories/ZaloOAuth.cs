using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class ZaloOAuth
{
    private readonly string _appId;
    private readonly string _appSecret;

    public ZaloOAuth(string appId, string appSecret)
    {
        _appId = appId;
        _appSecret = appSecret;
    }

    public async Task<string> GetAccessTokenAsync(string authorizationCode)
    {
        var url = "https://oauth.zaloapp.com/v3/access_token";
        var payload = new
        {
            app_id = _appId,
            app_secret = _appSecret,
            code = authorizationCode
        };

        using (var client = new HttpClient())
        {
            var content = new StringContent(JObject.FromObject(payload).ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseString);

            if (json["access_token"] != null)
            {
                return json["access_token"].ToString();
            }
            else
            {
                throw new Exception("Error getting access token: " + responseString);
            }
        }
    }
}
