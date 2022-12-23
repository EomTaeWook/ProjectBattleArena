using Kosher.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Internal
{
    internal class UIManager : MonoBehaviourSingleton<UIManager>
    {
        private readonly List<UIComponent> _uiContainer = new List<UIComponent>();
        private readonly List<UIComponent> _popupContainer = new List<UIComponent>();

        private Camera uiCamera;
        private GameObject uiCanvas;
        private GameObject uiPopupCanvas;
        public override void OnAwake()
        {
            var transform = gameObject.AddComponent<RectTransform>();
            transform.sizeDelta = ApplicationManager.Instance.GetResolution();
            transform.position = new Vector3(0, 0, 100);

            uiCamera = gameObject.AddComponent<Camera>();
            uiCamera.depth = 1;
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

        public void ShowAlert(string title, string body)
        {
            var item = AlertPopup.Instantiate();
            item.SetContent(title, body);
            item.gameObject.SetActive(true);
        }
        public UIComponent AddUI(UIComponent prefab)
        {
            var item = KosherUnityObjectPool.Instance.Pop<UIComponent>(prefab);
            _uiContainer.Add(item);
            item.transform.SetParent(this.uiCanvas.transform, false);
            item.gameObject.SetActive(true);
            return item;
        }

        public UIComponent AddPopupUI(UIComponent prefab)
        {
            var item = KosherUnityObjectPool.Instance.Pop<UIComponent>(prefab);
            _popupContainer.Add(item);
            item.transform.SetParent(this.uiPopupCanvas.transform, false);
            item.gameObject.SetActive(true);
            return item;
        }

        public void CloseUI(UIComponent item)
        {
            var removed = new List<UIComponent>();
            for(int i=0; i< _uiContainer.Count; ++i)
            {
                if (_uiContainer[i] == item)
                {
                    removed.Add(item);
                }
            }

            var removedPopup = new List<UIComponent>();
            for (int i = 0; i < _popupContainer.Count; ++i)
            {
                if (_popupContainer[i] == item)
                {
                    removedPopup.Add(item);
                }
            }

            foreach (var remove in removed)
            {
                remove.Recycle<UIComponent>();
                _uiContainer.Remove(remove);
            }

            foreach(var remove in removedPopup)
            {
                remove.Recycle<UIComponent>();
                _popupContainer.Remove(remove);
            }
        }
    }
}
