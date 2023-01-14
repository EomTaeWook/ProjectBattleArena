namespace Assets.Scripts.Models
{
    public enum AnimationType
    {
        Idle,
        Move,
        Die,
        Attack1,
        Attack2,

        Max
    }
    public enum SceneType
    {
        TitleScene,
        SelectCharacterScene,
        CreateCharacterScene,
        MainScene,
        StageScene,
        ArenaBattleScene,

        Max
    }
    public enum ServerType
    {
        Local,
        Dev,

        Max,
    }
    public enum AlertPopupType
    {
        Alert,
        Confirm,

        Max,
    }

}
