using Protocol.GameWebServerAndClient;
using Repository;
using ShareLogic;

namespace GameWebServer.Controllers.Crypto
{
    public class GetSecurityKeyController : APIController<GetSecurityKey>
    {
        private SecurityRepository _securityRepository;
        public GetSecurityKeyController(SecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }
        public override async Task<IGWCResponse> Process(GetSecurityKey request)
        {
            var publicKey = await _securityRepository.LoadSecurityPublicKeyAsync();

            return new GetSecurityKeyResponse()
            {
                Ok = true,
                SecurityKey = publicKey
            };
        }
    }
}
