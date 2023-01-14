namespace BA.Models
{
    public class MountingSkillModel
    {
        public int Slot1 { get; set; }
        public int Slot2 { get; set; }
        public int Slot3 { get; set; }
        public int Slot4 { get; set; }
        public int Slot1Exp { get; set; }
        public int Slot2Exp { get; set; }
        public int Slot3Exp { get; set; }
        public int Slot4Exp { get; set; }

        public long GetSlotId(int index)
        {
            switch(index)
            {
                case 0:
                    return Slot1;
                case 1:
                    return Slot2;
                case 2:
                    return Slot3;
                case 3:
                    return Slot4;
                default:
                    return -1;
            }
        }
    }
}
