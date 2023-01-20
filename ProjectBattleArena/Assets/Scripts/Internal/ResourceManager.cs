using DataContainer.Generated;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Unity;
using TemplateContainers;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class ResourceManager : Singleton<ResourceManager>
    {
        public T LoadAsset<T>(string path) where T : Component
        {
            return KosherUnityResourceManager.Instance.LoadResource<T>(path);
        }
        
        public GameObject LoadCharcterAsset(int templateId)
        {
            var template = TemplateContainer<CharacterTemplate>.Find(templateId);

            if(template.Invalid())
            {
                LogHelper.Error("template is invalid");
                return null;
            }

            return LoadCharcterAsset(template);
        }

        public GameObject LoadCharcterAsset(CharacterTemplate characterTemplate)
        {
            return LoadCharcterAsset(characterTemplate, null);
        }
        public GameObject LoadCharcterAsset(CharacterTemplate characterTemplate, Transform parent)
        {
            var prefab = KosherUnityResourceManager.Instance.LoadResource<GameObject>($"Prefabs/Character/{characterTemplate.Name}");

            var go = KosherUnityObjectPool.Instance.Pop(prefab);

            if(parent != null)
            {
                go.transform.SetParent(parent, false);
            }

            return go;
        }

    }
}
