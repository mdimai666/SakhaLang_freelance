using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFront.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace AppFront
{
    public class MyJS
    {
        private readonly IJSRuntime js;

        public MyJS(IJSRuntime js)
        {
            this.js = js;
        }

        public async ValueTask JqTrigger(string triggerName){
            // Console.WriteLine("JqTrigger:"+triggerName);
            // js.InvokeVoidAsync($"$(document).trigger('{triggerName}')");
            await js.InvokeVoidAsync("JqTrigger", triggerName);
        }
        public async ValueTask JqTriggerEx(string triggerName, int interval = 1){
            // Console.WriteLine("JqTrigger:"+triggerName);
            // js.InvokeVoidAsync($"$(document).trigger('{triggerName}')");
            await js.InvokeVoidAsync("JqTriggerEx", triggerName, interval);
        }

        public async ValueTask TickerChanged(string symbol, decimal price)
        {
            await js.InvokeVoidAsync("displayTickerAlert1", symbol, price);
        }

        public async ValueTask UpdateFeatherIcons(){
            await js.InvokeVoidAsync("UpdateFeatherIcons");
        }

        public async ValueTask BeauityJsonInSelector(string selector, string value = null){
            await js.InvokeVoidAsync("BeauityJsonInSelector", selector, value);
        }

        public void Dispose()
        {
        }

        //--2gis
        public async ValueTask gis2_init(string location, float zoom)
        {
            await js.InvokeVoidAsync("gis2_init", location, zoom);
        }

        public async ValueTask gis2_setCouriers(IEnumerable<Map2GisMarker> drivers)
        {
            await js.InvokeVoidAsync("gis2_setCouriers", drivers);
        }

        public async ValueTask gis2_removeAll()
        {
            await js.InvokeVoidAsync("gis2_removeAll");
        }
        public async ValueTask gis2_fitBounds()
        {
            await js.InvokeVoidAsync("gis2_fitBounds");
        }

        public class Map2GisClickEvent
        {
            public NPoint containerPoint { get; set; }
            public NPoint layerPoint { get; set; }
            //public MouseEventArgs originalEvent { get; set; }
            public LatLng latlng { get; set; }
            public string type { get; set; }

            public class NPoint
            {
                public int x { get; set; }
                public int y { get; set; }
            }

            public class LatLng
            {
                public float lat { get; set; }
                public float lng { get; set; }

            }
        }

        public class Map2GisPupClickEvent
        {
            public string id { get; set; }
        }
    }
}