using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using System.Threading;

namespace Service
{
  public class LogService
  {

    public void CreateLogger()
    {
      String basePath = AppContext.BaseDirectory;
      String LogDir = Path.Combine(basePath, "Log");
      String fileDir = Path.Combine(LogDir, "NetWeb_Log");

      if (!Directory.Exists(LogDir))
        Directory.CreateDirectory(LogDir);

      Log.Logger = new LoggerConfiguration()
            .Enrich.With(new ThreadIdEnricher())                                                    //add  to out thread id
            .MinimumLevel.Debug()                                                                        //最小的输出单位是Debug级别的
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)      //将Microsoft前缀的日志的最小输出级别改成Information
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} Thread:{ThreadId} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(fileDir, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} Thread:{ThreadId} [{Level:u3}] {Message:lj}{NewLine}{Exception}")                        //将日志输出到目标路径，文件的生成方式为每天生成一个文件
            .CreateLogger();
    }
  }

  class ThreadIdEnricher : ILogEventEnricher
  {
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
      logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
              "ThreadId", Thread.CurrentThread.ManagedThreadId));
    }
  }
}
