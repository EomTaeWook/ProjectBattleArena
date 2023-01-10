using DataContainer.Generated;
using Kosher.Log;

namespace GameContents
{
    public class AbnormalStatusEffect
    {
        public SkillsTemplate SkillsTemplate { get; private set; }
        public SkillEffectsAbnormalStatusTemplate SkillEffectsAbnormalStatusTemplate { get; private set; }
        public Unit Invoker { get; private set; }
        public Unit Target { get; private set; }
        public Battle Battle { get; private set; }
        public AbnormalStatusEffect(SkillsTemplate skillsTemplate,
            SkillEffectsAbnormalStatusTemplate skillEffectsAbnormalStatusTemplate,
            Unit invoker,
            Unit target,
            Battle battle)
        {
            SkillsTemplate = skillsTemplate;
            SkillEffectsAbnormalStatusTemplate = skillEffectsAbnormalStatusTemplate;
            Invoker = invoker;
            Target = target;
            Battle = battle;
        }
        public void Invoke()
        {
            AbnormalStatus abnormalStatus;
            if (SkillEffectsAbnormalStatusTemplate.AbnormalStatusType == DataContainer.AbnormalStatusType.ProportionalDamageFromLostHp)
            {
                var lostHpRate = Target.UnitStats.Hp.CurrentHp * 1.0F / Target.UnitStats.Hp.MaxHp;

                abnormalStatus = new AbnormalStatus(
                    SkillsTemplate,
                    SkillEffectsAbnormalStatusTemplate.AbnormalStatusType,
                    (int)lostHpRate,
                    SkillEffectsAbnormalStatusTemplate.Duration);
            }
            else
            {
                LogHelper.Error($"not process abnormal status type - {SkillEffectsAbnormalStatusTemplate.AbnormalStatusType}");
                return;
            }
            if (abnormalStatus != null)
            {
                var addAbnormalStatusEvent = new AddAbnormalStatusEvent(abnormalStatus,
                    Battle.GetBattleIndex(),
                    Battle.GetCurrentTicks());

                Target.AddAbnormalStatus(abnormalStatus);
                Battle.GetBattleEventHandler().Process(Target, addAbnormalStatusEvent);
            }
            else
            {

            }
        }
    }
}
