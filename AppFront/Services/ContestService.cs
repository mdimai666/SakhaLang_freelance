using AppShared.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppFront.Services
{
    public class ContestService : BasicClientService<Contest>
    {
        public ContestService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {

        }

        public async Task<Contest> ContestDetail(Guid id)
        {
            Contest result = await _client.GET<Contest>($"{_basePath}{_controllerName}/ContestDetail/{id}");
            return result;
        }

        public async Task<Contest> UpdateWithList(Contest entity)
        {
            //return base.Update(entity);
            var result = await _client.PUT<Contest>($"{_basePath}{_controllerName}/UpdateWithList/{entity.Id}", entity);
            return result;
        }

        public async Task<UserActionResult> CalcContest(Guid id)
        {
            UserActionResult result = await _client.GET<UserActionResult>($"{_basePath}{_controllerName}/CalcContest_v1/{id}");
            return result;
        }
    }
}
