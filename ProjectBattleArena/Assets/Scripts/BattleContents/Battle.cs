using DataContainer.Generated;
using Kosher.Log;
using ShareLogic.Random;
namespace GameContents
{
    public class Battle
    {
        private const int DefaultPerTicks = 33; //33ms;
        private readonly RandomGenerator _randomGenerator;
        private long _startedTime;
        private int _currentTicks;
        private readonly Party _allyParty;
        private readonly Party _enemyParty;
        private IBattleEventHandler _battleEventHandler;
        private int _battleEventIndex = 0;
        private readonly int[,] _map = new int[4, 4];
        private bool _isBattleEnd = false;
        public int GetMapMaxX()
        {
            return 4;
        }
        public int GetMapMaxY()
        {
            return 4;
        }
        public bool IsWin()
        {
            return _enemyParty.GetAliveTargets().Count == 0;
        }
        public Battle(IBattleEventHandler battleEventHandler,
            int randomSeed,
            List<UnitInfo> allies,
            List<UnitInfo> enemies
            )
        {
            _battleEventHandler = battleEventHandler;
            _randomGenerator = new RandomGenerator(randomSeed);
            _allyParty = new Party(allies, true);
            _enemyParty = new Party(enemies, false);

            _allyParty.SetBattle(this);
            _enemyParty.SetBattle(this);

            _startedTime = DateTime.Now.Ticks;
        }

        public Party GetAllyParty()
        {
            return _allyParty;
        }
        public Party GetEnemyParty()
        {
            return _enemyParty;
        }
        public int GetBattleIndex()
        {
            return _battleEventIndex++;
        }
        public int GetCurrentTicks()
        {
            return _currentTicks;
        }

        public void ProcessTicks()
        {
            if(_isBattleEnd == true)
            {
                return;
            }

            var elapsedTime = TimeSpan.FromTicks(DateTime.Now.Ticks - this._startedTime).TotalMilliseconds;

            int elapsedTickCount = (int)(elapsedTime / DefaultPerTicks);

            for (int i = 0; i < elapsedTickCount; ++i)
            {
                TickPassedEvent tickPassedEvent = new TickPassedEvent(
                    GetBattleIndex(),
                    _currentTicks);
                _battleEventHandler.Process(tickPassedEvent);
                _currentTicks++;
                DoAction();

                if(_allyParty.GetAliveTargets().Count == 0)
                {
                    EndBattleEvent endBattleEvent = new EndBattleEvent(
                        false,
                        GetBattleIndex(),
                        _currentTicks);
                    _battleEventHandler.Process(endBattleEvent);
                    _isBattleEnd = true;
                    break;
                }
                else if(_enemyParty.GetAliveTargets().Count == 0)
                {
                    EndBattleEvent endBattleEvent = new EndBattleEvent(
                        true,
                        GetBattleIndex(),
                        _currentTicks);
                    _battleEventHandler.Process(endBattleEvent);
                    _isBattleEnd = true;
                    break;
                }
            }
        }

        public List<Unit> GetSkillTargets(Unit invoker,
            int range,
            SkillEffectsTemplate skillEffectsTemplate )
        {
            var targetCandidates = new List<Unit>();

            var maxRangeY = invoker.Position.Y + range + 1;

            var minRangeY = invoker.Position.Y - range - 1;

            var maxRangeX = invoker.Position.X + range + 1;

            var minRangeX = invoker.Position.X - range - 1;

            if (skillEffectsTemplate.TargetType == DataContainer.TargetType.AllAllies)
            {
                var party = GetMyParty(invoker);
                targetCandidates.AddRange(party.GetAliveTargets());
            }
            else if (skillEffectsTemplate.TargetType == DataContainer.TargetType.AllEnemies)
            {
                var party = GetOpponentParty(invoker);
                targetCandidates.AddRange(party.GetAliveTargets());
            }
            else if (skillEffectsTemplate.TargetType == DataContainer.TargetType.Me)
            {
                targetCandidates.Add(invoker);
            }
            else if (skillEffectsTemplate.TargetType == DataContainer.TargetType.HighAggro)
            {
                var party = GetOpponentParty(invoker);
                var targetCandidate = party.GetAliveTargets();
                Unit targetUnit = null;
                var aggro = 0;
                foreach (var opponentUnit in targetCandidate)
                {
                    if (aggro < opponentUnit.GetAggroGauge())
                    {
                        targetUnit = opponentUnit;
                        aggro = opponentUnit.GetAggroGauge();
                    }
                }
                if (targetUnit != null)
                {
                    targetCandidates.Add(targetUnit);
                }
            }

            var targetUnits = new List<Unit>();

            foreach (var target in targetCandidates)
            {
                if (target.Position.Y >= minRangeY && target.Position.Y <= maxRangeY &&
                    target.Position.X >= minRangeX && target.Position.X <= maxRangeX)
                {
                    targetUnits.Add(target);
                }
            }
            return targetUnits;
        }

        public IBattleEventHandler GetBattleEventHandler()
        {
            return _battleEventHandler;
        }
        public bool IsBattleEnd()
        {
            return _isBattleEnd;
        }
        private void DoAction()
        {
            _allyParty.OnTickPassed();
            _enemyParty.OnTickPassed();

            List<Unit> actionUnits = new List<Unit>();

            foreach(var item in _allyParty.GetUnits())
            {
                if (item.IsDead() == true)
                {
                    continue;
                }
                if(item.GetAttackedRemainTicks() <=0)
                {
                    actionUnits.Add(item);
                }
            }
            foreach (var item in _enemyParty.GetUnits())
            {
                if (item.IsDead() == true)
                {
                    continue;
                }
                if (item.GetAttackedRemainTicks() <= 0)
                {
                    actionUnits.Add(item);
                }
            }
            var order = actionUnits.OrderByDescending(r => r.UnitInfo.Level);
            foreach(var item in order)
            {
                item.DoAction();
            }
        }

        public Party GetOpponentParty(Unit unit)
        {
            if (_allyParty.IsExist(unit) == true)
            {
                return _enemyParty;
            }
            else
            {
                return _allyParty;
            }
        }

        public Party GetMyParty(Unit unit)
        {
            if (_allyParty.IsExist(unit) == true)
            {
                return _allyParty;
            }
            else
            {
                return _enemyParty;
            }
        }
        public RandomGenerator GetRandomGenerator()
        {
            return _randomGenerator;
        }

    }
}
