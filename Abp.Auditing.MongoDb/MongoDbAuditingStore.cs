using System.Threading.Tasks;
using Abp.Auditing.MongoDb.Configuration;
using Abp.Auditing.MongoDb.Infrastructure;
using MongoDB.Driver;

namespace Abp.Auditing.MongoDb
{
    /// <summary>
    /// <see cref="IAuditingStore"/> 的特殊实现，使用的是 MongoDb 作为持久化存储。
    /// </summary>
    public class MongoDbAuditingStore : IAuditingStore
    {
        private readonly IMongoClientFactory _clientFactory;
        private readonly IAuditingMongoDbConfiguration _mongoDbConfiguration;
        
        public MongoDbAuditingStore(IMongoClientFactory clientFactory, IAuditingMongoDbConfiguration mongoDbConfiguration)
        {
            _clientFactory = clientFactory;
            _mongoDbConfiguration = mongoDbConfiguration;
        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            var entity = MongoDbAuditEntity.CreateFromAuditInfo(auditInfo);
            
            await _clientFactory.Create()
                .GetDatabase(_mongoDbConfiguration.DataBaseName)
                .GetCollection<MongoDbAuditEntity>(typeof(MongoDbAuditEntity).Name)
                .InsertOneAsync(entity);
        }
    }
}