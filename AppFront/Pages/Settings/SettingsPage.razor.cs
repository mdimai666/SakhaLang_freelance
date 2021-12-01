using AntDesign;
using AppFront.Models;
using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppFront.Pages.Settings
{
    public partial class SettingsPage
    {
        public SysOptions SysOptions { get; set; } = new();

        [Inject] public OptionService optionService { get; set; }
        [Inject] public MessageService messageService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _ = Load();
        }

        async Task Load()
        {
            SysOptions = await optionService.GetSysOptions();
            smtpSettings = await optionService.GetSmtpSettings();

            StateHasChanged();
        }

        private SmtpSettingsModel smtpSettings = new();

        async void OnSaveSysOptions(EditContext editContext)
        {
            var result = await optionService.SaveSysOptions(SysOptions);

            if (result.Ok)
            {
                SysOptions = result.Data;
                _ = messageService.Success(result.Message);
            }
            else
            {
                _ = messageService.Error(result.Message);
            }

        }
        async void OnSmtpSave(EditContext editContext)
        {
            var result = await optionService.SaveSmtpSettings(smtpSettings);

            if (result.Ok)
            {
                smtpSettings = result.Data;
                _ = messageService.Success(result.Message);
            }
            else
            {
                _ = messageService.Error(result.Message);
            }

        }


        #region TEST_MAIL
        TestMailMessage testMailMessage = new()
        {
#if DEBUG
            Email = "mdimai666@mail.ru",
#endif
            Subject = "Test mail",
            Message = "Test message"
        };

        bool testMailLoading = false;

        async void SendTestMail(EditContext editContext)
        {
            testMailLoading = true;
            var result = await optionService.SendTestEmail(testMailMessage);
            testMailLoading = false;
            StateHasChanged();

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
            }
            else
            {
                _ = messageService.Error(result.Message);
            }
        }

        #endregion


        #region PARSE_NEWS

        bool syncNewsLoading = false;

        [Inject] PostService postService { get; set; }

        async void SyncNewsNow()
        {
            syncNewsLoading = true;
            var result = await postService.ParseNewsFromMinsel();
            syncNewsLoading = false;
            StateHasChanged();

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
            }
            else
            {
                _ = messageService.Error(result.Message);
            }
        }


        #endregion

        
        #region TEST_SMS
        SendSmsModel testSmsMessage = new()
        {
#if DEBUG
            Phone = "+79644169433",
#endif
            Message = "Текст"
        };

        bool testSmsLoading = false;

        async void SendTestSms()
        {
            testSmsLoading = true;
            var result = await optionService.SendTestSms(testSmsMessage);
            testSmsLoading = false;
            StateHasChanged();

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
            }
            else
            {
                _ = messageService.Error(result.Message);
            }
        }

        #endregion

    }


}
