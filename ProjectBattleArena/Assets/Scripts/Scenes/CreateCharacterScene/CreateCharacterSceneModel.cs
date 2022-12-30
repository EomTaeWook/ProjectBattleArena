using Protocol.GameWebServerAndClient.ShareModels;

namespace Assets.Scripts.Scenes.SceneModels
{
    public class CreateCharacterSceneModel
    {
        public int CurrentTemplateId { get; set; } = -1;

        public string CharacterName { get; set; }
    }
}
