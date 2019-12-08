using System;
using System.ComponentModel.DataAnnotations;
// dapper 使用
namespace Model
{
  public class NetAPPModel
  {

    [Dapper.Contrib.Extensions.Table("type")]
    public class type
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string name { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("chapter")]
    public class chapter
    {
      [Dapper.Contrib.Extensions.ExplicitKey]
      public int id { get; set; }
      public string name { get; set; }
      public int? count { get; set; }
      public System.UInt32 number { get; set; }
      public System.UInt32 comic_id { get; set; }
      public string dir { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("analysis")]
    public class analysis
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public System.UInt32 totle { get; set; }
      public System.DateTime create_date { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("user")]
    public class user
    {
      [Required]
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      [Required]
      public string email { get; set; }
      public string password { get; set; }
      public string token { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("view_cat_cat_item")]
    public class view_cat_cat_item
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 cat_id { get; set; }
      public string cat_name { get; set; }
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 cat_item_id { get; set; }
      public string name { get; set; }
      public string href { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("category")]
    public class category
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string name { get; set; }
      public System.UInt32 type_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("comic")]
    public class comic
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string title { get; set; }
      public string editor { get; set; }
      public string type { get; set; }
      public string description { get; set; }
      public string cover { get; set; }
      public System.DateTime? create_date { get; set; }
      public System.DateTime? update_date { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("cat")]
    public class cat
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string cat_name { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("auth_log")]
    public class auth_log
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string ip { get; set; }
      public string user_name { get; set; }
      public System.DateTime? log_date { get; set; }
      public System.DateTime? logout_date { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("operator_log")]
    public class operator_log
    {
      [Dapper.Contrib.Extensions.ExplicitKey]
      public System.UInt32 id { get; set; }
      public string cat { get; set; }
      public string catItem { get; set; }
      public string action { get; set; }
      public System.DateTime? create_date { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("host_status")]
    public class host_status
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string ip { get; set; }
      public float memory { get; set; }
      public float cpu { get; set; }
      public float donwFlow { get; set; }
      public float uploadFlow { get; set; }
      public int runDay { get; set; }
      public float hardDisk { get; set; }
      public System.DateTime create_data { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("tag")]
    public class tag
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string name { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("cat_item")]
    public class cat_item
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string name { get; set; }
      public string href { get; set; }
      public System.UInt32 cat_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("and_permission_role")]
    public class and_permission_role
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public System.UInt32 permission_id { get; set; }
      public System.UInt32 role_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("permission")]
    public class permission
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string name { get; set; }
      public string href { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("user_infor")]
    public class user_infor
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string nick_name { get; set; }
      public bool? sex { get; set; }
      public string head_image { get; set; }
      public System.UInt32 user_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("view_blog_category")]
    public class view_blog_category
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public System.UInt32 user_id { get; set; }
      public string title { get; set; }
      public string editor { get; set; }
      public string description { get; set; }
      public string content { get; set; }
      public System.DateTime create_time { get; set; }
      public System.DateTime update_time { get; set; }
      public string type { get; set; }
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 middle_table_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("and_category_blog")]
    public class and_category_blog
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public System.UInt32 blog_id { get; set; }
      public System.UInt32 category_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("and_user_role")]
    public class and_user_role
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public System.UInt32 user_id { get; set; }
      public System.UInt32 role_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("and_tag_blog")]
    public class and_tag_blog
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public System.UInt32 tag_id { get; set; }
      public System.UInt32 blog_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("blog")]
    public class blog
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string title { get; set; }
      public string editor { get; set; }
      public System.DateTime update_time { get; set; }
      public System.DateTime create_time { get; set; }
      public string content { get; set; }
      public string description { get; set; }
      public System.UInt32 user_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("access_log")]
    public class access_log
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string ip { get; set; }
      public System.DateTime create_date { get; set; }
      public System.DateTime? end_date { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("and_tag_comic")]
    public class and_tag_comic
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public System.UInt32 tag_id { get; set; }
      public System.UInt32 comic_id { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("role")]
    public class role
    {
      [Dapper.Contrib.Extensions.Key]
      public System.UInt32 id { get; set; }
      public string name { get; set; }
    }
  }
}
