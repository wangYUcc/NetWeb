using Service.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Service
{
  public class SqlSugarConnectDBService : IConnectionDatabase<SqlSugarClient>
  {
    private readonly SqlSugarClient _conn;

    public SqlSugarConnectDBService(string connectString)
    {
      try
      {
        _conn = new SqlSugarClient(
         new ConnectionConfig()
         {
           ConnectionString = connectString,
           DbType = DbType.MySql,                         //设置数据库类型
           IsAutoCloseConnection = true,                //自动释放数据务，如果存在事务，在事务结束后释放
           InitKeyType = InitKeyType.Attribute,       //从实体特性中读取主键自增列信息
           


         });
      }
      catch { throw; }
    }


    public void CloseConnect()
    {
      _conn.Close();
    }

    public SqlSugarClient GetConnect()
    {
      return _conn;
    }
  }
}
