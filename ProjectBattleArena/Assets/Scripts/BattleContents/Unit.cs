using DataContainer;
using DataContainer.Generated;
using ShareLogic;
using System.Collections.Generic;

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

        private readonly List<AbnormalStatus> _abnormalStatusHolder = new List<AbnormalStatus>();
        private readonly List<AbnormalStatus> _oneTimeAbnormalStatusHolder = new List<AbnormalStatus>();

        public float GetAttackedRemainTicks()
        {
            return _attackedRemainTicks;
        }

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
        public void AddAbnormalStatus(AbnormalStatus abnormalStatus)
        {
            AbnormalStatus find = null;
            foreach(var item in _abnormalStatusHolder)
            {
                if(item.AbnormalStatusType == abnormalStatus.AbnormalStatusType)
                {
                    find = item;
                    break;
                }
            }
            if(find == null)
            {
                _abnormalStatusHolder.Add(abnormalStatus);
            }
            else
            {
                var duration = abnormalStatus.DurationTicks > find.DurationTicks ? abnormalStatus.DurationTicks : find.DurationTicks;
                var value = abnormalStatus.Value > find.Value ? abnormalStatus.Value : find.Value;
                find.Refresh(value, duration);
            }
        }
        public void RemoveAbnormalStatus(AbnormalStatusType abnormalStatusType)
        {
            AbnormalStatus find = null;
            foreach (var item in _abnormalStatusHolder)
            {
                if (item.AbnormalStatusType == abnormalStatusType)
                {
                    find = item;
                    break;
                }
            }
            _abnormalStatusHolder.Remove(find);
        }
        public bool IsOnAbnormalStatus(AbnormalStatusType abnormalStatusType)
        {
            foreach (var item in _abnormalStatusHolder)
            {
                if(item.AbnormalStatusType == abnormalStatusType)
                {
                    return true;
                }
            }

            return false;
        }
        public AbnormalStatus GetAbnormalStatus(AbnormalStatusType abnormalStatusType)
        {
            foreach (var item in _abnormalStatusHolder)
            {
                if (item.AbnormalStatusType == abnormalStatusType)
                {
                    return item;
                }
            }
            return null;
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
        public void CancleCasting()
        {
            _castingSkill = null;
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

            var removeAbnormalStatus = new List<AbnormalStatus>();
            foreach (var item in _abnormalStatusHolder)
            {
                item.DecreaseTicks();
                if (item.DurationTicks <= 0)
                {
                    removeAbnormalStatus.Add(item);
                }
            }

            foreach (var remove in removeAbnormalStatus)
            {
                _abnormalStatusHolder.Remove(remove);
            }
            if (IsCasting() == true)
            {
                _castingSkill.DecreaseTick();

                if (_castingSkill.IsFinished() == true)
                {
                    UseSkill(_castingSkill.UnitSkill);
                    _castingSkill = null;
                }
            }
        }
        private UnitSkill GetSkillsOfNextIndex()
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

            if (IsDead() == true)
            {
                return;
            }

            if(IsCasting() == true)
            {
                return;
            }

            UnitSkill nextSkill = GetSkillsOfNextIndex();

            nextSkill ??= baseAttackSkill;

            var canUsed = false;
            foreach(var item in nextSkill.SkillsTemplate.EffectRef)
            {
                var targets = _battle.GetSkillTargets(this,
                    nextSkill.SkillsGroupTemplate.Range,
                    item);

                if(targets.Count > 0)
                {
                    canUsed = true;
                    break;
                }
            }
            if(canUsed == false)
            {
                var party = _battle.GetMyParty(this);

                var oldPosition = new Position()
                {
                    X = Position.X,
                    Y = Position.Y
                };

                if (party.IsAlly() == true)
                {
                    this.Move(0, -1);
                }
                else
                {
                    this.Move(0, 1);
                }

                var newPosition = new Position()
                {
                    X = Position.X,
                    Y = Position.Y
                };

                _battle.GetBattleEventHandler().Process(this,
                                            new MoveEvent(
                                                oldPosition,
                                                newPosition,
                                                _battle.GetBattleIndex(),
                                                _battle.GetCurrentTicks()));


                _attackedRemainTicks = ConstHelper.BaseAttackTicks;
                return;
            }

            if (nextSkill.SkillsGroupTemplate.NeedCost > _attackStackedPoint)
            {
                _attackStackedPoint++;
                UseSkill(baseAttackSkill);
            }
            else
            {
                _attackStackedPoint = 0;
                _usedSkillIndex++;

                if(nextSkill.SkillsGroupTemplate.IsCasting == true)
                {
                    UseCastingSkill(nextSkill);
                }
                else
                {
                    UseSkill(nextSkill);
                }
            }
        }

        private void UseSkill(UnitSkill unitSkill)
        {
            StartSkillEvent startSkillEvent = new StartSkillEvent(unitSkill.SkillsTemplate, _battle.GetBattleIndex(), _battle.GetCurrentTicks());

            _battle.GetBattleEventHandler().Process(this, startSkillEvent);

            _attackedRemainTicks = ConstHelper.BaseAttackTicks * (UnitStats.AttackSpeed / 100);

            unitSkill.Invoke(this._battle);

            EndSkillEvent endSkillEvent = new EndSkillEvent(unitSkill.SkillsTemplate, _battle.GetBattleIndex(), _battle.GetCurrentTicks());

            _battle.GetBattleEventHandler().Process(this, endSkillEvent);
        }
        private void UseCastingSkill(UnitSkill castingSkill)
        {
            var skill = new CastingSkill(castingSkill, castingSkill.SkillsGroupTemplate.CastingTime / ConstHelper.DefaultPerTicks);

            _castingSkill = skill;

            StartCastingSkillEvent startCastingSkillEvent = new StartCastingSkillEvent(skill.UnitSkill.SkillsTemplate, _battle.GetBattleIndex(), _battle.GetCurrentTicks());

            _battle.GetBattleEventHandler().Process(this, startCastingSkillEvent);

            EndCastingSkillEvent endCastingSkillEvent = new EndCastingSkillEvent(skill.UnitSkill.SkillsTemplate, _battle.GetBattleIndex(), _battle.GetCurrentTicks());

            _battle.GetBattleEventHandler().Process(this, endCastingSkillEvent);
        }

        public void OnDamaged(Unit attacker, Damage damage)
        {
            if (damage.DamageValue <= 0)
            {
                _battle.GetBattleEventHandler().Process(this, new DamageEvent(damage,
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
                    _battle.GetBattleEventHandler().Process(this, 
                        new RemoveAbnormalStatusEvent(AbnormalStatusType.Shield,
                        _battle.GetBattleIndex(),
                        _battle.GetCurrentTicks()));
                }
                UnitStats.Hp.ModifyShield(-shieldDiff);
            }
            UnitStats.Hp.ModifyHp(-damage.DamageValue);

            _battle.GetBattleEventHandler().Process(this, new DamageEvent(damage,
                shieldDiff,
                _battle.GetBattleIndex(),
                _battle.GetCurrentTicks()));

            if(UnitStats.Hp.CurrentHp == 0)
            {
                _battle.GetBattleEventHandler().Process(this, new DieEvent(attacker,
                _battle.GetBattleIndex(),
                _battle.GetCurrentTicks()));
            }
        }
    }
}
