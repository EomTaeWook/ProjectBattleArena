using Assets.Scripts.Internal;
using Assets.Scripts.Models;
using Assets.Scripts.Scenes.SceneModels;
using System.Linq;
using TMPro;
using UnityEngine;

internal class CreateCharacterScene : BaseScene<CreateCharacterSceneModel>
{
    [SerializeField]
    private TextMeshProUGUI txtCharcterName;
    [SerializeField]
    private GameObject[] chracters;
    public override void OnAwakeScene()
    {
        CreateCharacterController.Instance.BindScene(this);
    }
    public void RefreshCharacter(int selectIndex)
    {
        for(int i=0; i< chracters.Length; ++i)
        {
            if(selectIndex == i)
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
        CreateCharacterController.Instance.Dispose();
    }
    public void OnBackButtonClick()
    {
        if(CharacterManager.Instance.CharacterCount == 0)
        {
            SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.TitleScene);
        }
        else
        {
            SceneManager.Instance.LoadScene(Assets.Scripts.Models.SceneType.SelectCharacterScene);
        }
    }
    public void OnCharacterButtonClick(int templateId)
    {
        SceneModel.CurrentTemplateId = templateId;

        RefreshCharacter(templateId % 1000);
    }
    public async void OnCreateButtonClickAsync()
    {
        if(txtCharcterName.text.Count() < 3)
        {
            UIManager.Instance.ShowAlert("알림", "캐릭터 명이 짧습니다.");
            return;
        }
        if (string.IsNullOrEmpty(txtCharcterName.text) == true)
        {
            UIManager.Instance.ShowAlert("알림", "캐릭터명을 입력해주세요.");
            return;
        }
        SceneModel.CharacterName = txtCharcterName.text;
        var created = await CreateCharacterController.Instance.RequestCreateCharacterAsync(this.SceneModel);
        if(created == false)
        {
            UIManager.Instance.ShowAlert("알림", "캐릭터 생성에 실패하였습니다.");
            return;
        }

        SceneManager.Instance.LoadScene(SceneType.SelectCharacterScene);
    }
}