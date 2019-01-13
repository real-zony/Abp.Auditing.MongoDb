namespace Abp.Auditing.MongoDb.Configuration
{
    /// <summary>
    /// 审计日志的 MongoDb 存储模块。
    /// </summary>
    public interface IAuditingMongoDbConfiguration
    {
        /// <summary>
        /// MongoDb 连接字符串。
        /// </summary>
        string ConnectionString { get; set; }
        
        /// <summary>
        /// 要连接的 MongoDb 数据库名称 
        /// </summary>
        string DataBaseName { get; set; }
    }
}