using System;
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

    public enum DamageEffectType
    {
        Normal,
        ProportionalDamageFromLostHp,

        Max
    }


    public enum AbnormalStatusType
    {
        Shield,
        CancelCasting,
        Stun,

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

    public enum GradeType
    {
        Normal,
        Rare,
        Epic,
        Legendary,
        Mythic,

        Max,
    }
    public enum AssetType
    {
        GachaSkill,
        Cash,

        Max,
    }


    public enum GoodsCategory
    {
        GachaSkill,
        Cash,

        Max,
    }


    public enum AttackType
    {
        Melee,
        LongRange,

        Max,
    }

}
