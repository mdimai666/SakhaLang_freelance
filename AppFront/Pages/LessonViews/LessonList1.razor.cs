using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;

namespace AppFront.Pages.LessonViews
{
    public partial class LessonList1
    {
        [Parameter] public List<Lesson> Items { get; set; } = null;
        [Inject] public LessonService service { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Load();
        }


        async void Load()
        {
            var result = await service.ListTable(new QueryFilter(1, 30));
            Items = result.Records.ToList();
            StateHasChanged();
        }
    }
}