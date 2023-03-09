using Assets.Scripts.Internal;
using Assets.Scripts.Models;
using Kosher.Log;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertPopup : UIItem
{
    [SerializeField]
    private TextMeshProUGUI txtTitle;
    [SerializeField]
    private TextMeshProUGUI txtBody;
    [SerializeField]
    private Button btnConfirm;
    [SerializeField]
    private Button btnCancel;

    private Action onConfrimCallback;
    public static AlertPopup Instantiate()
    {
        var prefab = ResourceManager.Instance.LoadAsset<AlertPopup>($"Prefabs/UI/AlertPopup");

        if(prefab == null)
        {
            LogHelper.Error($"no prefab");
            return null;
        }
        var item = UIManager.Instance.AddPopupUI(prefab);
        return item.GetComponent<AlertPopup>();
    }

    public void SetContent(AlertPopupType alertPopupType,
                        string title,
                        string body,
                        Action onConfrimCallback = null)
    {
        txtTitle.text = title;
        txtBody.text = body;

        if(alertPopupType == AlertPopupType.Confirm)
        {
            btnCancel.gameObject.SetActive(true);
        }
        else
        {
            btnCancel.gameObject.SetActive(false);
        }
        this.onConfrimCallback = onConfrimCallback;
    }
    
    public void OnBtnConfirmClick()
    {
        onConfrimCallback?.Invoke();
        this.DisposeUI();
    }
    public void OnBtnCancelClick()
    {
        this.DisposeUI();
    }
}
