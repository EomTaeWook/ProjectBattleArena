using DataContainer.Generated;

namespace GameContents
{
    public class CastingSkill
    {
        private int _castingTick = 0;
        public UnitSkill UnitSkill { get; private set; }
        public CastingSkill(UnitSkill unitSkill, int castingTicks)
        {
            UnitSkill = unitSkill;
            _castingTick = castingTicks;
        }
        
        public void DecreaseTick()
        {
            _castingTick--;
        }
        public bool IsFinished()
        {
            return _castingTick <= 0;
        }
    }
}
