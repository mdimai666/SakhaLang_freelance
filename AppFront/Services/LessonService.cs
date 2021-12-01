using AppShared.Models;


namespace AppFront.Services
{
    public class LessonService : BasicClientService<Lesson>
    {
        public LessonService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class PlaylistService : BasicClientService<Playlist>
    {
        public PlaylistService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class SchoolService : BasicClientService<School>
    {
        public SchoolService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class SchoolClassService : BasicClientService<SchoolClass>
    {
        public SchoolClassService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class SchoolClassPostService : BasicClientService<SchoolClassPost>
    {
        public SchoolClassPostService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class SchoolPostService : BasicClientService<SchoolPost>
    {
        public SchoolPostService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }
    public class LessonCategoryService : BasicClientService<LessonCategory>
    {
        public LessonCategoryService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }

}