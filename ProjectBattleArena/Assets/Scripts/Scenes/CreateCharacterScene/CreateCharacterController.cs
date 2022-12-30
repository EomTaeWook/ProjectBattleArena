using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.SceneModels;
using DataContainer;
using Protocol.GameWebServerAndClient;
using System.Threading.Tasks;

internal class CreateCharacterController : SceneController<CreateCharacterController>
{
    CreateCharacterScene scene;
    public override void BindScene(BaseScene baseScene)
    {
        scene = baseScene as CreateCharacterScene;
    }
    public void Dispose()
    {
        scene = null;
    }
    public async Task<bool> RequestCreateCharacterAsync(CreateCharacterSceneModel sceneModel)
    {
        var request = new CreateCharacter()
        {
            CharacterName = sceneModel.CharacterName,
            Job = (JobType)sceneModel.CurrentTemplateId
        };
        var response = await HttpRequestHelper.AuthRequest<CreateCharacter, CreateCharacterResponse>(request);

        if (response.Ok == false)
        {
            return false;
        }
        CharacterManager.Instance.Add(response.NewCharacterData);
        return true;
    }
}