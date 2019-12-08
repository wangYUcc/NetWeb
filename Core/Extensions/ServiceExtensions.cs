using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
  public static class ServiceExtensions
  {
    public static IServiceCollection AddHangfire(this IServiceCollection services, string connectString)
    {
      //services.TryAddSingleton<ISchedulerFactory, StdSchedulerFactory>();
      // Add Hangfire services.
      services.AddHangfire(configuration => configuration
          .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseStorage(new MySqlStorage
          (connectString, new MySqlStorageOptions()
          {
            TransactionIsolationLevel = IsolationLevel.ReadCommitted,
            QueuePollInterval = TimeSpan.FromSeconds(15),
            JobExpirationCheckInterval = TimeSpan.FromHours(1),
            CountersAggregateInterval = TimeSpan.FromMinutes(5),
            PrepareSchemaIfNecessary = true,
            DashboardJobListLimit = 50000,
            TransactionTimeout = TimeSpan.FromMinutes(1),
            TablePrefix = "Jop_"
          })
          ));
      // Add the processing server as IHostedService
      services.AddHangfireServer();



      return services;

    }


    public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration config)
    {

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                   ValidIssuer = "wang",//Issuer，这两项和前面签发jwt的设置一致
                   ValidAudience = "user",        //Audience

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SecurityKey"]))//拿到SecurityKey
                  };
                });

      // 定义授权策略
      //如下，我们定义了一个名称为EmployeeOnly的授权策略，它要求用户的Claims中必须包含类型为EmployeeNumber的Claim。
      //services.AddAuthorization(options =>
      //{
      //  options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
      //});
      return services;

    }



  }
}
