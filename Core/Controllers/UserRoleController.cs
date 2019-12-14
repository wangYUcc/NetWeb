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
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
    private readonly SqlSugarClient _conn;
    private readonly ILogger<UserInfoController> _logger;
    public UserRoleController(IConnectionDatabase<SqlSugarClient> conn, ILogger<UserInfoController> logger)
    {
      _conn = conn.GetConnect();
      _logger = logger;
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      try
      {
        var model = _conn.Queryable<view_user_role>().InSingle(id);
        if (model == null)
          return BadRequest(Options.RespnseJsonOptions.Get(400, "请求失败"));
        return Ok(Options.RespnseJsonOptions.Get(200, "请求成功", model));
      }
      catch
      {
        _logger.LogDebug("  UserRole 查询错误");
        throw;
      }
    }


  }
}