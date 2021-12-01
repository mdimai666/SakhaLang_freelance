using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppFront.Models;
using AppShared.Models;


namespace AppFront.Services
{
    public class OptionService : BasicClientService<Option>
    {
        HttpClient _httpClient;

        public OptionService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SysOptions> GetSysOptions()
        {
            return await _client.GET<SysOptions>($"/api/Option/GetSysOptions");
        }

        public async Task AutosetOrdersToCouriers(bool value)
        {
            await _client.PUT("/api/Option/AutosetOrdersToCouriers", new { value = value });

            //HttpContent data = new StringContent(value.ToString(), Encoding.UTF8);
            //await _httpClient.PutAsync("/api/Option/AutosetOrdersToCouriers", data);
        }

        public async Task<UserActionResult<SysOptions>> SaveSysOptions(SysOptions val)
        {
            return await _client.Post<UserActionResult<SysOptions>>($"{_basePath}{_controllerName}/SaveSysOptions", val);
        }

        public async Task<UserActionResult<SmtpSettingsModel>> SaveSmtpSettings(SmtpSettingsModel val)
        {
            return await _client.Post<UserActionResult<SmtpSettingsModel>>($"{_basePath}{_controllerName}/SaveSmtpSettings", val);
        }


        public async Task<SmtpSettingsModel> GetSmtpSettings()
        {
            return await _client.GET<SmtpSettingsModel>($"{_basePath}{_controllerName}/GetSmtpSettings");
        }

        public async Task<UserActionResult> SendTestEmail(TestMailMessage form)
        {
            try
            {
                return await _client.Post<UserActionResult>($"{_basePath}{_controllerName}/SendTestEmail", form);
            }
            catch (Exception ex)
            {
                return new UserActionResult
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<UserActionResult> SendTestSms(SendSmsModel form)
        {
            try
            {
                return await _client.Post<UserActionResult>($"{_basePath}{_controllerName}/SendTestSms", form);
            }
            catch (Exception ex)
            {
                return new UserActionResult
                {
                    Message = ex.Message
                };
            }
        }

    }
}