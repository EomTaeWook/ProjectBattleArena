namespace GameContents
{
    public class CastingSkill
    {
        private int _castingTick = 0;
        private UnitSkill _unitSkill;
        public CastingSkill(UnitSkill unitSkill)
        {
            _unitSkill = unitSkill;
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
