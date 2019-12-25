using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
  public class BlogController : Controller
  {
    private readonly SqlSugarClient _conn;
    private readonly ILogger<BlogController> _logger;
    public BlogController(IConnectionDatabase<SqlSugarClient> conn, ILogger<BlogController> logger)
    {
      _conn = conn.GetConnect();
      _logger = logger;
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      try
      {
        var model = _conn.Queryable<blog>().InSingle(id);
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" blog 查询错误");
        throw;
      }
    }


    // 分页操作
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
      List<blog> listmodel = null;
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
          listmodel = _conn.Queryable<blog>().OrderBy(blog => blog.id)
            .ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));

        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<blog>()
          .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value + "limit " + pageIndex + " " + pageSize);
        }
      }
      else
      {
        try
        {
          /**过滤查询**/
          listmodel = _conn.Queryable<blog>()
            .Where(attr + "=" + serach)
            .OrderBy(item => item.id)
            .ToPageList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
        }
        catch (Exception ex)
        {
          var sql = _conn.Queryable<blog>()
            .Where(attr + "=" + serach)
            .OrderBy(item => item.id).ToSql();
          _logger.LogError(1002, ex, " 过滤查询 " + sql.Value + "limit " + pageIndex + " " + pageSize);
        }


      }

      return Ok(listmodel);
    }

    [HttpPost]
    public async System.Threading.Tasks.Task<IActionResult> PostAsync([FromBody] Formmodel blog)
    {
      if (!ModelState.IsValid)
      {
     return   BadRequest(Options.RespnseJsonOptions.Get(400, "添加失败"));
      }
      try
      {
        int rowId = 0;
        
      blog createModel=blog;
        rowId = await _conn.Insertable<blog>(createModel).ExecuteReturnIdentityAsync();
        if (rowId == 0)
        {
          return BadRequest(Options.RespnseJsonOptions.Get(400, "blog添加失败"));
        }
        var tags = new List<and_tag_blog>();
        var tag = new and_tag_blog();
        tag.blog_id = rowId;
        foreach (var it in blog.tagIds)
        {
          tag.tag_id = it;
          tags.Add(tag);
        }
       if(!(_conn.Insertable<and_tag_blog>(tags).ExecuteCommand()>0))
          return BadRequest(Options.RespnseJsonOptions.Get(400, "blog tag 关联失败")); ;

        var cates = new List<and_category_blog>();
        var cate = new and_category_blog();
        cate.blog_id = rowId;
        foreach (var it in blog.cateIds)
        {
          cate.category_id = it;
          cates.Add(cate);
        }
        if (!(_conn.Insertable<and_category_blog>(cates).ExecuteCommand() > 0))
          return BadRequest(Options.RespnseJsonOptions.Get(400, "blog tag 关联失败")); ;

      
      }
      catch (Exception ex)
      {
        _logger.LogError(1001, ex, "blog提交错误 数据:" );
        return BadRequest(Options.RespnseJsonOptions.Get(400, "发生异常，blog添加失败"));
      }
      return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] blog blog)
    {

      try
      {
        if (_conn.Queryable<blog>().Where(it => it.id == id).First() == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "id 对应数据不存在"));
      }
      catch
      {
        _logger.LogError("异常查询");
      }

      blog.id = id;
      if (!ModelState.IsValid)
      {
        try
        {
          int i = _conn.Updateable<blog>(blog).ExecuteCommand();
          return Ok(Options.RespnseJsonOptions.Get(200, "更新成功"));
        }
        catch (Exception ex)
        {
          _logger.LogError("异常更新");

        }
      }

      return BadRequest(Options.RespnseJsonOptions.Get(400, "更新失败"));

    }

    [HttpDelete("{id}")] //删除操作要处理关联关系的删除
    public IActionResult Delete(int id)
    {
      try
      {
        if (_conn.Deleteable<blog>().With(SqlWith.RowLock).In(id).ExecuteCommand() > 0)
          return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
      }
      catch
      {
        _logger.LogError("删除失败");
        throw;
      }
      return BadRequest(Options.RespnseJsonOptions.Get(400, "删除失败"));
    }

    [HttpGet("GetCategorys")]
    public async Task<IActionResult> GetCategorys() 
    {
      List<category> models = null;
        try
        {
        /**过滤查询**/
        models =   _conn.Queryable<category>().ToList();
        }
        catch (Exception ex)
        {
          _logger.LogError(1002, ex, "类别查询");
        throw;
        }
      return Ok(Options.RespnseJsonOptions.Get(400, "成功返回", models));
    }
    [HttpGet("GetTags")]
    public async Task<IActionResult> GetTags()
    {
      List<tag> models = null;
      try
      {
        /**过滤查询**/
        models =  _conn.Queryable<tag>().ToList();
      }
      catch (Exception ex)
      {
        _logger.LogError(1002, ex, "类别查询");
        throw;
      }
      return Ok(Options.RespnseJsonOptions.Get(400, "成功返回", models));
    }

    [HttpPut("id")]
    public async Task<IActionResult> SetCategorysRelationship(int id,[FromBody] List<and_category_blog> models)
    {
      //List<and_category_blog> models = null;
      try
      {
        /**过滤查询**/
       // models = await _conn.Queryable<and_category_blog>().Where(it=>it.blog_id== id).ToListAsync();
        if( _conn.Updateable<and_category_blog>(models).ExecuteCommand()==0)
          return Ok(Options.RespnseJsonOptions.Get(200, "无任何更新", models));


      }
      catch (Exception ex)
      {
        _logger.LogError(1002, ex, "类别查询");
        throw;
      }
      return Ok(Options.RespnseJsonOptions.Get(200, "成功返回", models));
    }


    public async Task<IActionResult> SetTagsRelationship()
    {
      List<tag> models = null;
      try
      {
        /**过滤查询**/
        models = await _conn.Queryable<tag>().ToListAsync();
      }
      catch (Exception ex)
      {
        _logger.LogError(1002, ex, "类别查询");
        throw;
      }
      return Ok(Options.RespnseJsonOptions.Get(400, "成功返回", models));
    }

  }

  public class Formmodel:blog{
    public int[] tagIds { get; set; }
    public int[] cateIds { get; set; }

  }
}