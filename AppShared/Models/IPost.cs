using AppShared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.Models
{
    public interface IPost<TKey>
    {
        [Key]
        [Required]
        public TKey Id { get; set; }
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Title { get; set; }
        public string Content { get; set; }

        public string Image { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid UserId { get; set; }

        public EPostStatus Status { get; set; }

    }

    public enum EPostStatus : int
    {
        Draft = 0,
        Pending = 1,
        Publish = 2,
        Private = 3,
        Inherit = 10,
        Trash = 11,
        Undefined = 20,

    }

    public interface IPost : IPost<Guid> { }

    /// <summary>
    /// С строковым ключом
    /// </summary>
    public interface IPostS : IPost<string> { }

}
