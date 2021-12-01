using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFront.Pages.StatisticsView
{
    public partial class ChartCard
    {
        [Parameter]
        public string Avatar { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment Action { get; set; }

        [Parameter]
        public string Total { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment Footer { get; set; }

        [Parameter]
        public string ContentHeight { get; set; }
    }
}
