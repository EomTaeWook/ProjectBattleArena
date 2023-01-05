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
            _allyParty.DoAction();

            _enemyParty.DoAction();
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
