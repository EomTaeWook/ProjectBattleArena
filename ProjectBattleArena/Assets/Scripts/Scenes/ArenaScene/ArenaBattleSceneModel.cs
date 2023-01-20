using Protocol.GameWebServerAndClient.ShareModels;

public class ArenaBattleSceneModel
{
    public BattleInfoModel BattleInfoModel { get; set; }
}
public class BattleInfoModel
{
    public CharacterData OpponentCharacterData { get; set; }

    public bool IsBattleWin { get; set; }

    public int RandomSeed { get; set; }
}
