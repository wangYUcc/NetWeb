using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using Dapper;
namespace XUnitTest
{
  public class UnitTest1
  {
    protected readonly ITestOutputHelper Output;

    public UnitTest1(ITestOutputHelper tempOutput)
    {
      Output = tempOutput;
    }


    [Fact]
    public void Test1()
    {
      Output.WriteLine("hello");
     
      
      
    }
    [Theory]
    [InlineData(1)]
    public void Test2(int i)
    {
      try
      {
        var conn = new ConnectDataBaseService("Database=netapp;Data Source=103.61.37.35;User Id=wangyulong;Password=wang282926;CharSet=utf8;port=3306;Allow User Variables=true");


        string select = "select * from user";

       var connect= conn.GetConnect();


        var selectresult = connect.Query(select);
        Output.WriteLine("-----------------fuck-----------------");

       
      }
      catch (Exception e)
      {

        Output.WriteLine(e.ToString());
      }

    }
  }
}
