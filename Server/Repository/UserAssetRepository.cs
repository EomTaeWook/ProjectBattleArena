using Repository.Interface;

namespace Repository
{
    public class UserAssetRepository
    {
        readonly IDBContext _dbContext;
        public UserAssetRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }    
    }
}
