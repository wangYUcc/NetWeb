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
  public class RoleController : Controller
  {
    private readonly SqlSugarClient _conn;
    private readonly ILogger<RoleController> _logger;
    public RoleController(IConnectionDatabase<SqlSugarClient> conn, ILogger<RoleController> logger)
    {
      _conn = conn.GetConnect();
      _logger = logger;
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      try
      {
        var model = _conn.Queryable<role>().InSingle(id);
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" role 查询错误");
        throw;
      }
    }


    // 分页操作
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
      List<role> listmodel = null;   
        try
        {
        /**分页查询**/
        listmodel = _conn.Queryable<role>().OrderBy(role => role.id).ToList();
        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<role>()
          .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value );
        }
      return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", listmodel));

     
    }
    [HttpGet("getStruct")]
    public IActionResult GetTagStruct()
    {
      ArrayList items = new ArrayList();
      try
      {

        var dt = _conn.Ado.GetDataTable(
         "select column_name,column_default,is_nullable,data_type from information_schema.columns " +
         "where table_name = @tableName and table_schema = @scheme; ",
         new SugarParameter[]{
                      new SugarParameter("@tableName","role"),
                      new SugarParameter("@scheme","netapp")
        });
        var rows = dt.Rows;

        for (int i = 0; i < rows.Count; i++)
        {
          items.Add(rows[i].ItemArray);
        }
      }
      catch (Exception e)
      {
        _logger.LogError("查询表结构失败");
      }
      return Ok(Options.RespnseJsonOptions.Get(200, "查询成功", new { items }));
    }

    [HttpPost]
    public async System.Threading.Tasks.Task<IActionResult> PostAsync([FromBody] role model)
    {
      if (ModelState.IsValid || string.IsNullOrEmpty(model.name) || model.name.ToLower() == "null")
      {
    return    BadRequest(Options.RespnseJsonOptions.Get(400, "添加失败"));
      }
      try
      {
        int rowCount = 0;
        rowCount = await _conn.Insertable<role>(model).ExecuteReturnIdentityAsync();
        if (rowCount == 0)
        {
          return BadRequest(Options.RespnseJsonOptions.Get(400, "role添加失败"));
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(1001, ex, "role提交错误 数据:" + model.ObjToString());
        return BadRequest(Options.RespnseJsonOptions.Get(400, "发生异常，role添加失败"));
      }
      return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] role model)
    {

      try
      {
        if (_conn.Queryable<role>().Where(it => it.id == id).First() == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "id 对应数据不存在"));
      }
      catch
      {
        _logger.LogError("异常查询");
      }

      model.id = id;
      if (!ModelState.IsValid)
      {
        try
        {
          int i = _conn.Updateable<role>(model).ExecuteCommand();
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
        if (_conn.Deleteable<role>().With(SqlWith.RowLock).In(id).ExecuteCommand() > 0)
          return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
      }
      catch
      {
        _logger.LogError("删除失败");
        throw;
      }
      return BadRequest(Options.RespnseJsonOptions.Get(400, "删除失败"));
    }
  }
}