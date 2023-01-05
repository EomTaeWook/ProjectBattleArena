using DataContainer.Generated;
using TemplateContainers;

namespace GameContents
{
    public class UnitSkill
    {
        public SkillsTemplate SkillsTemplate { get; private set; }

        private readonly Unit _invoker;
        public UnitSkill(Unit unit, SkillsTemplate skillsTemplate)
        {
            _invoker = unit;
            SkillsTemplate = skillsTemplate;
        }
        public void Invoke(Battle battle)
        {
            foreach(var effect in SkillsTemplate.EffectRef)
            {
                var targets = battle.GetSkillTargets(_invoker, SkillsTemplate.Range, effect);

                foreach (var target in targets)
                {
                    if (effect.EffectType == DataContainer.EffectType.Damage)
                    {
                        var tempalte = TemplateContainer<SkillEffectsDamageTemplate>.Find(effect.EffectPart);

                        InvokeDamageEffect(tempalte, target, battle);
                    }
                    else if (effect.EffectType == DataContainer.EffectType.Buff)
                    {
                        InvokeBuffEffect();
                    }
                    else if (effect.EffectType == DataContainer.EffectType.AbnormalStatus)
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

        private double GetMinDamage(double attack, int hitFactor)
        {
            return attack * (hitFactor * 0.95) / 100.0;
        }
        private double GetMaxDamage(double attack, int hitFactor)
        {
            return attack * hitFactor / 100.0;
        }

        private void InvokeDamageEffect(SkillEffectsDamageTemplate skillEffectsDamageTemplate,
                                        Unit target,
                                        Battle battle)
        {
            var factor = skillEffectsDamageTemplate.HitFactor;

            var maxDamage = GetMaxDamage(_invoker.UnitStats.Attack, factor);

            var minDamage = GetMinDamage(_invoker.UnitStats.Attack, factor);

            var random = battle.GetRandomGenerator().Next((int)(maxDamage - minDamage));

            double damage = minDamage + random;

            damage -= target.UnitStats.Defense;

            double critical = battle.GetRandomGenerator().NextDouble() * 100;

            critical += target.UnitStats.CriticalResistance;

            if (critical <= _invoker.UnitStats.CriticalRate)
            {
                damage = damage * 1 + _invoker.UnitStats.CriticalDamage;
            }
            
            if(damage <= 0)
            {
                damage = 1;
            }

            var damageData = new Damage()
            {
                DamageValue = (int)damage,
            };

            double dodgeValue = battle.GetRandomGenerator().NextDouble() * 100;

            var levelDiff = target.UnitInfo.Level - _invoker.UnitInfo.Level;
            var levelDodge = 1.0;
            if(levelDiff > 0)
            {
                levelDodge += levelDiff * 0.02;
            }
            else
            {
                levelDodge += levelDiff * 0.01;
            }

            var dodgeRate = target.UnitStats.DodgeRate * levelDodge;
            var hitRate = _invoker.UnitStats.HitRate - dodgeRate;

            if(dodgeValue > hitRate)
            {
                damageData.IsDodge = true;
                damageData.DamageValue = 0;
                target.OnDamaged(_invoker, damageData);
                return;
            }

            double block = battle.GetRandomGenerator().NextDouble() * 100;
            block += _invoker.UnitStats.BlockPenetration;
            if (block <= target.UnitStats.BlockRate)
            {
                damageData.IsBlock = true;
                damageData.DamageValue = 0;
                target.OnDamaged(_invoker, damageData);
                return;
            }
            target.OnDamaged(_invoker, damageData);
        }
    }
}