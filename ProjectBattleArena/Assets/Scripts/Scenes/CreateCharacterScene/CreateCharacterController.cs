using Assets.Scripts.Internal;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.SceneModels;
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
    public void SelectCharacter(int templateId)
    {
        scene.SceneModel.CurrentTemplateId = templateId;
    }
    public async Task<bool> RequestCreateCharacterAsync(CreateCharacterSceneModel sceneModel)
    {
        var tokenData = ApplicationManager.Instance.MakeUserToken();
        tokenData.CharacterName = sceneModel.CharacterName;
        var request = new CreateCharacter()
        {
            CharacterTemplateId = sceneModel.CurrentTemplateId,
            Token = ApplicationManager.Instance.GetUserToken(tokenData),
        };
        var response = await HttpHelper.Request<CreateCharacter, CreateCharacterResponse>(request);

        if (response.Ok == false)
        {
            return false;
        }
        CharacterManager.Instance.Add(response.NewCharacterData);
        return true;
    }
}