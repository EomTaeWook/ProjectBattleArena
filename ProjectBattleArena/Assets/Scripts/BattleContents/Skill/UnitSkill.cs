using DataContainer.Generated;
using Kosher.Log;
using System.Collections.Generic;
using System.Numerics;
using TemplateContainers;

public class UnitSkill
{
    public SkillsTemplate SkillsTemplate { get; private set; }

    private Unit _invoker;
    public UnitSkill(Unit unit, SkillsTemplate skillsTemplate)
    {
        _invoker = unit;
        SkillsTemplate = skillsTemplate;
    }

    public void Invoke(Battle battle)
    {
        foreach(var item in SkillsTemplate.Effets)
        {
            var targets = new List<Unit>();
            if(item.TargetType == DataContainer.TargetType.AllAllies)
            {
                var party = battle.GetMyParty(_invoker);

                targets.AddRange(party.GetAliveTargets());
            }
            else if(item.TargetType == DataContainer.TargetType.AllEnemies)
            {
                var party = battle.GetOpponentParty(_invoker);

                targets.AddRange(party.GetAliveTargets());
            }
            else if(item.TargetType == DataContainer.TargetType.Me)
            {
                targets.Add(_invoker);
            }
            else if (item.TargetType == DataContainer.TargetType.OneNearbyEnemy)
            {
                var party = battle.GetOpponentParty(_invoker);
                var targetCandidate = party.GetAliveTargets();
                float minDistance = float.MaxValue;
                Unit targetUnit = null;
                foreach (var opponentUnit in targetCandidate)
                {
                    var position = Vector2.Distance(opponentUnit.Position, _invoker.Position);

                    if(position < minDistance)
                    {
                        minDistance = position;
                        targetUnit = opponentUnit;
                    }
                }
                if(targetUnit == null)
                {
                    LogHelper.Error($"target unit is null");
                }
                targets.Add(targetUnit);
            }
            else
            {
                LogHelper.Error($"invalid target type : {item.TargetType}");
            }

            if (item.EffectRef.EffectType == DataContainer.EffectType.Damage)
            {
                var tempalte = TemplateContainer<SkillEffectsDamageTemplate>.Find(item.EffectRef.EffectPart);

                
            }
        }
    }



    private void InvokeDamageEffect(SkillEffectsDamageTemplate skillEffectsDamageTemplate, Unit target)
    {

    }
}