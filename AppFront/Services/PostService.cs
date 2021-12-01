using AppShared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppFront.Services
{
    public class PostService : BasicClientService<Post>
    {
        public PostService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {
        }

        List<Post> cachedParsedPosts = null;

        public async Task<List<Post>> GetNews()
        {
            if (cachedParsedPosts == null)
            {
                cachedParsedPosts = await NewsFromMinsel();
                Console.WriteLine("++cachedParsedPosts");
            }
            return cachedParsedPosts;

            
        }

        public async Task<FileEntity> FileEntity(Guid file_id)
        {
            var result = await _client.GET<FileEntity>($"{_basePath}{_controllerName}/FileEntity/{file_id}");

            return result;
        }

        public async Task<UserActionResult> DeleteFileEntity(Guid id)
        {
            var result = await _client.DELETE<UserActionResult>($"{_basePath}{_controllerName}/DeleteFileEntity/{id}");

            return result;
        }

        //public NewsFromMinsel
        public async Task<List<Post>> NewsFromMinsel()
        {
            return await _client.GET<List<Post>>($"{_basePath}{_controllerName}/NewsFromMinsel");
        }

        public async Task<UserActionResult> ParseNewsFromMinsel()
        {
            return await _client.GET<UserActionResult>($"{_basePath}{_controllerName}/ParseNewsFromMinsel");
        }
    }
}
