using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("user_infor")]
    public partial class user_infor
    {
           public user_infor(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:别名
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           public string nick_name {get;set;}

           /// <summary>
           /// Desc:true 为男 false 为女
           /// Default:1
           /// Nullable:True
           /// </summary>           
           public byte? sex {get;set;}

           /// <summary>
           /// Desc:
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           public string head_image {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int user_id {get;set;}
    public int role_id { get; set; }

  }
}
