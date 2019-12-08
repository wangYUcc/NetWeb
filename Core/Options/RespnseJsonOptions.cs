using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Options
{
  public static class RespnseJsonOptions
  {
    public static Object Get(int code,object content){
      return new { Code = code, Content = content };
    }
  }
}
