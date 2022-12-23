using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShareLogic
{
    public class HttpRequester
    {
        private static readonly HttpClient _webClient = new HttpClient();

        public async Task<string> PostByJsonAsync(string url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _webClient.PostAsync(url, content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
    }
}
