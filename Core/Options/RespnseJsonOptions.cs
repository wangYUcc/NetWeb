using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Options
{
  public static class RespnseJsonOptions
  {
    public static Object Get(int code,string info="操作成功", object content = null)
    {
      // return new { Code = code, Info=info, Content = content };
      return content ;
    }
  }
}
