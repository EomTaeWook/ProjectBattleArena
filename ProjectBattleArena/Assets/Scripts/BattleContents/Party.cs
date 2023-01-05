using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameContents
{
    public class Party
    {
        public Battle Battle { get; private set; }
        private readonly List<Unit> _units = new List<Unit>();
        private bool _isAlly;
        public Party(List<UnitInfo> unitInfoDatas, bool isAlly)
        {
            foreach (var item in unitInfoDatas)
            {
                var unit = new Unit(item);
                _units.Add(unit);
            }

            _isAlly = isAlly;
        }
        public ReadOnlyCollection<Unit> GetUnits()
        {
            return new ReadOnlyCollection<Unit>(_units);
        }
        public bool IsExist(Unit unit)
        {
            for (int i = 0; i < _units.Count; ++i)
            {
                if (_units[i] == unit)
                {
                    return true;
                }
            }
            return false;
        }
        public ICollection<Unit> GetAliveTargets()
        {
            var list = new List<Unit>();

            foreach (var unit in _units)
            {
                if (unit.IsDead() == true)
                {
                    continue;
                }
                list.Add(unit);
            }

            return list;
        }
        public void SetBattle(Battle battle)
        {
            if (battle == null)
            {
                throw new ArgumentNullException(nameof(battle));
            }
            this.Battle = battle;
            var meleeUnits = new List<Unit>();
            var longRangeUnits = new List<Unit>();
            foreach (var unit in _units)
            {
                unit.SetBattle(battle);

                if (unit.CharacterTemplate.AttackType == DataContainer.AttackType.Melee)
                {
                    meleeUnits.Add(unit);
                }
                else if (unit.CharacterTemplate.AttackType == DataContainer.AttackType.LongRange)
                {
                    longRangeUnits.Add(unit);
                }
            }
            var index = 0;
            foreach (var item in meleeUnits)
            {
                var initX = 2 / meleeUnits.Count  - 1;
                if (_isAlly == true)
                {
                    item.Move(initX + index, 2);
                }
                else
                {
                    item.Move(initX + index, 1);
                }
                index++;
            }
            index = 0;

            foreach (var item in longRangeUnits)
            {
                var initX = 2 / longRangeUnits.Count - 1;
                if (_isAlly == true)
                {
                    item.Move(initX + index, 3);
                }
                else
                {
                    item.Move(initX + index, 0);
                }
                index++;
            }
        }
        public void OnTickPassed()
        {
            foreach (var unit in _units)
            {
                if (unit.IsDead() == true)
                {
                    continue;
                }
                unit.OnTickPassed();
            }
        }
        public bool IsAlly()
        {
            return _isAlly;
        }
    }
}
