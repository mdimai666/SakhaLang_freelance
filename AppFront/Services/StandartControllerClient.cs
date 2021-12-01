using AppShared.Models;
using BlastCore.Exceptions;
using BlastCore.Extensions;
using BlastCore.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace AppFront.Services
{
    public class StandartControllerClient<TEntity> where TEntity : IBasicEntity
    {
        //protected readonly HttpClient _httpClient;
        protected readonly QServer _client;

        protected string _controllerName { get; set; }
        protected string _basePath { get; set; }


        public StandartControllerClient(QServer client)
        {
            //_httpClient = client;
            _client = client;
            //_client.EnsureSuccessStatusCode = false;
            _basePath = "/api/";
            _controllerName = typeof(TEntity).Name;
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await _client.GET<TEntity>($"{_basePath}{_controllerName}/{id}");
        }

        public async Task<TotalResponse<TEntity>> ListTable(QueryFilter filter)
        {
            //string query = "filter="+GetQueryString(filter);
            //string query = GetQueryString(filter);

            string json = JsonSerializer.Serialize(filter);
            var bytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(json));
            var str = Encoding.UTF8.GetString(bytes);
            //var compressed = RemoteLinq.Compress(str);

            string query = $"skip={filter.Skip}&take={filter.Take}&filter=" + Uri.EscapeDataString(str);
            //Console.WriteLine(JsonSerializer.Serialize(filter));
            return await _client.GET<TotalResponse<TEntity>>($"{_basePath}{_controllerName}/ListTable?{query}");
        }

        public async Task<UserActionResult<TEntity>> Add(TEntity entity)
        {
            try
            {
                return await _client.Post<UserActionResult<TEntity>>($"{_basePath}{_controllerName}", entity);
            }
            catch (Exception ex)
            {
                return new UserActionResult<TEntity>
                {
                    Ok = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<UserActionResult<TEntity>> Update(TEntity entity)
        {
            try
            {
                return await _client.PUT<UserActionResult<TEntity>>($"{_basePath}{_controllerName}/{entity.Id}", entity);
            }
            catch (Exception ex)
            {
                return new UserActionResult<TEntity>
                {
                    Ok = false,
                    Message = ex.Message,
                };
            }

        }

        public async Task<UserActionResult> Delete(Guid id)
        {
            try
            {
                return await _client.DELETE<UserActionResult>($"{_basePath}{_controllerName}/{id}");
            }
            catch (Exception ex)
            {
                return new UserActionResult
                {
                    Ok = false,
                    Message = ex.Message
                };
            }
        }

        //public async Task<TEntity> Patch(Guid id, JsonPatchDocument<TEntity> patch)
        //{
        //    return await _client.Patch<TEntity>($"{_basePath}{_controllerName}/{id}", patch);
        //}

        public string GetQueryString(object obj)
        {
            //return Uri.EscapeDataString(JsonSerializer.Serialize(obj));

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            List<string> arr = new List<string>();

            foreach (var p in obj.GetType().GetProperties())
            {
                var val = p.GetValue(obj, null);
                if (val != null)
                {
                    if (p.PropertyType is object)
                    {
                        //arr.Add(p.Name + "=" + HttpUtility.UrlEncode(JsonSerializer.Serialize(val)));
                        arr.Add(p.Name + "=" + (JsonSerializer.Serialize(val)));
                    }
                    else
                    {
                        //arr.Add(p.Name + "=" + HttpUtility.UrlEncode(val.ToString()));
                        arr.Add(p.Name + "=" + (val.ToString()));
                    }
                }
            }

            //return String.Join("&", properties.ToArray());
            return String.Join("&", arr);
        }
    }
}
