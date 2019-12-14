
using SqlSugar;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTest
{
  public class TestDB
  {
    ITestOutputHelper _output;
    SqlSugarClient _conn;
    public TestDB(ITestOutputHelper tempOutput)
    {
      _output = tempOutput;
      _conn = new SqlSugarClient(
         new ConnectionConfig()
         {
           ConnectionString = "Database=netapp;Data Source=103.61.37.35;User Id=wangyulong;Password=wang282926;CharSet=utf8;port=3306;Allow User Variables=true",
           DbType = DbType.MySql,                         //设置数据库类型
           IsAutoCloseConnection = true,                //自动释放数据务，如果存在事务，在事务结束后释放
           InitKeyType = InitKeyType.Attribute        //从实体特性中读取主键自增列信息
         });
    }

    [Theory]
    [InlineData("id", "1; and 1=1", "0", "10")]
    [InlineData("id", "1'; and 1=1 ' ", "0", "10")]
    public void TestUser(string attr, string serach, string pageIndex, string pageSize)
    {
      // test usercontroller



      List<user> listUser = null;

      /**条件过滤值**/
      //var attr = Request.Query["attr"];                                          //类型 
      //string serach = Request.Query["serach"];                            //搜索值 
      //string pageIndex = Request.Query["pageIndex"];               //偏移
      //pageSize = Request.Query["pageSize"];                              // 列数


      if (string.IsNullOrEmpty(serach) && string.IsNullOrEmpty(attr))
      {
        /**分页查询**/
        listUser = _conn.Queryable<user>().OrderBy(user => user.id).ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
      }
      else
      {
        /**过滤查询**/
        listUser = _conn.Queryable<user>().Where(
       attr+"="+serach
        ).ToList();
      }

      var User = listUser[0];
    

   var ss=   string.Equals(GetObjValByAttrName(User, attr), serach);

      _output.WriteLine(listUser.ToString());
      //return listUser;

    }

    /// <summary>
    /// 通过属性名（字符串）获取属性值 obj 的属性值
    /// </summary>
    /// <returns>返回obj的属性值</returns>
    public object GetObjValByAttrName(object obj, string attr)
    {
      var propertyInfo = obj.GetType().GetProperty(attr);   // 通过属性名获取值
      var value = propertyInfo.GetValue(obj, null);
      return value;
    }


    [Theory]
    [InlineData()]
    void UpUser(){
      user user = new user() {
        id = 1,password="23456",email="123@00.xom"

      };
    
      int i = 0;
      i = _conn.Updateable<user>(user).ExecuteCommand();
    var sql= _conn.Updateable<user>(user).ToSql();
      _output.WriteLine(i.ToString()+sql);
  
    }

    [Fact]
    void GetTableStruct()
    {


      var dt = _conn.Ado.GetDataTable(
        "select column_name,is_nullable,data_type ,column_default " +
        "from information_schema.columns where" +
        " table_name = @tableName and table_schema = @scheme; "
      , new SugarParameter[]{
      new SugarParameter("@tableName","tag"),
      new SugarParameter("@scheme","netapp")
      });

       dt = _conn.Ado.GetDataTable(
         "select column_name,is_nullable,data_type from,column_default information_schema.columns " +
         "where table_name = @tableName and table_schema = @scheme; ",
         new SugarParameter[]{
                      new SugarParameter("@tableName","tag"),
                      new SugarParameter("@scheme","netapp")
        });
      var rows=dt.Rows;
     Array a= rows[0].ItemArray;
       
    }
  }
}
