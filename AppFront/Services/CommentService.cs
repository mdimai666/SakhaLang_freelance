using AppShared.Models;
using System;
using System.Net.Http;

namespace AppFront.Services
{
    public class CommentService: BasicClientService<Comment>
    {
        public CommentService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }


}
