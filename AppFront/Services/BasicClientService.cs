using AntDesign;
using AntDesign.TableModels;
using AppFront.Models;
using AppShared.Models;
using BlastCore.Extensions;
using BlastCore.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppFront.Services
{
    public class BasicClientService<TEntity> where TEntity : IBasicEntity
    {
        protected readonly StandartControllerClient<TEntity> controller;
        protected readonly QServer _client;
        private readonly IServiceProvider serviceProvider;

        protected string _controllerName { get; set; }
        protected string _basePath { get; set; }
        protected MessageService messageService { get; }

        public BasicClientService(IServiceProvider serviceProvider, HttpClient httpClient)
        {
            _basePath = "/api/";
            _controllerName = typeof(TEntity).Name;

            _client = new QServer(httpClient);
            controller = new StandartControllerClient<TEntity>(_client);
            this.serviceProvider = serviceProvider;
            this.messageService = GetService<MessageService>();
        }

        protected T GetService<T>()
        {
            return (T)serviceProvider.GetService(typeof(T));
        }

        public virtual async Task<TEntity> Get(Guid id)
        {
            return await controller.Get(id);
        }

        public virtual async Task<TotalResponse<TEntity>> ListTable(QueryFilter filter)
        {
            return await controller.ListTable(filter);
        }

        public virtual async Task<TotalResponse<TEntity>> ListTable<TQueryModel>(QueryModel<TQueryModel> queryModel)
        {
            return await controller.ListTable(queryModel.AsQueryFilter());
        }

        public virtual async Task<UserActionResult<TEntity>> Add(TEntity entity)
        {
            return await controller.Add(entity);
        }

        public virtual async Task<UserActionResult<TEntity>> Update(TEntity entity)
        {
            return await controller.Update(entity);

        }

        public virtual async Task<TEntity> SmartSave(bool addNew, TEntity entity)
        {
            UserActionResult<TEntity> result;

            if (addNew)
                result = await controller.Add(entity);
            else
                result = await controller.Update(entity);

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
                return result.Data;
            }
            else
            {
                _ = messageService.Error(result.Message);
                return default(TEntity);
            }


        }


        public virtual async Task<UserActionResult> Delete(Guid id)
        {
            return await controller.Delete(id);
        }

        //
        //public async Task<T> GetViewModel<T>()
        //{
        //    string name = typeof(T).Name;
        //    return await _client.GET<T>($"/vm/ViewModels/Get/{name}");

        //}
    }


}
