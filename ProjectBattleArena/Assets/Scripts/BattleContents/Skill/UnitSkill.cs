using DataContainer.Generated;

public class UnitSkill
{
    public SkillsTemplate SkillsTemplate { get; set; }

    public void Invoke(Battle battle)
    {
        foreach(var item in SkillsTemplate.EffectRef)
        {

        }
    }
}