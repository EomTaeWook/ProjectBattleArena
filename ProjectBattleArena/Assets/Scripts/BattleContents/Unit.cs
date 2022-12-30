using DataContainer.Generated;
using Protocol.GameWebServerAndClient.ShareModels;
using System.Collections.Generic;
using System.Numerics;
using TemplateContainers;

public class Unit
{
    private int _attackedStackPoint = 0;

    private int _usedSkillIndex = 0;

    private float _attackedRemainTicks = 0;

    private Vector2 _position;

    private CharacterTemplate _characterTemplate;

    public UnitStats _unitStats;

    private CastingSkill _castingSkill;

    private List<UnitSkill> _skillDatas = new List<UnitSkill>();

    private UnitSkill baseAttackSkill;
    private Battle _battle;
    public Unit(CharacterData characterData)
    {
        _characterTemplate = TemplateContainer<CharacterTemplate>.Find((int)characterData.Job);
        _unitStats = new UnitStats(_characterTemplate, characterData);

        MakeSkills(characterData.EquippedSkillDatas);
    }

    public void SetBattle(Battle battle)
    {
        _battle = battle;
    }

    public void MakeSkills(List<SkillData> equippedSkillDatas)
    {
        baseAttackSkill = new UnitSkill()
        {
            SkillsTemplate = _characterTemplate.BaseAttackSkillRef
        };

        foreach (var item in equippedSkillDatas)
        {
            var skillsTemplate = TemplateContainer<SkillsTemplate>.Find(item.SkillTemplate);
            _skillDatas.Add(new UnitSkill()
            {
                SkillsTemplate = skillsTemplate
            });
        }
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

        if(_attackedRemainTicks < 0)
        {
            _attackedRemainTicks = 0;
        }

        if (IsCasting() == false)
        {
            _castingSkill.DecreaseTick();

            if(_castingSkill.IsFinished() == true)
            {
                _castingSkill = null;
            }
        }
    }
    public UnitSkill GetSkillsOfNextIndex()
    {
        if(_skillDatas.Count == 0)
        {
            return null;
        }
        if(_skillDatas.Count <= _usedSkillIndex)
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

        if (nextSkill.SkillsTemplate.NeedCost > _attackedStackPoint)
        {
            _attackedStackPoint++;
            UseSkill(baseAttackSkill);
        }
        else
        {
            _attackedStackPoint = 0;
            _usedSkillIndex++;
            UseSkill(nextSkill);
        }
    }

    private void UseSkill(UnitSkill unitSkill)
    {
        StartSkillEvent startSkillEvent = new StartSkillEvent(unitSkill.SkillsTemplate, _battle.GetBattleIndex(), _battle.GetCurrentTicks());

        _attackedRemainTicks = _unitStats.AttackSpeed;

        unitSkill.Invoke(_battle);

        EndSkillEvent endSkillEvent = new EndSkillEvent(unitSkill.SkillsTemplate, _battle.GetBattleIndex(), _battle.GetCurrentTicks());
    }
    

    public void OnDamaged(Unit attacker, Damage damage)
    {
        
    }

}