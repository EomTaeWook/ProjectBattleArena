using DataContainer;
using DataContainer.Generated;
using ShareLogic;

namespace GameContents
{
    public abstract class BattleEvent
    {
        public int Ticks { get; private set; }
        public int Id { get; private set; }
        public BattleEvent(int id, int ticks)
        {
            Ticks = ticks;
            Id = id;
        }
    }
    public class TickPassedEvent : BattleEvent
    {
        public TickPassedEvent(int id, int ticks) : base(id, ticks)
        {
        }
    }
    public class EndBattleEvent : BattleEvent
    {
        public bool IsWin { get; private set; }
        public EndBattleEvent(bool isWin, int id, int ticks) : base(id, ticks)
        {
            IsWin = isWin;
        }
    }
    public class MoveEvent : BattleEvent
    {
        public Position OldPosition { get; private set; }
        public Position NewPosition { get; private set; }
        public MoveEvent(Position oldPosition, Position newPosition, int id, int ticks) : base(id, ticks)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
        }
    }

    public class StartSkillEvent : BattleEvent
    {
        public SkillsTemplate SkillsTemplate { get; private set; }
        public StartSkillEvent(SkillsTemplate template, int id, int ticks) : base(id, ticks)
        {
            SkillsTemplate = template;
        }
    }
    public class DamageEvent : BattleEvent
    {
        public Damage Damage { get; private set; }
        public int ShildDamaged { get; private set; }
        public DamageEvent(
            Damage damage,
            int shildDamaged,
            int id,
            int ticks) : base(id, ticks)
        {
            Damage = damage;
            ShildDamaged = shildDamaged;
        }
    }
    public class DieEvent : BattleEvent
    {
        public Unit Attacker { get; private set; }
        public DieEvent(
            Unit attacker,
            int id,
            int ticks) : base(id, ticks)
        {
            Attacker = attacker;
        }
    }
    public class AddAbnormalStatusEvent : BattleEvent
    {
        public AbnormalStatus AbnormalStatus { get; private set; }
        public AddAbnormalStatusEvent(AbnormalStatus abnormalStatus,
            int id,
            int ticks) : base(id, ticks)
        {
            AbnormalStatus = abnormalStatus;
        }
    }
    public class RemoveAbnormalStatusEvent : BattleEvent
    {
        public AbnormalStatusType AbnormalStatusType { get; private set; }
        public RemoveAbnormalStatusEvent(AbnormalStatusType abnormalStatusType, int id, int ticks) : base(id, ticks)
        {
            AbnormalStatusType = abnormalStatusType;
        }
    }
    public class EndSkillEvent : BattleEvent
    {
        public SkillsTemplate SkillsTemplate { get; private set; }
        public EndSkillEvent(SkillsTemplate template, int id, int ticks) : base(id, ticks)
        {
            SkillsTemplate = template;
        }
    }

}
