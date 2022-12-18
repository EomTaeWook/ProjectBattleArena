using Newtonsoft.Json;
using Protocol.GameWebServerAndClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Internal
{

    public static class HttpRequestHelper
    {
        private static readonly HttpClient _webClient = new HttpClient();

        public static async Task<TRes> Request<TReq, TRes>(TReq request) where TReq : ICGWRequest where TRes : IGWCResponse
        {
            var typeName = request.GetType().Name;
            var json = JsonConvert.SerializeObject(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _webClient.PostAsync($"{ApplicationManager.Instance.CurrentServerUrl}/{typeName}", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TRes>(jsonResponse);
        }
    }
}
