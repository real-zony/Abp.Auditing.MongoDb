using System;
using System.Linq;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Abp.UI;

namespace Abp.Auditing.MongoDb
{
/// <summary>
    /// 审计日志记录实体，仅用于 MongoDb 存储使用。
    /// </summary>
    public class MongoDbAuditEntity
    {
        /// <summary>
        /// <see cref="ServiceName"/> 属性的最大长度。
        /// </summary>
        public static int MaxServiceNameLength = 256;

        /// <summary>
        /// <see cref="MethodName"/> 属性的最大长度。
        /// </summary>
        public static int MaxMethodNameLength = 256;

        /// <summary>
        /// <see cref="Parameters"/> 属性的最大长度。
        /// </summary>
        public static int MaxParametersLength = 1024;

        /// <summary>
        /// <see cref="ClientIpAddress"/> 属性的最大长度。
        /// </summary>
        public static int MaxClientIpAddressLength = 64;

        /// <summary>
        /// <see cref="ClientName"/> 属性的最大长度。
        /// </summary>
        public static int MaxClientNameLength = 128;

        /// <summary>
        /// <see cref="BrowserInfo"/> 属性的最大长度。
        /// </summary>
        public static int MaxBrowserInfoLength = 512;

        /// <summary>
        /// <see cref="Exception"/> 属性的最大长度。
        /// </summary>
        public static int MaxExceptionLength = 2000;

        /// <summary>
        /// <see cref="CustomData"/> 属性的最大长度。
        /// </summary>
        public static int MaxCustomDataLength = 2000;
        
        /// <summary>
        /// 调用接口时用户的编码，如果是匿名访问，则可能为 null。
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 调用接口时用户的集团 Id，如果是匿名访问，则可能为 null。
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// 调用接口时，请求的应用服务/控制器名称。
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 调用接口时，请求的的具体方法/接口名称。
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 调用接口时，传递的具体参数。
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 调用接口的时间，以服务器的时间进行记录。
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 调用接口执行方法时所消耗的时间，以毫秒为单位。
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 调用接口时客户端的 IP 地址。
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 调用接口时客户端的名称(通常为计算机名)。
        /// </summary>
        public string ClientName { get; set; }
        
        /// <summary>
        /// 调用接口的浏览器信息。
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 调用接口时如果产生了异常，则记录在本字段，如果没有异常则可能 null。
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public string CustomData { get; set; }

        /// <summary>
        /// 从给定的 <see cref="auditInfo"/> 审计信息创建一个新的 MongoDb 审计日志实体
        /// (<see cref="MongoDbAuditEntity"/>)。
        /// </summary>
        /// <param name="auditInfo">原始审计日志信息。</param>
        /// <returns>创建完成的 <see cref="MongoDbAuditEntity"/> 实体对象。</returns>
        public static MongoDbAuditEntity CreateFromAuditInfo(AuditInfo auditInfo)
        {
            var expMsg = GetAbpClearException(auditInfo.Exception);
            
            return new MongoDbAuditEntity
            {
                UserCode = auditInfo.UserId?.ToString(),
                GroupId = null,
                ServiceName = auditInfo.ServiceName.TruncateWithPostfix(MaxServiceNameLength),
                MethodName = auditInfo.MethodName.TruncateWithPostfix(MaxMethodNameLength),
                Parameters = auditInfo.Parameters.TruncateWithPostfix(MaxParametersLength),
                ExecutionTime = auditInfo.ExecutionTime,
                ExecutionDuration = auditInfo.ExecutionDuration,
                ClientIpAddress = auditInfo.ClientIpAddress.TruncateWithPostfix(MaxClientIpAddressLength),
                ClientName = auditInfo.ClientName.TruncateWithPostfix(MaxClientNameLength),
                BrowserInfo = auditInfo.BrowserInfo.TruncateWithPostfix(MaxBrowserInfoLength),
                Exception = expMsg.TruncateWithPostfix(MaxExceptionLength),
                CustomData = auditInfo.CustomData.TruncateWithPostfix(MaxCustomDataLength)
            };
        }
        
        public override string ToString()
        {
            return string.Format(
                "审计日志: {0}.{1} 由用户 {2} 执行，花费了 {3} 毫秒，请求的源 IP 地址为: {4} 。",
                ServiceName, MethodName, UserCode, ExecutionDuration, ClientIpAddress
            );
        }
        
        /// <summary>
        /// 创建更加清楚明确的异常信息。
        /// </summary>
        /// <param name="exception">要处理的异常数据。</param>
        private static string GetAbpClearException(Exception exception)
        {
            var clearMessage = "";
            switch (exception)
            {
                case null:
                    return null;

                case AbpValidationException abpValidationException:
                    clearMessage = "异常为参数验证错误，一共有 " + abpValidationException.ValidationErrors.Count + "个错误:";
                    foreach (var validationResult in abpValidationException.ValidationErrors) 
                    {
                        var memberNames = "";
                        if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                        {
                            memberNames = " (" + string.Join(", ", validationResult.MemberNames) + ")";
                        }

                        clearMessage += "\r\n" + validationResult.ErrorMessage + memberNames;
                    }
                    break;

                case UserFriendlyException userFriendlyException:
                    clearMessage =
                        $"业务相关错误，错误代码: {userFriendlyException.Code} \r\n 异常详细信息: {userFriendlyException.Details}";
                    break;
            }

            return exception + (string.IsNullOrEmpty(clearMessage) ? "" : "\r\n\r\n" + clearMessage);
        }
    }
}