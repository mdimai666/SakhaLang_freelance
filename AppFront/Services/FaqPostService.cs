using AppShared.Models;
using System;
using System.Net.Http;


namespace AppFront.Services
{
    public class FaqPostService : BasicClientService<FaqPost>
    {
        public FaqPostService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }

}