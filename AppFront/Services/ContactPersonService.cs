using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppShared.Models;


namespace AppFront.Services
{
    public class ContactPersonService : BasicClientService<ContactPerson>
    {
        List<ContactPerson> _cachedItems = null;

        public ContactPersonService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }

        //public override async Task<TotalResponse<ContactPerson>> ListTable(QueryFilter filter)
        //{
        //    var res = await base.ListTable(filter);
        //    _cachedItems = res?.Records.ToList() ?? new();
        //    return res;
        //}

        //public override async Task<TotalResponse<ContactPerson>> ListTable<TQueryModel>(AntDesign.TableModels.QueryModel<TQueryModel> queryModel)
        //{
        //    var res = await base.ListTable(queryModel);
        //    _cachedItems = res?.Records.ToList() ?? new();
        //    return res;
        //}

        public async Task<List<ContactPerson>> GetCached()
        {
            if (_cachedItems is null)
            {
                var res = await ListTable(new QueryFilter(1, 20));
                _cachedItems = res.Records.ToList();
            }
            return _cachedItems;
        }
    }

}