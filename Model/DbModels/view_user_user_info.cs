using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///VIEW
    ///</summary>
    [SugarTable("view_user_user_info")]
    public partial class view_user_user_info
    {
           public view_user_user_info(){


           }
           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string email {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string password {get;set;}

           /// <summary>
           /// Desc:
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           public string head_image {get;set;}

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
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int user_info_id {get;set;}

    }
}
