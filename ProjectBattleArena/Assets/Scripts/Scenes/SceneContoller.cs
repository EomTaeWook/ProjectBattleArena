using Kosher.Framework;

namespace Assets.Scripts.Scenes
{
    public abstract class SceneController<T> : Singleton<T> where T : class, new()
    {
        public abstract void BindScene(BaseScene baseScene);
    }
}
