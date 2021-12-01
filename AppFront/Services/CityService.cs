using AppShared.Models;
using System;
using System.Net.Http;


namespace AppFront.Services
{
    public class CityService : BasicClientService<City>
    {
        public CityService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    } 

}