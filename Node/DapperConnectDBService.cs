using System;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Service.Interface;

// 这里是使用dapper 的
namespace Service
{

  public class DapperConnectDBService : IConnectionDatabase<MySqlConnection>
  {
    private static MySqlConnection _connection = null;

    public DapperConnectDBService(String connectionString)
    {
      try
      {
        if (_connection == null) _connection = new MySqlConnection(connectionString);
      }
      catch { throw; }
    }

    public MySqlConnection GetConnect()
    {
      if (_connection != null) return _connection;
      else return null;
    }

    public void CloseConnect()
    {
      try { _connection.Close(); }
      catch { throw; }
    }

  }
}
