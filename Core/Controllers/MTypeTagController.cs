using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using SqlSugar;
using Sugar.Enties;
namespace Core.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MTypeTagController : ControllerBase
  {
    // 中间表操作

    private readonly SqlSugarClient _conn;
    private readonly ILogger<MTypeTagController> _logger;
    public MTypeTagController(IConnectionDatabase<SqlSugarClient> conn, ILogger<MTypeTagController> logger)
    {
      _conn = conn.GetConnect();
      _logger = logger;
    }


    // 获取所有角色
    [HttpGet("GetTypes")]
    public async Task<IActionResult> GetTypes()
    {
      try
      {
        var model = _conn.Queryable<type>().ToList();
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" and_type_tag 查询错误");
        throw;
      }
      finally
      {
        _conn.Close();
      }
    }

    //获取所有权限
    [HttpGet("GetTags")]
    public async Task<IActionResult> GetTags()
    {
      try
      {
        var model = _conn.Queryable<tag>().ToList();
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" and_type_tag 查询错误");
        throw;
      }
      finally
      {
        _conn.Close();
      }
    }


    //查看角色拥有权限 by role_id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {

      try
      {
        var model = _conn.Queryable<view_type_tag>().Where(it => it.type_id == id).ToList();
        //三表查询（老子之前见了视图这里没有用处）
        //var model = _conn.Queryable<role, and_type_tag, permission>((role, middleTable, permission) 
        //  => id == middleTable.role_id && middleTable.permission_id == permission.id)
        // .Select((role, middleTable, permission) =>
        // new {middleTable.id , middleTable.role_id,middleTable.permission_id,
        //   roleName = role.name,
        //   permissionName = permission.name
        // }).ToList();

        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" and_type_tag 查询错误");
        throw;
      }
      finally
      {
        _conn.Close();
      }
    }


    [HttpPost]
    public async System.Threading.Tasks.Task<IActionResult> PostAsync([FromBody] ManageModel model)
    {
      if (!ModelState.IsValid)
      {
        BadRequest(Options.RespnseJsonOptions.Get(400, "添加失败"));
      }
      try
      {
        var lists =await  _conn.Queryable<and_type_tag>().Where(it => it.type_id == model.selectedValue).ToListAsync();
        if (lists.Count == 0 || _conn.Deleteable<and_type_tag>().Where(lists).ExecuteCommand() > 0)
        {
          List<and_type_tag> list = new List<and_type_tag>();
          if (model.checkArray.Length == 0)
            return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
          foreach (int i in model.checkArray)
            list.Add(new and_type_tag() { type_id = model.selectedValue, tag_id = i });
          if (_conn.Insertable(list).ExecuteCommand() > 0)
            return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(1001, ex, "and_type_tag提交错误 数据:" + model.ObjToString());
        return BadRequest(Options.RespnseJsonOptions.Get(400, "发生异常，and_type_tag添加失败"));
      }
      return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
    }
  }
}