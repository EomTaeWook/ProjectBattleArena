using DataContainer.Generated;

namespace GameContents
{
    public class DamageEffect
    {
        public SkillsTemplate SkillsTemplate { get; private set; }
        public SkillEffectsDamageTemplate SkillEffectsDamageTemplate { get; private set; }
        public Unit Invoker { get; private set; }
        public Unit Target { get; private set; }
        public Battle Battle { get; private set; }
        public DamageEffect(SkillsTemplate skillsTemplate,
            SkillEffectsDamageTemplate skillEffectsDamageTemplate,
            Unit invoker,
            Unit target,
            Battle battle)
        {
            SkillsTemplate = skillsTemplate;
            SkillEffectsDamageTemplate = skillEffectsDamageTemplate;
            Invoker = invoker;
            Target = target;
            Battle = battle;
        }
        public void Invoke()
        {
            var factor = SkillEffectsDamageTemplate.HitFactor;
            var maxDamage = GetMaxDamage(Invoker.UnitStats.Attack, factor);

            var minDamage = GetMinDamage(Invoker.UnitStats.Attack, factor);

            var random = Battle.GetRandomGenerator().Next((int)(maxDamage - minDamage));

            double damage = minDamage + random;

            if (SkillEffectsDamageTemplate.DamageEffectType == DataContainer.DamageEffectType.ProportionalDamageFromLostHp)
            {
                var lostHp = Target.UnitStats.Hp.CurrentHp * 1F / Target.UnitStats.Hp.MaxHp;
                damage += damage * lostHp;
            }

            damage -= Target.UnitStats.Defense;

            double critical = Battle.GetRandomGenerator().NextDouble() * 100;

            critical += Target.UnitStats.CriticalResistance;

            if (critical <= Invoker.UnitStats.CriticalRate)
            {
                damage = damage * 1 + Invoker.UnitStats.CriticalDamage;
            }

            if (damage <= 0)
            {
                damage = 1;
            }

            var damageData = new Damage()
            {
                DamageValue = (int)damage,
            };
            double dodgeValue = Battle.GetRandomGenerator().NextDouble() * 100;

            var levelDiff = Target.UnitInfo.Level - Invoker.UnitInfo.Level;
            var levelDodge = 1.0;
            if (levelDiff > 0)
            {
                levelDodge += levelDiff * 0.02;
            }
            else
            {
                levelDodge += levelDiff * 0.01;
            }

            var dodgeRate = Target.UnitStats.DodgeRate * levelDodge;
            var hitRate = Invoker.UnitStats.HitRate - dodgeRate;

            if (dodgeValue > hitRate)
            {
                damageData.IsDodge = true;
                damageData.DamageValue = 0;
                Target.OnDamaged(Invoker, damageData);
                return;
            }

            double block = Battle.GetRandomGenerator().NextDouble() * 100;
            block += Invoker.UnitStats.BlockPenetration;
            if (block <= Target.UnitStats.BlockRate)
            {
                damageData.IsBlock = true;
                damageData.DamageValue = 0;
                Target.OnDamaged(Invoker, damageData);
                return;
            }
            Target.OnDamaged(Invoker, damageData);
        }
        private double GetMinDamage(double attack, int hitFactor)
        {
            return attack * (hitFactor * 0.95) / 100.0;
        }
        private double GetMaxDamage(double attack, int hitFactor)
        {
            return attack * hitFactor / 100.0;
        }
    }
}
