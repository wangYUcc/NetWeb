using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
  }
}
