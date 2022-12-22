using Assets.Scripts.Internal;
using Kosher.Log;
using Kosher.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class AlertPopup : UIElement
{
    [SerializeField]
    private TextMeshProUGUI txtTitle;
    [SerializeField]
    private TextMeshProUGUI txtBody;    

    public static AlertPopup Instantiate()
    {
        var prefab = KosherUnityResourceManager.Instance.LoadResouce<AlertPopup>($"Prefabs/UI/Popup/AlertPopup");

        if(prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }
        var item = UIManager.Instance.AddPopupUI(prefab);
        return item.GetComponent<AlertPopup>();
    }

    public void SetContent(string title, string body)
    {
        txtTitle.text = title;
        txtBody.text = body;
    }
    
    public void OnBtnConfirmClick()
    {
        this.Dispose();
    }

}
