using Assets.Scripts.Internal;
using Assets.Scripts.Models;
using Kosher.Log;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;
using TMPro;
using UnityEngine;

public class CharacterSlot : UIComponent
{
    [SerializeField]
    private TextMeshProUGUI txtCharcterName;
    [SerializeField]
    private TextMeshProUGUI txtCharcterJobType;
    [SerializeField]
    private TextMeshProUGUI txtCreateCharacter;

    private bool isCreateButton;
    CharacterData characterData;
    public static CharacterSlot Instantiate()
    {
        var prefab = KosherUnityResourceManager.Instance.LoadResouce<CharacterSlot>($"Prefabs/SelectCharacter/CharacterSlot");

        if (prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }

        var item = KosherUnityObjectPool.Instance.Pop<CharacterSlot>(prefab);
        return item;
    }
    public void SetCreateMode(bool isCreateMode)
    {
        txtCharcterName.gameObject.SetActive(!isCreateMode);
        txtCharcterJobType.gameObject.SetActive(!isCreateMode);
        txtCreateCharacter.gameObject.SetActive(isCreateMode);

        isCreateButton = isCreateMode;
    }
    public void Init(CharacterData characterData)
    {
        txtCharcterName.text = characterData.CharacterName;
        txtCharcterJobType.text = characterData.Job.ToString();
        this.characterData = characterData;
    }
    public void OnSelectButtonClick()
    {
        if(isCreateButton == false)
        {
            SelectCharacterSceneController.Instance.SelectCharacter(this.characterData);
        }
        else
        {
            SelectCharacterSceneController.Instance.Dispose();

            SceneManager.Instance.LoadScene(SceneType.CreateCharacterScene);
        }        
    }
}
