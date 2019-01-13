namespace Abp.Auditing.MongoDb.Configuration
{
    public class AuditingMongoDbConfiguration : IAuditingMongoDbConfiguration
    {
        public string ConnectionString { get; set; }
        
        public string DataBaseName { get; set; }
    }
}