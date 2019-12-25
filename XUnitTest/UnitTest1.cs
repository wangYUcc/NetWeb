using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using Dapper;
using static Model.NetAPPModel;
using Dapper.Contrib.Extensions;

namespace XUnitTest
{
  public class UnitTest1
  {
    protected readonly ITestOutputHelper Output;

    public UnitTest1(ITestOutputHelper tempOutput)
    {
      Output = tempOutput;
     
    }


    [Theory]
    [InlineData(1)]
    public void Test2(int i)
    {
      try
      {
        var conn = new DapperConnectDBService
          ("Database=netapp;Data Source=103.61.37.35;User Id=wangyulong;Password=wang282926;CharSet=utf8;port=3306;Allow User Variables=true");
        string select = "select * from user";
        var connect = conn.GetConnect();
        var selectresult = connect.Query(select);
        var res = connect.Get<user>(1);
        Output.WriteLine(res.id.ToString());
      }
      catch (Exception e)
      {
        Output.WriteLine(e.ToString());
      }

    }






    // 数据库实体生成
    //[Fact]
    public void POCO()
    {

      var conns = new DapperConnectDBService("Database=netapp;Data Source=103.61.37.35;User Id=wangyulong;Password=wang282926;CharSet=utf8;port=3306;Allow User Variables=true");

      using (var conn = conns.GetConnect())
      {

        string MySql = "select TABLE_NAME from  information_schema.tables where  table_schema='" + conn.Database + "'";
        Output.WriteLine(MySql);
        PocoClassGenerator.TableSchemaSqls = new Dictionary<string, string> {
               {"sqlconnection", "select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'BASE TABLE'" },
               {"sqlceserver", "select TABLE_NAME from INFORMATION_SCHEMA.TABLES  where TABLE_TYPE = 'BASE TABLE'" },
               {"sqliteconnection", "SELECT name FROM sqlite_master where type = 'table'" },
               {"oracleconnection", "select TABLE_NAME from USER_TABLES where table_name not in (select View_name from user_views)" },
               {"mysqlconnection", MySql },
               {"npgsqlconnection", "select table_name from information_schema.tables where table_type = 'BASE TABLE'" }
       };

        var result = conn.GenerateAllTables(GeneratorBehavior.DapperContrib);

        //var result = conn.GenerateClass("select name from sys.netapp");
        Output.WriteLine(result);
      }
    }

  }
}
