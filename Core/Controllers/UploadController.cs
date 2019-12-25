using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Interface;
using SqlSugar;
using Sugar.Enties;


namespace Core.Controllers
{
  [Consumes("application/json", "multipart/form-data")]//此处为新增
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class UploadController : ControllerBase
  {
    private readonly SqlSugarClient _conn;
    private readonly ILogger<UploadController> _logger;
    private readonly IConfiguration _configuration;
    public UploadController(IConnectionDatabase<SqlSugarClient> conn, 
      ILogger<UploadController> logger,
      IConfiguration configuration
      )
    {
      _configuration = configuration;
      _conn = conn.GetConnect();
      _logger = logger;
    }
 
    [HttpPut("{id}")]
    public async Task<IActionResult> uploadHeadPic(int id, IFormCollection files)
    {
      if (files.Files.Count > 1)
        return BadRequest("错误");

      IFormFile file = files.Files[0];
      string baseDir = AppContext.BaseDirectory;
      string pathDir = Path.Combine(baseDir, "..", "statics");
      string fileExt = Path.GetExtension(file.FileName);
      string fileName; ;
      string guid;

      if (!Directory.Exists(pathDir))
        Directory.CreateDirectory(pathDir);

      if (file.Length == 0)
        return BadRequest();

      user_infor u_info = null;
      try
      {
         u_info = _conn.Queryable<user_infor>().Where(it=>it.user_id==id).First();
      } catch
      {
        _logger.LogError("查询错误 at uploadHeadPic");
        throw;
      }
      if (u_info == null)
        return BadRequest();
      fileName = u_info.head_image;

      if (string.IsNullOrEmpty(fileName)||fileName.ToLower()=="null")
      {
        guid = Guid.NewGuid().ToString();
        fileName = guid  + fileExt;
        //fileName = Path.Combine(_configuration["StaticsFiles"], fileName);
        u_info.head_image = fileName;
        try { _conn.Updateable<user_infor>(u_info).ExecuteCommand(); }
        catch { throw; }
      }

      fileName= Path.Combine(pathDir, fileName);
      using (var stream = System.IO.File.Create(fileName))
      {
        await file.CopyToAsync(stream);
      }

      return Ok(Options.RespnseJsonOptions.Get(200));
    }

    [HttpPost("uploadHeadCover")]
    public async Task<IActionResult> uploadHeadCover(IFormFile file)
    {
      var baseDir = AppContext.BaseDirectory;
      var pathDir = Path.Combine(baseDir, "..", "statics");
      if (!Directory.Exists(pathDir))
        Directory.CreateDirectory(pathDir);


      if (file.Length > 0)
      {
        var fileName = Path.GetFileName(file.FileName);
        if (true)
          fileName = Path.Combine(pathDir, fileName);
        else;  //todo


        using (var stream = System.IO.File.Create(fileName))
        {
          await file.CopyToAsync(stream);
        }
      }
      return Ok(Options.RespnseJsonOptions.Get(200));
    }

    [HttpPost("uploadChapter")]
    public async Task<IActionResult> uploadChapter(List<IFormFile> files)
    {
      var baseDir = AppContext.BaseDirectory;
      var pathDir = Path.Combine(baseDir, "..", "statics");
      if (!Directory.Exists(pathDir))
        Directory.CreateDirectory(pathDir);

      long size = files.Sum(f => f.Length);
      foreach (var formFile in files)
      {
        if (formFile.Length > 0)
        {
          //todo （no complete）
          var filePath = Path.GetFileName(formFile.FileName);
          using (var stream = System.IO.File.Create(filePath))
          {
            await formFile.CopyToAsync(stream);
          }
        }
      }
      return Ok();
    }
  }
}