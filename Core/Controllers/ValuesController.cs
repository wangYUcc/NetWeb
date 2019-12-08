using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Core.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    // GET api/values
  //[PermissionFilter("admin4")]
    [HttpGet]
    public IActionResult Get()
    {
      var i = 30;
      //while(i!=0){
      //  Thread.Sleep(2000);
      //  Log.Information("hello fuck");
      //  i--;
      //}
      return Ok(Options.RespnseJsonOptions.Get(200, "成功"));
      //  return Ok(Options.RespnseJsonOptions.Get(200,new { fuck=1,con="value"}));
      //  return new string[] { "value1", "value2" };
    }

    // GET api/values/5

    [HttpGet("{id}")]
    //[Authorize]
    //[Authorize(Roles = "Admin")]
    public ActionResult<IEnumerable<string>> Get(int id)
    {


      return new string[] { "value1", "value2" };
    }

    // POST api/values
    [HttpPost]
    public void Post( [FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
