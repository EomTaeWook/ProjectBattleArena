namespace Assets.Scripts.Scenes
{
    internal interface ISceneModel<TModel> where TModel : class, new()
    {
        TModel SceneModel { get; set; }
    }
}
