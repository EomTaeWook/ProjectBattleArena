using BA.Repository;
using DataContainer.Generated;
using Protocol.GameWebServerAndClient;
using TemplateContainers;

namespace GameWebServer.Controllers.Character
{
    public class GachaSkillController : AuthAPIController<GachaSkill>
    {
        private SkillRepository _skillRepository;
        public GachaSkillController(SkillRepository skillRepository,
            CharacterRepository characterRepository)
        {
            _skillRepository = skillRepository;
        }

        public override async Task<IGWCResponse> Process(string account, GachaSkill request)
        {





            return new GachaSkillResponse()
            {
                Ok = true,
            };
        }
    }
}
