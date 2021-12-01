using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppShared.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace AppFront.Services
{
    public class DocumentService : BasicClientService<Document>
    {
        public DocumentService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {
            _basePath = "/api/";
            _controllerName = typeof(Document).Name;
        }

    }
}