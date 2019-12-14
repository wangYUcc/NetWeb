using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using SqlSugar;
using Sugar.Enties;

namespace Core.Controllers
{
  // [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  [ApiController]
  public class TagController : Controller
  {
    private readonly SqlSugarClient _conn;
    private readonly ILogger<TagController> _logger;
    public TagController(IConnectionDatabase<SqlSugarClient> conn, ILogger<TagController> logger)
    {
      _conn = conn.GetConnect();
      _logger = logger;
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      try
      {
        var model = _conn.Queryable<tag>().InSingle(id);
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" tag 查询错误");
        throw;
      }
      finally
      {
        _conn.Close();
      }
    }
    


    // 分页操作
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
      List<tag> listmodel = null;
      /**条件过滤值**/
      var attr = Request.Query["attr"];                                          //类型 
      var serach = Request.Query["serach"];                            //搜索值 
      var pageIndex = Request.Query["pageIndex"];               //偏移
      var pageSize = Request.Query["pageSize"];                              // 列数

      if (string.IsNullOrEmpty(pageIndex))
      {
        pageIndex = "0";
      }
      if (string.IsNullOrEmpty(pageSize))
      {
        pageSize = "10";
      }

      if (string.IsNullOrEmpty(serach) && string.IsNullOrEmpty(attr))
      {
        try
        {
          /**分页查询**/
          listmodel = _conn.Queryable<tag>().OrderBy(tag => tag.id)
            .ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));

        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<tag>()
          .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value + "limit " + pageIndex + " " + pageSize);
        }
      }
      else
      {
        try
        {
          /**过滤查询**/
          listmodel = _conn.Queryable<tag>()
            .Where(attr + "=" + serach)
            .OrderBy(item => item.id)
            .ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<tag>()
            .Where(attr + "=" + serach)
            .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value + "limit " + pageIndex + " " + pageSize);
        }


      }

      return Ok(listmodel);
    }

    [HttpPost]
    public async System.Threading.Tasks.Task<IActionResult> PostAsync([FromBody] tag model)
    {
      if (!ModelState.IsValid)
      {
        BadRequest(Options.RespnseJsonOptions.Get(400, "添加失败"));
      }
      try
      {
        int id = 0;
        id = await _conn.Insertable<tag>(model).ExecuteReturnIdentityAsync();
        if (id == 0)
        {
          return BadRequest(Options.RespnseJsonOptions.Get(400, "tag添加失败"));
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(1001, ex, "tag提交错误 数据:" + model.ObjToString());
        return BadRequest(Options.RespnseJsonOptions.Get(400, "发生异常，tag添加失败"));
      }
      return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] tag model)
    {

      try
      {
        if (_conn.Queryable<tag>().Where(it => it.id == id).First() == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "id 对应数据不存在"));
      }
      catch
      {
        _logger.LogError("异常查询");
      }

      model.id = id;
      if (ModelState.IsValid)
      {
        try
        {
          int i = _conn.Updateable<tag>(model).ExecuteCommand();
          return Ok(Options.RespnseJsonOptions.Get(200, "更新成功"));
        }
        catch (Exception ex)
        {
          _logger.LogError("异常更新");

        }
      }

      return BadRequest(Options.RespnseJsonOptions.Get(400, "更新失败"));

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      try
      {
        if (_conn.Deleteable<tag>().With(SqlWith.RowLock).In(id).ExecuteCommand() > 0)
          return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
      }
      catch
      {
        _logger.LogError("删除失败");
        throw;
      }
      return BadRequest(Options.RespnseJsonOptions.Get(400, "删除失败"));
    }


    [HttpGet("getStruct")]
    public  IActionResult GetTagStruct()
    {
      ArrayList items = new ArrayList();
      try
      {

        var dt = _conn.Ado.GetDataTable(
         "select column_name,column_default,is_nullable,data_type from information_schema.columns " +
         "where table_name = @tableName and table_schema = @scheme; ",
         new SugarParameter[]{
                      new SugarParameter("@tableName","tag"),
                      new SugarParameter("@scheme","netapp")
        });
        var rows = dt.Rows;
        
        for (int i = 0; i < rows.Count; i++)
        {
          items.Add(rows[i].ItemArray);
        }
      }
      catch(Exception e)
      {
        _logger.LogError("查询表结构失败");
      }
      return Ok(Options.RespnseJsonOptions.Get(200, "查询成功",new { items } ));
    }


  }

}