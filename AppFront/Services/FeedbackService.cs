using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppShared.Dto;
using AppShared.Models;


namespace AppFront.Services
{
    public class FeedbackService : BasicClientService<Feedback>
    {
        public FeedbackService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }

        public async Task<UserActionResult> SendFeedback(FeedbackDto feedback)
        {
            UserActionResult result = await _client.Post<UserActionResult>($"{_basePath}{_controllerName}/SendFeedback/", feedback);
            //Console.WriteLine($"PublishZayavka = {result == "true"}");
            return result;
        }
    }

}