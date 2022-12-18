using Assets.Scripts.Models;
using Kosher.Unity;
using System.Collections.Generic;

namespace Assets.Scripts.Internal
{
    internal class UIManager : MonoBehaviourSingleton<UIManager>
    {
        private readonly List<UIElement> _uiContainer = new List<UIElement>();
        private readonly List<UIElement> _popupContainer = new List<UIElement>();
        public void AddUI(UIElement prefab)
        {
            var item = KosherUnityObjectPool.Instance.Pop<UIElement>(prefab);
            _uiContainer.Add(item);
        }

        public void AddPopupUI(UIElement prefab)
        {
            var item = KosherUnityObjectPool.Instance.Pop<UIElement>(prefab);
            _popupContainer.Add(item);
        }

        public void CloseUI(UIElement item)
        {
            var removed = new List<UIElement>();
            for(int i=0; i< _uiContainer.Count; ++i)
            {
                if (_uiContainer[i] == item)
                {
                    removed.Add(item);
                }
            }

            var removedPopup = new List<UIElement>();
            for (int i = 0; i < _popupContainer.Count; ++i)
            {
                if (_popupContainer[i] == item)
                {
                    removedPopup.Add(item);
                }
            }

            foreach (var remove in removed)
            {
                KosherUnityObjectPool.Instance.Push(remove);
                _uiContainer.Remove(remove);
            }

            foreach(var remove in removedPopup)
            {
                KosherUnityObjectPool.Instance.Push(remove);
                _popupContainer.Remove(remove);
            }
        }
    }
}
