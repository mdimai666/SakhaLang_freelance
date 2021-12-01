using System;
using System.Net.Http;
using AppShared.Models;


namespace AppFront.Services
{
    public class ActionHistoryService : BasicClientService<ActionHistory>
    {
        public ActionHistoryService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }
    }

}