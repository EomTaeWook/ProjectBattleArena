using Assets.Scripts.Internal;
using Assets.Scripts.Scenes.ScenesControl;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;
using System.Linq;
using TMPro;
using UnityEngine;

internal class CreateCharacterScene : BaseScene
{
    [SerializeField]
    private TextMeshProUGUI txtCharcterName;
    [SerializeField]
    private GameObject[] chracters;
    public override void OnAwakeScene()
    {
        RefreshCharacter();
    }
    public void RefreshCharacter()
    {
        var index = (int)CreateCharacterControl.Instance.CurrentJobType;

        for(int i=0; i< chracters.Length; ++i)
        {
            if(index == i)
            {
                chracters[i].gameObject.SetActive(true);
            }
            else
            {
                chracters[i].gameObject.SetActive(false);
            }
        }
    }
    public override void OnDestroyScene()
    {
        CreateCharacterControl.Instance.Dispose();
    }
    public void OnBackButtonClick()
    {
        if(CharacterManager.Instance.CharacterCount == 0)
        {
            SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.TitleScene);
        }
        else
        {
            SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.LobbyScene);
        }
    }
    public void OnCharacterButtonClick(int index)
    {
        CreateCharacterControl.Instance.ChangeCurrentCharacter((JobType)index);
        RefreshCharacter();
    }

    public async void OnCreateButtonClickAsync()
    {
        if(txtCharcterName.text.Count() < 3)
        {
            UIManager.Instance.ShowAlert("알림", "캐릭터 명이 짧습니다.");
            return;
        }
        await CreateCharacterControl.Instance.RequestCreateCharacterAsync(txtCharcterName.text);
    }
    
}