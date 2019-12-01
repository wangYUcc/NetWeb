using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Service
{
  public class CommandService
  {/// <summary>
  /// 
  /// </summary>
  /// <param name="fileName">it is file  dir and command </param>
  /// <param name="arguments"></param>
    public  string CallCommand(string fileName,string arguments)
    {
      var info = "";
      //创建一个ProcessStartInfo对象 使用系统shell 指定命令和参数 设置标准输出
      var psi = new ProcessStartInfo(fileName, arguments) { RedirectStandardOutput = true };
      //var psi = new ProcessStartInfo("dotnet", "--info") { RedirectStandardOutput = true };

      //启动
      var proc = Process.Start(psi);
      if (proc == null)
      {
        Console.WriteLine("Can not exec.");
      }
      else
      {
        Console.WriteLine("-------------Start read standard output--------------");
        //开始读取
        using (var sr = proc.StandardOutput)
        {
          while (!sr.EndOfStream)
          {
            info += sr.ReadLine();
            Console.WriteLine(info);
          }

          if (!proc.HasExited)
          {
            proc.Kill();
          }
        }
        Console.WriteLine("---------------Read end------------------");
        Console.WriteLine($"Total execute time :{(proc.ExitTime - proc.StartTime).TotalMilliseconds} ms");
        Console.WriteLine($"Exited Code ： {proc.ExitCode}");
      }
      return info;
    }
  }
}
