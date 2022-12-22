using BA.Repository;
using Protocol.GameWebServerAndClient;

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
            var loadSecurityKeyModel  = await _securityRepository.LoadLatestSecurityKey();

            if(loadSecurityKeyModel == null)
            {
                return MakeCommonErrorMessage("failed to laod security key");
            }

            return new GetSecurityKeyResponse()
            {
                Ok = true,
                SecurityKey = loadSecurityKeyModel.PublicKey
            };
        }
    }
}
