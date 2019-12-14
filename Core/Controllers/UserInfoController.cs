using System;
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
  public class UserInfoController : Controller
  {
    private readonly SqlSugarClient _conn;
    private readonly ILogger<UserInfoController> _logger;
    public UserInfoController(IConnectionDatabase<SqlSugarClient> conn, ILogger<UserInfoController> logger)
    {
      _conn = conn.GetConnect();
      _logger = logger;
    }
    [HttpGet("{userid}")]
    public IActionResult Get(int userid)
    {
      try
      {
        var model = _conn.Queryable<user_infor>().Where(it => it.user_id == userid).First();
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" user_infor 查询错误");
        throw;
      }
    }


    // 分页操作
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
      List<user_infor> listmodel = null;
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
          listmodel = _conn.Queryable<user_infor>().OrderBy(user_infor => user_infor.id)
            .ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));

        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<user_infor>()
          .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value + "limit " + pageIndex + " " + pageSize);
        }
      }
      else
      {
        try
        {
          /**过滤查询**/
          listmodel = _conn.Queryable<user_infor>()
            .Where(attr + "=" + serach)
            .OrderBy(item => item.id)
            .ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<user_infor>()
            .Where(attr + "=" + serach)
            .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value + "limit " + pageIndex + " " + pageSize);
        }


      }

      return Ok(listmodel);
    }

    [HttpPut("{userid}")]
    public IActionResult Put(int userid, [FromBody] user_infor user_infor)
    {
      try
      {
        var info = _conn.Queryable<user_infor>().Where(it => it.user_id == userid).First();
        if (info == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "id 对应数据不存在"));

        user_infor.id = info.id;
      }
      catch
      {
        _logger.LogError("异常查询");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(Options.RespnseJsonOptions.Get(400, "数据格式不合规范"));
      }

      try
      {
        if (_conn.Updateable<user_infor>(user_infor).ExecuteCommand() > 0)
          return Ok(Options.RespnseJsonOptions.Get(200, "更新成功"));
        return Ok(Options.RespnseJsonOptions.Get(200, "没有内容更新"));
      }
      catch (Exception ex)
      {
        _logger.LogError("异常更新");
      }
      return BadRequest(Options.RespnseJsonOptions.Get(400, "更新失败"));
    }
  }
}