using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContainer
{
    public enum JobType
    {
        SwordMan = 1000,
        Wizard,

        Max,
    }
    public enum EffectType
    {
        Damage,
        Buff,
        AbnormalStatus,

        Max
    }

    public enum TargetType
    {
        OneNearbyEnemy,
        Me,
        AllAllies,
        AllEnemies,


        Max,
    }


}
