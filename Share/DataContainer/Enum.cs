﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContainer
{
    public enum JobType
    {
        Tanker,
        Attacker,
        Healer,

        Max,
    }
    public enum EffectType
    {
        Damage,
        Buff,
        AbnormalStatus,

        Max
    }

    public enum AbnormalStatusType
    {
        Shield,

        Max,
    }


    public enum TargetType
    {
        HighAggro,
        Me,
        AllAllies,
        AllEnemies,


        Max,
    }


}
