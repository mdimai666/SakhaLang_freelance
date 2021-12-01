using BlastCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFront.Models
{


    public class DtoNewsInList
    {
        public User AuthorUser { get; set; }

        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public int CommentsCount {get;set;}


        public class User
        {
            public string UserName { get; set; }
            public string AvatarUrl { get; set; }
        }

        public static List<DtoNewsInList> GetRandomPosts(int count = 6)
        {
            List<string> titles = new List<string>
            {
                "The Best Features Coming to iOS and Web design",
                "Latest Quirky Opening Sentence or Paragraph",
                "Share an Amazing and Shocking Fact or Statistic",
                "Withhold a Compelling Piece of Information",
                "Unadvertised Bonus Opening: Share a Quote",
                "Ships at a distance have Every Man’s Wish on Board",
            };

            List<string> texts = new List<string>
            {
                "Donut fruitcake soufflé apple pie candy canes jujubes croissant chocolate bar ice cream.",
                "Apple pie caramels lemon drops halvah liquorice carrot cake. Tiramisu brownie lemon drops.",
                "Tiramisu jelly-o chupa chups tootsie roll donut wafer marshmallow cheesecake topping.",
                "Croissant apple pie lollipop gingerbread. Cookie jujubes chocolate cake icing cheesecake.",
                "Muffin liquorice candy soufflé bear claw apple pie icing halvah. Pie marshmallow jelly.",
                "A little personality goes a long way, especially on a business blog. So don’t be afraid to let loose.",
            };

            List<string> tags = new List<string>
            {
                "Fashion",
                "Food",
                "Gaming",
                "Quote",
                "Video",
            };

            List<string> thumbnailUrls = new List<string>
            {
                "/app-assets/images/slider/01.jpg",
                "/app-assets/images/slider/02.jpg",
                "/app-assets/images/slider/03.jpg",
                "/app-assets/images/slider/04.jpg",
                "/app-assets/images/slider/05.jpg",
                "/app-assets/images/slider/06.jpg",
                "/app-assets/images/slider/07.jpg",
                "/app-assets/images/slider/08.jpg",
                "/app-assets/images/slider/09.jpg",
                "/app-assets/images/slider/10.jpg",
            };

            return Tools.IntRange(count).Select(i => new DtoNewsInList
            {
                Title = titles.TakeRandom(),
                Excerpt = texts.TakeRandom(),
                Tags = tags.TakeRandomRange(Tools.RandomNumber(1, 3)).ToList(),
                ThumbnailUrl = thumbnailUrls.TakeRandom(),
                CommentsCount = Tools.RandomNumber(0,25500),

            }).ToList();

        }
    }
}
