using DataContainer.Generated;
using Kosher.Log;
using System.Collections.Generic;
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
        var maxRangeY = _invoker.Position.Y + SkillsTemplate.Range;

        var minRangeY = _invoker.Position.Y - SkillsTemplate.Range;

        var maxRangeX = _invoker.Position.X + SkillsTemplate.Range;

        var minRangeX = _invoker.Position.X - SkillsTemplate.Range;

        foreach (var item in SkillsTemplate.Effets)
        {
            var targetCandidates = new List<Unit>();

            if (item.TargetType == DataContainer.TargetType.AllAllies)
            {
                var party = battle.GetMyParty(_invoker);
                targetCandidates.AddRange(party.GetAliveTargets());
            }
            else if(item.TargetType == DataContainer.TargetType.AllEnemies)
            {
                var party = battle.GetOpponentParty(_invoker);
                targetCandidates.AddRange(party.GetAliveTargets());
            }
            else if(item.TargetType == DataContainer.TargetType.Me)
            {
                targetCandidates.Add(_invoker);
            }
            else if (item.TargetType == DataContainer.TargetType.HighAggro)
            {
                var party = battle.GetOpponentParty(_invoker);
                var targetCandidate = party.GetAliveTargets();
                Unit targetUnit = null;
                var aggro = 0;
                foreach (var opponentUnit in targetCandidate)
                {
                    if(aggro < opponentUnit.GetAggroGauge())
                    {
                        targetUnit = opponentUnit;
                        aggro = opponentUnit.GetAggroGauge();
                    }
                }
                if (targetUnit != null)
                {
                    targetCandidates.Add(targetUnit);
                }
            }
            else
            {
                LogHelper.Error($"invalid target type : {item.TargetType}");
                return;
            }

            var targetUnits = new List<Unit>();

            foreach(var target in targetCandidates)
            {
                if(target.Position.Y >= minRangeY && target.Position.Y <= maxRangeY &&
                    target.Position.X >= minRangeX && target.Position.X <= maxRangeX)
                {
                    targetUnits.Add(target);
                }
            }


 
            foreach(var target in targetUnits)
            {
                if (item.EffectRef.EffectType == DataContainer.EffectType.Damage)
                {
                    var tempalte = TemplateContainer<SkillEffectsDamageTemplate>.Find(item.EffectRef.EffectPart);

                    InvokeDamageEffect(tempalte, target, battle);
                }
                else if(item.EffectRef.EffectType == DataContainer.EffectType.Buff)
                {
                    InvokeBuffEffect();
                }
                else if(item.EffectRef.EffectType == DataContainer.EffectType.AbnormalStatus)
                {
                    InvokeAbnormalStatusEffect();
                }
            }
        }
    }
    private void InvokeAbnormalStatusEffect()
    {

    }
    private void InvokeBuffEffect()
    {

    }
    private void InvokeDamageEffect(SkillEffectsDamageTemplate skillEffectsDamageTemplate,
                                    Unit target,
                                    Battle battle)
    {
        var factor = skillEffectsDamageTemplate.HitFactor;

        double damage = _invoker.UnitStats.Attack * factor / 100;

        double critical = battle.GetRandomGenerator().Next();

        critical += target.UnitStats.CriticalResistance;

        if (critical <= _invoker.UnitStats.CriticalRate)
        {
            damage *= _invoker.UnitStats.CriticalDamage;
        }

        damage -= target.UnitStats.Defense;

        var damageData = new Damage()
        {
            DamageValue = (int)damage,
        };

        double hit = battle.GetRandomGenerator().Next();

        hit += target.UnitStats.Dodge;

        if (hit >= _invoker.UnitStats.HitRate)
        {
            damageData.IsDodge = true;
            damageData.DamageValue = 0;
            target.OnDamaged(_invoker, damageData);
            return;
        }

        double block = battle.GetRandomGenerator().Next();
        block += _invoker.UnitStats.BlockPenetration;
        if (block <= target.UnitStats.BlockRate)
        {
            damageData.IsDodge = true;
            damageData.DamageValue = 0;
            target.OnDamaged(_invoker, damageData);
            return;
        }
        target.OnDamaged(_invoker, damageData);
    }
}