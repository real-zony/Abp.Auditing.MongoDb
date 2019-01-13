using MongoDB.Driver;

namespace Abp.Auditing.MongoDb.Infrastructure
{
    public interface IMongoClientFactory
    {
        IMongoClient Create();
    }
}