using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

using Service;

namespace Core
{
  public class Program
  {

    public static void Main(string[] args)
    {
      new LogService().CreateLogger();
      
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)           
            .UseStartup<Startup>()
            .UseSerilog() ;
  }
}
