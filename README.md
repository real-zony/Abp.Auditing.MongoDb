> 审计日志库是基于 MongoDB 实现的高性能日志记录组件，如果需要启用审计日志的话，需要集成本模块。

NuGet 包名：HKAbp.Auditing.MongoDb  
NuGet 包地址：[NuGet 地址](https://www.nuget.org/packages/Abp.Auditing.MongoDb/4.5.0)  
NuGet 包版本：[![NuGet version](https://img.shields.io/badge/nuget-4.5.0-brightgreen.svg)](https://www.nuget.org/packages/Abp.Auditing.MongoDb/4.5.0)  
NuGet 包下载量：[![NuGet Downloads](https://img.shields.io/nuget/dt/Abp.Auditing.MongoDb.svg?style=flat-square)](https://www.nuget.org/stats/packages/Abp.Auditing.MongoDb?groupby=Version)

使用方法：

1. 在启动项目增加对 ``Abp.Auditing.MongoDb`` 包的引用。

2. 在启动项目的模块定义 ``DependsOn`` 标签增加对 ``AbpAuditingMongoDbModule`` 模块的依赖。

3. 在 Abp 启动模块的预加载方法 (``PreInitialize()``) 当中增加以下代码：

   ```csharp
   public override void PreInitialize()
   {
   // ... 其他代码
   
   Configuration.Auditing.IsEnabled = true;
   Configuration.Modules.ConfigureMongoDbAuditingStore("mongodb://username:password@ip:port","AuditInfo");
       
   // ... 其他代码
   }
   ```

4. 如果需要开启匿名访问的审计日志统计，还得在启动模块的预加载方法当中增加以下代码进行开启。

   ```csharp
   Configuration.Auditing.IsEnabledForAnonymousUsers = true;
   ```


