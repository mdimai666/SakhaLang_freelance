
using AppShared.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AppShared.Dto
{
    public class NewsMediaPulse : IPostS
    {
        public string Id { get; set; }
        public string MediaId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Excerpt { get; set; }
        public string Image { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid UserId { get; set; }
        public string Source { get; set; }
        public int CommentsCount { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
        public EPostStatus Status { get; set; } = EPostStatus.Publish;

        public NewsMediaPulse()
        {

        }

        public NewsMediaPulse(NewsMediaPulseDto raw)
        {
            Regex reg = new Regex("[«»']");

            this.Id = raw.id;
            this.MediaId = raw.media_id;
            this.Title = reg.Replace(raw.title ?? "", "\"");
            this.Excerpt = raw.descr;
            this.Content = raw.text;
            this.Created = Convert.ToDateTime(raw.time);
            this.Modified = Created;
            this.Image = raw.image?.Replace("//mediapulse.online/", "//mediapulse.site/");
            this.Source = raw.source;

            StripContent();
        }

        void StripContent(){
            if(!string.IsNullOrEmpty(Content)){
                Regex reg = new Regex("<!--(.+?)-->", RegexOptions.Multiline);
                Content = reg.Replace(Content, "");
            }
        }
    }

    public class NewsMediaPulseDto
    {
        public string id;
        public string media_id;
        public string image;
        public string source;
        public string title;
        public string time;
        public string url;
        public string descr;
        public string text;
    }

    public class MediapulseResponse
    {
        public bool error;
        //public string message; //"Successful connection" 
        //public DateTime date_time; 
        public IEnumerable<NewsMediaPulseDto> media;
    }
}