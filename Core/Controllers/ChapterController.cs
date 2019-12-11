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
  public class ChapterController : Controller
  {
    private readonly SqlSugarClient _conn;
    private readonly ILogger<ChapterController> _logger;
    public ChapterController(IConnectionDatabase<SqlSugarClient> conn, ILogger<ChapterController> logger)
    {
      _conn = conn.GetConnect();
      _logger = logger;
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      try
      {
        var User = _conn.Queryable<chapter>().First();
        return Ok(User);
      }
      catch
      {
        _logger.LogDebug(" chapter 查询错误");
        throw;
      }
    }


    // 分页操作
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
      List<chapter> listUser = null;
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
          listUser = _conn.Queryable<chapter>().OrderBy(chapter => chapter.id)
            .ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));

        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<chapter>()
          .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value + "limit " + pageIndex + " " + pageSize);
        }
      }
      else
      {
        try
        {
          /**过滤查询**/
          listUser = _conn.Queryable<chapter>()
            .Where(attr + "=" + serach)
            .OrderBy(item => item.id)
            .ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<chapter>()
            .Where(attr + "=" + serach)
            .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value + "limit " + pageIndex + " " + pageSize);
        }


      }

      return Ok(listUser);
    }

    [HttpPost("{comic_id}")]
    public IActionResult Post(int comic_id, [FromForm] chapter chapter)
    {
      if (ModelState.IsValid)
      {
        BadRequest("添加失败");
      }
      try
      {
        chapter.comic_id = comic_id;
        int rowCount = 0;
        rowCount = _conn.Insertable<chapter>(chapter).ExecuteCommand();
        if (rowCount == 0)
        {
          return BadRequest("chapter添加失败");
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(1001, ex, "chapter提交错误 数据:" + chapter.ObjToString());
        return BadRequest("发生异常，chapter添加失败");
      }

      return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] chapter chapter)
    {

      try
      {
        if (_conn.Queryable<chapter>().Where(it => it.id == id).First() == null)
          return BadRequest("id 对应数据不存在");
      }
      catch
      {
        _logger.LogError("异常查询");
      }

      chapter.id = id;
      if (!ModelState.IsValid)
      {
        try
        {
          int i = _conn.Updateable<chapter>(chapter).ExecuteCommand();
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