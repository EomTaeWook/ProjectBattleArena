using DataContainer;
using DataContainer.Generated;

namespace GameContents
{
    public class AbnormalStatus
    {
        public SkillsTemplate SkillsTemplate { get; private set; }
        public AbnormalStatusType AbnormalStatusType { get; private set; }
        public int Value { get; private set; }
        public int DurationTicks { get; private set; }
        public AbnormalStatus(SkillsTemplate skillsTemplate, AbnormalStatusType abnormalStatusType, int value, int duration)
        {
            SkillsTemplate = skillsTemplate;
            AbnormalStatusType = abnormalStatusType;
            Value = value;
            DurationTicks = duration;
        }
        public void Refresh(int value, int duration)
        {
            Value = value;
            DurationTicks = duration;
        }
        public void DecreaseTicks()
        {
            DurationTicks--;
        }
    }
}
