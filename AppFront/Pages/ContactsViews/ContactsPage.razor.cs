using System;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.ComponentModel;
using AppFront.Services;
using Microsoft.AspNetCore.Components;
using AppShared.Dto;
using AntDesign;

namespace AppFront.Pages.ContactsViews
{
    public partial class ContactsPage
    {

        private FeedbackDto model = new FeedbackDto();
        bool isLoading = false;
        bool WasSend = false;


        [Inject]
        public FeedbackService feedbackService { get; set; }
        [Inject]
        public MessageService messageService { get; set; }


        private async void OnFinish(EditContext editContext)
        {
            isLoading = true;
            //Console.WriteLine($"Success:{JsonSerializer.Serialize(model)}");

            var res = await feedbackService.SendFeedback(model);
            if (res.Ok)
            {
                WasSend = true;

            }
            else
            {
                _ = messageService.Error(res.Message);
            }
            isLoading = false;
            StateHasChanged();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(model)}");
        }
    }
}