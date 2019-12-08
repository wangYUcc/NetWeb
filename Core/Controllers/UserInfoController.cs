using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using SqlSugar;
using Sugar.Enties;

namespace Core.Controllers
{
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
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      try
      {
        var User = _conn.Queryable<user_infor>().First();
        return Ok(User);
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
      List<user_infor> listUser = null;
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
          listUser = _conn.Queryable<user_infor>().OrderBy(user_infor => user_infor.id)
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
          listUser = _conn.Queryable<user_infor>()
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

      return Ok(listUser);
    }

    [HttpPost("{user_id}")]
    public IActionResult Post(int user_id, [FromForm] user_infor user_infor)
    {
      if (ModelState.IsValid)
      {
        BadRequest("添加失败");
      }
      try
      {
        user_infor.user_id = user_id;
        int rowCount = 0;
        rowCount = _conn.Insertable<user_infor>(user_infor).ExecuteCommand();
        if (rowCount == 0)
        {
          return BadRequest("user_infor添加失败");
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(1001, ex, "user_infor提交错误 数据:" + user_infor.ObjToString());
        return BadRequest("发生异常，user_infor添加失败");
      }
      
      return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] user_infor user_infor)
    {

      try
      {
        if (_conn.Queryable<user_infor>().Where(it => it.id == id).First() == null)
          return BadRequest("id 对应数据不存在");
      }
      catch
      {
        _logger.LogError("异常查询");
      }

      user_infor.id = id;
      if (!ModelState.IsValid)
      {
        try
        {
          int i = _conn.Updateable<user_infor>(user_infor).ExecuteCommand();
          return Ok(Options.RespnseJsonOptions.Get(200, "更新成功"));
        }
        catch (Exception ex)
        {
          _logger.LogError("异常更新");

        }
      }

      return BadRequest("更新失败");

    }

  }


}