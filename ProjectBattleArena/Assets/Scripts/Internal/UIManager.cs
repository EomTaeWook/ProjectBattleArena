using Kosher.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Internal
{
    internal class UIManager : MonoBehaviourSingleton<UIManager>
    {
        private readonly List<UIItem> _uiContainer = new List<UIItem>();
        private readonly List<UIItem> _popupContainer = new List<UIItem>();

        private Camera uiCamera;
        private GameObject uiCanvas;
        private GameObject uiPopupCanvas;
        public override void OnAwake()
        {
            var transform = gameObject.AddComponent<RectTransform>();
            transform.sizeDelta = ApplicationManager.Instance.GetResolution();
            transform.position = new Vector3(0, 0, 100);

            uiCamera = gameObject.AddComponent<Camera>();
            uiCamera.depth = 5;
            uiCamera.clearFlags = CameraClearFlags.Depth;
            uiCamera.orthographic = true;

            uiCanvas = new GameObject("UICanvas");
            uiCanvas.transform.SetParent(transform, false);
            var uiCanvasComp = uiCanvas.AddComponent<Canvas>();
            uiCanvasComp.sortingOrder = 10;
            uiCanvasComp.renderMode = RenderMode.ScreenSpaceCamera;
            uiCanvasComp.worldCamera = uiCamera;
            uiCanvas.AddComponent<GraphicRaycaster>();

            uiPopupCanvas = new GameObject("UIPopupCanvas");
            uiPopupCanvas.transform.SetParent(transform, false);
            var uiPopupCanvasComp = uiPopupCanvas.AddComponent<Canvas>();
            uiPopupCanvasComp.sortingOrder = 11;
            uiPopupCanvasComp.renderMode = RenderMode.ScreenSpaceCamera;
            uiPopupCanvasComp.worldCamera = uiCamera;
            uiPopupCanvas.AddComponent<GraphicRaycaster>();
        }

        public void ShowAlert(string title, string body, Action onConfirmCallback = null)
        {
            var item = AlertPopup.Instantiate();
            item.SetContent(Models.AlertPopupType.Alert, title, body, onConfirmCallback);
            item.gameObject.SetActive(true);
        }
        public void ShowConfirmAlert(string title, string body, Action onConfirmCallback)
        {
            var item = AlertPopup.Instantiate();
            item.SetContent(Models.AlertPopupType.Confirm, title, body, onConfirmCallback);
            item.gameObject.SetActive(true);
        }
        public UIItem AddUI(UIItem prefab, Transform parent = null)
        {
            if(_uiContainer.Count > 0)
            {
                _uiContainer[_uiContainer.Count - 1].gameObject.SetActive(false);
            }

            var item = KosherUnityObjectPool.Instance.Pop<UIItem>(prefab);
            _uiContainer.Add(item);
            if(parent == null)
            {
                item.transform.SetParent(this.uiCanvas.transform, false);
            }
            else
            {
                item.transform.SetParent(parent, false);
            }
            
            item.gameObject.SetActive(true);
            return item;
        }

        public UIItem AddPopupUI(UIItem prefab)
        {
            var item = KosherUnityObjectPool.Instance.Pop<UIItem>(prefab);
            _popupContainer.Add(item);
            item.transform.SetParent(this.uiPopupCanvas.transform, false);
            item.gameObject.SetActive(true);
            return item;
        }

        public void CloseUI(UIItem item)
        {
            var removed = new List<UIItem>();
            for(int i=0; i< _uiContainer.Count; ++i)
            {
                if (_uiContainer[i] == item)
                {
                    removed.Add(item);
                }
            }

            var removedPopup = new List<UIItem>();
            for (int i = 0; i < _popupContainer.Count; ++i)
            {
                if (_popupContainer[i] == item)
                {
                    removedPopup.Add(item);
                }
            }

            foreach (var remove in removed)
            {
                remove.Recycle<UIItem>();
                _uiContainer.Remove(remove);
            }

            foreach(var remove in removedPopup)
            {
                remove.Recycle<UIItem>();
                _popupContainer.Remove(remove);
            }
        }
    }
}
