using DataContainer;
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
            AbnormalStatus abnormalStatus = null;
            if (SkillEffectsAbnormalStatusTemplate.AbnormalStatusType == AbnormalStatusType.CancelCasting)
            {
                if (Target.IsCasting() == true)
                {
                    abnormalStatus = new AbnormalStatus(SkillsTemplate,
                         SkillEffectsAbnormalStatusTemplate.AbnormalStatusType,
                         0,
                         SkillEffectsAbnormalStatusTemplate.Duration
                         );
                }
            }
            else if(SkillEffectsAbnormalStatusTemplate.AbnormalStatusType == AbnormalStatusType.Stun)
            {
                abnormalStatus = new AbnormalStatus(SkillsTemplate,
                         SkillEffectsAbnormalStatusTemplate.AbnormalStatusType,
                         0,
                         SkillEffectsAbnormalStatusTemplate.Duration
                         );
            }
            else if(SkillEffectsAbnormalStatusTemplate.AbnormalStatusType == AbnormalStatusType.Shield)
            {
                
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
                //not invoke
            }
        }
    }
}
