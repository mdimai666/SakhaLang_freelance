using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using AntDesign;
using AppFront;
using AppFront.Shared;
using AppFront.Shared._Header;
using AppFront.Shared.ContentParts;
using AppFront.Components;
using AppFront.Services;
using AppFront.Models;
using AppShared.Models;
using BlastCore.Extensions;

namespace AppFront.Components
{
    public partial class ExampleFileView
    {
        [Parameter]
        public string DocName {  get; set; }
        [Parameter]
        public string Ext { get; set; } = "pdf";
        public string URL => $"https://dev.do.sakha.education:81/examples/{DocName}.{Ext}";
    }
}