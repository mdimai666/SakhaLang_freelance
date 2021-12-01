using AppShared.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("options")]
    public sealed class Option : SimpleEntity
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Key { get; set; }
        //[DataType(DataType.DateTime)]
        //public DateTime Modified { get; set; }

        public static readonly string defaultStatus = "";
        public static readonly string defaultType = "";
    }

    public enum EOptionNames
    {
        SiteUrl,
        //home,
        //blogname,
        //blogdescription,
        //users_can_register,
        //admin_email,
        //start_of_week,
        //comments_notify,
        //mailserver_url,
        //mailserver_login,
        //mailserver_pass,
        //mailserver_port,
        //default_category,
        //default_comment_status,
        //posts_per_page,
        //date_format,
        //time_format,
        //permalink_structure,
        //gmt_offset,
        //default_role,
        //upload_path,
        //blog_public,
        //show_on_front,
        //thumbnail_size_w,
        //thumbnail_size_h,
        //thumbnail_crop,
        //close_comments_for_old_posts,
        //close_comments_days_old,

        AutosetOrdersToCouriers
    }

}