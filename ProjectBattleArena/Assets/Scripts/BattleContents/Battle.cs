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
        }

        public Party GetAllyParty()
        {
            return _allyParty;
        }
        public Party GetEnemyParty()
        {
            return _enemyParty;
        }
        public void Init()
        {
            _allyParty.SetBattle(this);
            _enemyParty.SetBattle(this);
            _startedTime = DateTime.Now.Ticks;
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
            var elapsedTime = TimeSpan.FromTicks(DateTime.Now.Ticks - this._startedTime).TotalMilliseconds;

            int elapsedTickCount = (int)(elapsedTime / DefaultPerTicks);

            for (int i = 0; i < elapsedTickCount; ++i)
            {
                TickPassedEvent tickPassedEvent = new TickPassedEvent(GetBattleIndex(), _currentTicks);
                _battleEventHandler.Process(tickPassedEvent);
                _currentTicks++;
                DoAction();
            }
        }
        public IBattleEventHandler GetBattleEventHandler()
        {
            return _battleEventHandler;
        }
        public bool IsBattleEnd()
        {
            var survivingAllies = _allyParty.GetAliveTargets();

            var survivingEnemies = _enemyParty.GetAliveTargets();

            if (survivingAllies.Count == 0)
            {
                return true;
            }
            else if (survivingEnemies.Count == 0)
            {
                return true;
            }
            return false;
        }
        private void DoAction()
        {
            _allyParty.DoAction(_currentTicks);
            _enemyParty.DoAction(_currentTicks);
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
