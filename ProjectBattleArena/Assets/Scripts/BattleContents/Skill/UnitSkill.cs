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
                var startSkillEffect = new StartSkillEffectEvent(SkillsTemplate,
                    effect,
                    battle.GetBattleIndex(),
                    battle.GetCurrentTicks());

                battle.GetBattleEventHandler().Process(_invoker, startSkillEffect);

                var targets = battle.GetSkillTargets(_invoker, SkillsTemplate.Range, effect);

                foreach (var target in targets)
                {
                    if (effect.EffectType == DataContainer.EffectType.Damage)
                    {
                        var effectTemplate = TemplateContainer<SkillEffectsDamageTemplate>.Find(effect.EffectPart);

                        InvokeDamageEffect(effectTemplate, target, battle);
                    }
                    else if (effect.EffectType == DataContainer.EffectType.Buff)
                    {
                        InvokeBuffEffect();
                    }
                    else if (effect.EffectType == DataContainer.EffectType.AbnormalStatus)
                    {
                        var effectTemplate = TemplateContainer<SkillEffectsAbnormalStatusTemplate>.Find(effect.EffectPart);

                        InvokeAbnormalStatusEffect(effectTemplate, target, battle);
                    }
                }

                var endSkillEffect = new EndSkillEffectEvent(SkillsTemplate,
                    effect,
                    battle.GetBattleIndex(),
                    battle.GetCurrentTicks());

                battle.GetBattleEventHandler().Process(_invoker, endSkillEffect);
            }
        }
        private void InvokeAbnormalStatusEffect(SkillEffectsAbnormalStatusTemplate abnormalStatusTemplate, Unit target, Battle battle)
        {
            var abnormalStatusEffect = new AbnormalStatusEffect(SkillsTemplate,
                abnormalStatusTemplate,
                _invoker,
                target,
                battle);

            abnormalStatusEffect.Invoke();
        }
        private void InvokeBuffEffect()
        {

        }

        

        private void InvokeDamageEffect(SkillEffectsDamageTemplate skillEffectsDamageTemplate,
                                        Unit target,
                                        Battle battle)
        {
            var damageEffect = new DamageEffect(SkillsTemplate,
                skillEffectsDamageTemplate,
                _invoker,
                target,
                battle);

            damageEffect.Invoke();
        }
    }
}