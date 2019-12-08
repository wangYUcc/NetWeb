using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
  ///<summary>
  ///
  ///</summary>
  [SugarTable("user")]
  public partial class user
  {

    /// <summary>
    /// Desc:
    /// Default:
    /// Nullable:False
    /// </summary>           
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int id { get; set; }

    /// <summary>
    /// Desc:
    /// Default:
    /// Nullable:False
    /// </summary>           
    [Required]
    [EmailAddress]
    public string email { get; set; }

    /// <summary>
    /// Desc:
    /// Default:
    /// Nullable:False
    /// </summary>      
    [Required]
  [MinLength(6)]
    public string password { get; set; }

    /// <summary>
    /// Desc:
    /// Default:NULL
    /// Nullable:True
    /// </summary>           
    public string token { get; set; }

  }
}
