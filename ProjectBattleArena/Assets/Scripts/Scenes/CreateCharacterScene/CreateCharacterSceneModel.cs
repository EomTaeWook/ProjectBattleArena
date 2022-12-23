using Protocol.GameWebServerAndClient.ShareModels;

namespace Assets.Scripts.Scenes.SceneModels
{
    public class CreateCharacterSceneModel
    {
        public JobType CurrentJobType { get; set; } = JobType.SwordMan;

        public string CharacterName { get; set; }
    }
}
