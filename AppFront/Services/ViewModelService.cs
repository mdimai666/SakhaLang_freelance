using AppShared.Dto;
using AppShared.Models;
using AppShared.ViewModels;
using BlastCore.Features;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppFront.Services
{
    public class ViewModelService : IViewModelService
    {
        protected readonly QServer _client;
        protected string _controllerName { get; set; }
        protected string _basePath { get; set; }

        public ViewModelService(IServiceProvider serviceProvider, HttpClient httpClient)
        {
            _basePath = "/vm/";
            _controllerName = "ViewModel";

            _client = new QServer(httpClient);
        }

        async Task<T> Get<T>(string query = "") where T : class
        {
            string tname = typeof(T).Name;
            return await _client.GET<T>($"{_basePath}{_controllerName}/{tname}?{query}");
        }

        public async Task<bool> TryUpdateInitialSiteData()
        {
            InitialSiteDataViewModel vm = await InitialSiteDataViewModel();

            if (vm == null)
            {
                Console.WriteLine("InitialSiteDataViewModel is null");
                return false;
            }
            Q.UpdateInitialSiteData(vm);
            return true;
        }

        public async Task<InitialSiteDataViewModel> InitialSiteDataViewModel()
        {
            return await Get<InitialSiteDataViewModel>();
        }

        public async Task<EditUserViewModel> EditUserViewModel(Guid id)
        {
            return await Get<EditUserViewModel>($"id={id}");
        }

        public string SystemExportSettingsUrl()
        {
            string url = _client.m_backendUrl + "vm/ViewModel/SystemExportSettings";
            return url;
        }

        public async Task<UserActionResult> SystemImportSettings(SystemImportSettingsFile_v1 val)
        {
            var result = await _client.Post<UserActionResult>($"{_basePath}{_controllerName}/SystemImportSettings", val);
            return result;
        }

        public async Task<StatisticPageViewModel> StatisticPageViewModel()
        {
            return await Get<StatisticPageViewModel>();
        }
    }

}