using Abp.Auditing.MongoDb.Configuration;
using MongoDB.Driver;

namespace Abp.Auditing.MongoDb.Infrastructure
{
    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly IAuditingMongoDbConfiguration _mongoDbConfiguration;
        
        public MongoClientFactory(IAuditingMongoDbConfiguration mongoDbConfiguration)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
        }
        
        public IMongoClient Create()
        {
            return new MongoClient(_mongoDbConfiguration.ConnectionString);
        }
    }
}