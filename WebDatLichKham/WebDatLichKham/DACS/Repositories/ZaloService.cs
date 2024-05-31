namespace WebDatLichKham.Repositories
{
    public class ZaloService
    {
        private readonly string _accessToken;

        public ZaloService(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<string> SendMessageAsync(string phoneNumber, string message)
        {
            var url = "https://openapi.zalo.me/v2.0/oa/message";
            var payload = new
            {
                recipient = new { user_id = phoneNumber },
                message = new { text = message }
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
                var response = await client.PostAsJsonAsync(url, payload);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }
                else
                {
                    return responseString; // Trả về phản hồi chi tiết từ API
                }
            }
        }
    }
}