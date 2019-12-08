using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;
using SqlSugar;
using Sugar.Enties;

namespace Core.Controllers.Auth
{
  [Route("api/[action]")]
  [ApiController]
  public class AuthController : ControllerBase
  {

    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    private readonly SqlSugarClient _conn;

    public AuthController(IConnectionDatabase<SqlSugarClient> conn, ILogger<AuthController> logger, IConfiguration configuration)
    {
      _configuration = configuration;
      _conn = conn.GetConnect();
      _logger = logger;
    }


    [HttpPost]
    public IActionResult Login(string username, string password)
    {
      if (username == "mo" && password == "123456")
      {
        // push the user’s name into a claim, so we can identify the user later on.
        var claims = new[]
        {
                   new Claim(ClaimTypes.Name, username),
                   new Claim(ClaimTypes.Role, "admin"),
                   new Claim("Permission", "admin4"),
     };
        //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "wang",
            audience: "user",
            claims: claims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: creds);
        return Ok(new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token)
        });
      }

      return BadRequest("用户名密码错误");
    }

    public IActionResult Registe([FromForm] user Form)
    {
      if (ModelState.IsValid)
      {
        try
        {
          int rowCount = 0;
          rowCount = _conn.Insertable<user>(Form).ExecuteCommand();
          if (rowCount == 0)
          {
            return BadRequest(Options.RespnseJsonOptions.Get(500, "user添加失败"));
          }
        }
        catch
        {
          throw;
        }
        return Ok(Options.RespnseJsonOptions.Get(200, "成功创建"));
      }
      else
      {
        return BadRequest(Options.RespnseJsonOptions.Get(400, "不和格式"));
      }
    }
  }
}