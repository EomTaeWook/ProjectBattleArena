namespace ShareLogic
{
    public class Incremental
    {
        private long _index;
        private readonly object _syncObj = new object();
        public long Increment()
        {
            lock(_syncObj)
            {
                return ++_index;
            }
        }
        public long Current()
        {
            return ++_index;
        }
    }
}
