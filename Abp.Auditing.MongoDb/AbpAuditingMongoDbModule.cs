using Abp.Auditing.MongoDb.Configuration;
using Abp.Auditing.MongoDb.Infrastructure;
using Abp.Dependency;
using Abp.Modules;

namespace Abp.Auditing.MongoDb
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpAuditingMongoDbModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAuditingMongoDbConfiguration,AuditingMongoDbConfiguration>();
            IocManager.Register<IMongoClientFactory,MongoClientFactory>();
            
            // 替换自带的审计日志存储实现
            Configuration.ReplaceService(typeof(IAuditingStore),() =>
            {
                IocManager.Register<IAuditingStore, MongoDbAuditingStore>(DependencyLifeStyle.Transient);
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpAuditingMongoDbModule).Assembly);
        }
    }
}