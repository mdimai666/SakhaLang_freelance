using AntDesign;
using AntDesign.TableModels;
using AppFront.AuthProviders;
using AppFront.Models;
using AppFront.Services;
using AppShared.Models;
using BlastCore.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppFront.Shared
{
    public partial class BasicPage<TModel>
    {

        public bool Busy { get; set; } = true;

        [Parameter]
        public RenderFragment WhenLoading { get; set; }

        [Parameter]
        public RenderFragment<Exception> ErrorMessageTemplate { get; set; }

        [Parameter]
        public RenderFragment<TModel> ChildContent { get; set; }

        public Exception Error { get; set; }

        //public TModel model { get; set; }


        [Parameter]
        public Func<Task<TModel>> LoadFunc { get; set; }

        public bool Errored
        {
            get
            {
                return Error != null;
            }
        }

        //[Inject]
        //public DriverService service { get; set; }

        //[Parameter]
        //public Guid ID { get; set; }

        [Parameter]
        public TModel model { get; set; } = default(TModel);

        [Parameter]
        public EventCallback<TModel> modelChanged { get; set; }

        protected override async Task<Task> OnInitializedAsync()
        {
            _ = StartLoad();

            return base.OnInitializedAsync();

        }

        //protected override void OnAfterRender(bool firstRender)
        //{
        //    base.OnAfterRender(firstRender);

        //    _ = StartLoad();
        //}


        public async virtual Task StartLoad()
        {
            try
            {
                while (Q.Profile.Id == "0") {
                    await Task.Delay(10);
                }

                //Console.WriteLine("=1=");
                var task = LoadFunc();
                //Console.WriteLine("=2+"+task);
                model = await task;
                _ = modelChanged.InvokeAsync(model);

                //ChildContent(model);

                //if(model is Driver d)
                //{
                //    Console.WriteLine("=dr" + d?.FullName);

                //}
                //Console.WriteLine("=3=" + model);
                //model = await LoadFunc();

            }
            catch (Exception ex)
            {
                Error = ex;
                //throw;
            }
            finally
            {
                Busy = false;
            }

            StateHasChanged();

        }

        //public async virtual Task<TModel> LoadFunc()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
