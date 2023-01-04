﻿using DataContainer;
using DataContainer.Generated;
using ShareLogic;

namespace GameContents
{
    public class Unit
    {
        public bool IsNpc { get; private set; }
        public Position Position { get; private set; } = new Position();
        public UnitStats UnitStats { get; private set; }
        public int GetAggroGauge()
        {
            return _aggroGauge;
        }

        public UnitInfo UnitInfo { get; private set; }
        public CharacterTemplate CharacterTemplate { get => this.UnitInfo.CharacterTemplate; }

        private int _aggroGauge;
        private int _attackStackedPoint = 0;
        private int _usedSkillIndex = 0;
        private float _attackedRemainTicks = 0;

        private CastingSkill _castingSkill;
        private List<UnitSkill> _skillDatas = new List<UnitSkill>();
        private UnitSkill baseAttackSkill;
        private Battle _battle;

        public Unit(UnitInfo unitInfo)
        {
            UnitInfo = unitInfo;
            UnitStats = new UnitStats(unitInfo);
            MakeSkills(unitInfo.EquippedSkillDatas);
            if (CharacterTemplate.JobType == DataContainer.JobType.Tanker)
            {
                _aggroGauge = ConstHelper.TankAggro;
            }
            else if (CharacterTemplate.JobType == DataContainer.JobType.Attacker)
            {
                _aggroGauge = ConstHelper.AttakerAggro;
            }
            else if (CharacterTemplate.JobType == DataContainer.JobType.Healer)
            {
                _aggroGauge = ConstHelper.HealerAggro;
            }
        }
        public void Move(int diffX, int diffY)
        {
            Position.X += diffX;
            Position.Y += diffY;
        }
        public void ModifyAggro(int diff)
        {
            _aggroGauge += diff;
            var minAggro = 0;

            if (CharacterTemplate.JobType == DataContainer.JobType.Tanker)
            {
                minAggro = ConstHelper.TankAggro;
            }
            else if (CharacterTemplate.JobType == DataContainer.JobType.Attacker)
            {
                minAggro = ConstHelper.AttakerAggro;
            }
            else if (CharacterTemplate.JobType == DataContainer.JobType.Healer)
            {
                minAggro = ConstHelper.HealerAggro;
            }

            if (_aggroGauge <= minAggro)
            {
                _aggroGauge = minAggro;
            }

        }
        public void SetBattle(Battle battle)
        {
            _battle = battle;
        }

        public void MakeSkills(List<SkillsTemplate> equippedSkillDatas)
        {
            baseAttackSkill = new UnitSkill(this, CharacterTemplate.BaseAttackSkillRef);

            foreach (var item in equippedSkillDatas)
            {
                _skillDatas.Add(new UnitSkill(this, item));
            }
        }
        public bool IsDead()
        {
            return UnitStats.Hp.CurrentHp == 0;
        }
        public bool IsCasting()
        {
            return _castingSkill != null;
        }

        public void OnTickPassed()
        {
            if (_attackedRemainTicks > 0)
            {
                _attackedRemainTicks--;
            }

            if (_attackedRemainTicks < 0)
            {
                _attackedRemainTicks = 0;
            }

            if (IsCasting() == true)
            {
                _castingSkill.DecreaseTick();

                if (_castingSkill.IsFinished() == true)
                {
                    _castingSkill = null;
                }
            }
        }
        public UnitSkill GetSkillsOfNextIndex()
        {
            if (_skillDatas.Count == 0)
            {
                return null;
            }
            if (_skillDatas.Count <= _usedSkillIndex)
            {
                _usedSkillIndex = 0;
            }
            return _skillDatas[_usedSkillIndex];
        }

        public void DoAction()
        {
            if (_attackedRemainTicks > 0)
            {
                return;
            }

            var nextSkill = GetSkillsOfNextIndex();

            if (nextSkill == null)
            {
                UseSkill(baseAttackSkill);
                return;
            }

            if (nextSkill.SkillsTemplate.NeedCost > _attackStackedPoint)
            {
                _attackStackedPoint++;
                UseSkill(baseAttackSkill);
            }
            else
            {
                _attackStackedPoint = 0;
                _usedSkillIndex++;
                UseSkill(nextSkill);
            }
        }

        private void UseSkill(UnitSkill unitSkill)
        {
            StartSkillEvent startSkillEvent = new StartSkillEvent(unitSkill.SkillsTemplate, _battle.GetBattleIndex(), _battle.GetCurrentTicks());

            _attackedRemainTicks = 3 * (UnitStats.AttackSpeed / 100);

            unitSkill.Invoke(this._battle);

            EndSkillEvent endSkillEvent = new EndSkillEvent(unitSkill.SkillsTemplate, _battle.GetBattleIndex(), _battle.GetCurrentTicks());
        }


        public void OnDamaged(Unit attacker, Damage damage)
        {
            if (damage.DamageValue <= 0)
            {
                _battle.GetBattleEventHandler().Process(new DamageEvent(damage,
                    0,
                    _battle.GetBattleIndex(),
                    _battle.GetCurrentTicks()));
                return;
            }
            var shieldDiff = 0;
            if (UnitStats.Hp.Shield > 0)
            {
                shieldDiff = damage.DamageValue;
                if (UnitStats.Hp.Shield < shieldDiff)
                {
                    shieldDiff = UnitStats.Hp.Shield;
                    damage.DamageValue = -shieldDiff;
                    _battle.GetBattleEventHandler().Process(new RemoveAbnormalStatusEvent(AbnormalStatusType.Shield,
                        _battle.GetBattleIndex(),
                        _battle.GetCurrentTicks()));
                }
                UnitStats.Hp.ModifyShield(-shieldDiff);
            }
            UnitStats.Hp.ModifyHp(-damage.DamageValue);

            _battle.GetBattleEventHandler().Process(new DamageEvent(damage,
                shieldDiff,
                _battle.GetBattleIndex(),
                _battle.GetCurrentTicks()));
        }

    }
}
