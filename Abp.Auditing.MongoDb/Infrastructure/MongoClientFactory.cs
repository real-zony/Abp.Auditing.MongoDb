using Abp.Auditing.MongoDb.Configuration;
using MongoDB.Driver;

namespace Abp.Auditing.MongoDb.Infrastructure
{
    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly IAuditingMongoDbConfiguration _mongoDbConfiguration;
        private readonly object _locker = new object();
        private MongoClient _singletonClient;
        
        public MongoClientFactory(IAuditingMongoDbConfiguration mongoDbConfiguration)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
        }
        
        public IMongoClient Create()
        {
            if (_singletonClient == null)
            {
                lock (_locker)
                {
                    if (_singletonClient == null)
                    {
                        _singletonClient = new MongoClient(_mongoDbConfiguration.ConnectionString);
                    }
                }
            }

            return _singletonClient;
        }
    }
}