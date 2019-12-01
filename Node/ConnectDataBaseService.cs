using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;

namespace Service
{

  public class ConnectDataBaseService
  {
    private static MySqlConnection _connection = null;

    public ConnectDataBaseService(String connectionString)
    {
      try
      {
        if (_connection == null)
          _connection = new MySqlConnection(connectionString);
      }
      catch
      {

        throw;
      }
    }

    public MySqlConnection GetConnect()
    {
      if (_connection != null)
        return _connection;
      else
        return null;
    }

    public void CloseConnect()
    {
      try
      {
        _connection.Close();
      }
      catch
      {
        throw;
      }

    }
  }
}
