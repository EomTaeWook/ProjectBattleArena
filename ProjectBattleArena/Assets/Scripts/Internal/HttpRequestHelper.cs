using Newtonsoft.Json;
using Protocol.GameWebServerAndClient;
using ShareLogic;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    public class HttpHelper
    {
        static readonly HttpRequestHelper _requester = new HttpRequestHelper();
        public static async Task<TRes> Request<TReq, TRes>(TReq request) where TReq : ICGWRequest where TRes : IGWCResponse
        {
            var typeName = request.GetType().Name;
            var json = JsonConvert.SerializeObject(request);

            var url = $"{ApplicationManager.Instance.CurrentServerUrl}/{typeName}";

            var go = Loading.Instantiate();
            try
            {
                var jsonResponse = await _requester.PostByJsonAsync(url, json);
                return JsonConvert.DeserializeObject<TRes>(jsonResponse);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            finally
            {
                go.DisposeUI();
            }
            return default;
            
        }

        public static async Task<TRes> AuthRequest<TReq, TRes>(TReq request) where TReq : AuthRequest where TRes : IGWCResponse
        {
            request.Token = ApplicationManager.Instance.GetUserToken();

            var response = await Request<TReq, TRes>(request);

            return response;
        }
    }
}
