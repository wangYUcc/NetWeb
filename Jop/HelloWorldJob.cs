using System;
using Quartz;
using System.Threading.Tasks;
namespace Jop
{


  [DisallowConcurrentExecution]
  public class HelloWorldJob : IJob
  {
    Action _logger=null;


    public HelloWorldJob(Action logger)
    {
      _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
      _logger();
      return Task.CompletedTask;
    }
  }
}
