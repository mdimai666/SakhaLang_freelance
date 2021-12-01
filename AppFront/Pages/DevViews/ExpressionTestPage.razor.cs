using AntDesign;
using AppFront.Services;
using AppShared.Models;
using BlastCore.Extensions;
using BlastCore.Features;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Remote.Linq;
using Remote.Linq.Newtonsoft.Json;
using Remote.Linq.Text.Json;
using System.Linq.Expressions;
//using Newtonsoft.Json;
using System.Text.Json;

namespace AppFront.Pages.DevViews
{
    public partial class ExpressionTestPage
    {

        [Inject] QServer server { get; set; }
        [Inject] GeoLocationService geoLocationService { get; set; }
        [Inject] MessageService messageService { get; set; }

        IEnumerable<GeoLocation> geoLocations { get; set; }
        int total = 0;


        int SIZE = 0;
        int SIZE2 = 0;
        string MSG;
        string REQ;
        string REQ2;
        string REQ3;

        async void OnBtn1Click()
        {
            //var result = await geoLocationService.ListTable(new QueryFilter().AddQuery<GeoLocation>(s => s.Name.ToLower().Contains("íüóðáà")));


            //var result = await geoLocationService.ListTable(new QueryFilter().AddQuery<GeoLocation>(s => s.OKTMO.ToLower().Contains("40111") || s.Name.Contains("¸")));

            var q = new QueryFilter().AddQuery<GeoLocation>(search);
            SIZE = q.JsonExpression.Length;

            string json = System.Text.Json.JsonSerializer.Serialize(q);
            string query = "filter=" + Uri.EscapeDataString(json);
            SIZE2 = query.Length;


            string newQ = RemoteLinq.Compress(json);
            REQ = newQ;

            REQ2 = json;
            REQ3 = RemoteLinq.Decompress(newQ);

            MSG = $"Equal = {REQ.Equals(REQ2)}";

            StateHasChanged();

            //var result = await geoLocationService.ListTable(q);

            //if (result.Ok)
            //{
            //    geoLocations = result.Records;
            //    total = result.TotalCount;
            //    _ = messageService.Success("OK");

            //} else
            //{
            //    _ = messageService.Error("error");
            //}
            StateHasChanged();

        }

        string SearchText = "à";

        Expression<Func<GeoLocation, bool>> search =>
            model => model.OKTMO.Contains(SearchText.ToLower())
            || model.Name.ToLower().Contains(SearchText.ToLower())
            || model.ShortName.ToLower().Contains(SearchText.ToLower());

        

        async void OnBtn2Click()
        {
            Expression<Func<GeoLocation, bool>> expression = user => user.OKTMO == "98641445116" && user.Name.ToLower().Contains("éûû");

            //Remote.Linq.Expressions.LambdaExpression qu = expression.ToRemoteLinqExpression();
            Remote.Linq.Expressions.LambdaExpression qu = search.ToRemoteLinqExpression();

            JsonSerializerOptions serializerSettings = new JsonSerializerOptions().ConfigureRemoteLinq();

            string remote1 = RemoteLinq.SerialiseRemoteExpression(qu);

            QueryFilter filter = new QueryFilter();

            Console.WriteLine("qu=" + qu);

            //var serializer = new ExpressionSerializer(new System.Text.Json.JsonSerializer());

            //var x = expression.ToExpressionNode();
            //var expressionSerializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
            //var serialized = expressionSerializer.SerializeText(expression);

            //var x = Uri.EscapeDataString(serialized);

            //var x = Uri.EscapeDataString(RemoteLinq.Compress(remote1));
            var x = (remote1);

            SIZE = remote1.Length;
            StateHasChanged();


            //var result = await server.GET<TotalResponse<GeoLocation>>($"api/GeoLocation/ListTable2?remote1={remote1}&q={qu}&x={x}");
            var result = await server.GET<TotalResponse<GeoLocation>>($"api/GeoLocation/ListTable2?&x={x}");

        }

    }
}