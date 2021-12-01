using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppFront.Pages.LessonViews
{
    public partial class LessonDetail
    {

        [Parameter]
        public Guid ID {  get; set; }

        public Lesson Post { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Inject]
        LessonService service { get; set; }


        protected override async void OnInitialized()
        {
            base.OnInitialized();

            //HttpContext.Current.Request.Url.Authority


            Post = await service.Get(ID);
            StateHasChanged();

        }

        public string VideoFrame()
        {
            if (Post == null) return "";
            if (string.IsNullOrWhiteSpace(Post.VideoLink)) return "";

            if (Post.VideoLink.Contains("youtube.com") == false) return "";

            Regex re = new Regex("v=([A-Za-z0-9_-]+)");

            var f = re.Match(Post.VideoLink);

            string ID = "";
            string frame = "";

            Console.WriteLine("xx="+f.Success);

            if (f.Success)
            {
                ID = f.Groups[1].Value;
                frame = $"https://www.youtube.com/embed/{ID}?autoplay=0&rel=0&showinfo=0";
            }

            return frame;
        }
    }
}
