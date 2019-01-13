using Abp.Auditing.MongoDb.Configuration;
using Abp.Configuration.Startup;

namespace Abp.Auditing.MongoDb.Extensions
{
    /// <summary>
    /// MongoDb 审计日志存储提供器的配置类的扩展方法。
    /// </summary>
    public static class AuditingMongoDbConfigurationExtensions
    {
        /// <summary>
        /// 配置审计日志的 MongoDb 实现的相关参数。 
        /// </summary>
        /// <param name="modules">模块配置类</param>
        /// <param name="connectString">MongoDb 连接字符串。</param>
        /// <param name="dataBaseName">要操作的 MongoDb 数据库。</param>
        public static void ConfigureMongoDbAuditingStore(this IModuleConfigurations modules,string connectString,string dataBaseName)
        {
            var configuration = modules.AbpConfiguration.Get<IAuditingMongoDbConfiguration>();
            
            configuration.ConnectionString = connectString;
            configuration.DataBaseName = dataBaseName;
        }
    }
}