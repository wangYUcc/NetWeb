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
  public class MRolePermissionController : ControllerBase
  {
    // 中间表操作

    private readonly SqlSugarClient _conn;
    private readonly ILogger<MRolePermissionController> _logger;
    public MRolePermissionController(IConnectionDatabase<SqlSugarClient> conn, ILogger<MRolePermissionController> logger)
    {
      _conn = conn.GetConnect();
      _logger = logger;
    }


    // 获取所有角色
    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRoles()
    {
      try
      {
        var model = _conn.Queryable<role>().ToList();
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" and_permission_role 查询错误");
        throw;
      }
      finally
      {
        _conn.Close();
      }
    }

    //获取所有权限
    [HttpGet("GetPermissions")]
    public async Task<IActionResult> GetPermissions()
    {
      try
      {
        var model = _conn.Queryable<permission>().ToList();
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug(" and_permission_role 查询错误");
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
        var model = _conn.Queryable<view_permission_role>().Where(it => it.role_id == id).ToList();
        //三表查询（老子之前见了视图这里没有用处）
        //var model = _conn.Queryable<role, and_permission_role, permission>((role, middleTable, permission) 
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
        _logger.LogDebug(" and_permission_role 查询错误");
        throw;
      }
      finally
      {
        _conn.Close();
      }
    }


    //角色权限添加 //角色权限修改// 角色权限删除
    [HttpPost]
    public async System.Threading.Tasks.Task<IActionResult> PostAsync([FromBody] ManageModel model)
    {
      if (!ModelState.IsValid)
      {
      return  BadRequest(Options.RespnseJsonOptions.Get(400, "添加失败"));
      }
      try
      {
        var lists = _conn.Queryable<and_permission_role>().Where(it => it.role_id == model.selectedValue).ToList();
        if (lists.Count==0 || _conn.Deleteable<and_permission_role>().Where(lists).ExecuteCommand() > 0)
        {
          List<and_permission_role> list = new List<and_permission_role>();
          if (model.checkArray.Length == 0)
            return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
          foreach (int i in model.checkArray)
            list.Add(new and_permission_role() { role_id = model.selectedValue, permission_id = i });
          if (_conn.Insertable(list).ExecuteCommand() > 0)
            return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(1001, ex, "and_permission_role提交错误 数据:" + model.ObjToString());
        return BadRequest(Options.RespnseJsonOptions.Get(400, "发生异常，and_permission_role添加失败"));
      }
      return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
    }
  }
}