using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Jop__JobQueue")]
    public partial class Jop__JobQueue
    {
           public Jop__JobQueue(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int JobId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Queue {get;set;}

           /// <summary>
           /// Desc:
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           public DateTime? FetchedAt {get;set;}

           /// <summary>
           /// Desc:
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           public string FetchToken {get;set;}

    }
}
