using AppFront.AuthProviders;
using AppShared.Models;
using Microsoft.JSInterop;

namespace AppFront.Shared
{
    using MenuItem = AppFront.MenuItem;

    public partial class MainLayout
    {




        private List<MenuItem> menu_items;

        protected override void OnAfterRender(bool firstRender)
        {
            // execute conditionally for loading data, otherwise this will load
            // every time the page refreshes
            if (firstRender)
            {
                // Do work to load page data and set properties
                // Console.WriteLine("RENDER!");
            }

            JSRuntime.InvokeVoidAsync("d_onPageLoad");

        }

        protected override void OnInitialized()
        {

            if (Q.Profile.Username == "anonymous")
            {
                //Navigation.NavigateTo($"Login?returnUrl={Uri.EscapeDataString(Navigation.Uri)}");
            }

            Q.Root.On(typeof(Profile), EmitTypeMode.All, d =>
            {
                StateHasChanged();
            });
            Q.Root.On(typeof(UserFromClaims), EmitTypeMode.All, async d =>
            {
                StateHasChanged();
            });

            menu_items = new List<MenuItem>
            {

                new MenuItem { Icon = "oi oi-home",  Title = "Главная", Url = "/",  },
                new MenuItem { Icon = "oi oi-list-rich",  Title = "Новости", Url = "/news",  },
                new MenuItem { Icon = "oi oi-dashboard",  Title = "Контент", Url = "/data/Lesson",  },
                new MenuItem { Icon = "oi oi-document",  Title = "Документы", Url = "/docs",  },
                new MenuItem { Icon = "oi oi-grid-two-up",  Title = "Категории", Url = "/Category",  },
                new MenuItem { Icon = "oi oi-book",  Title = "Уроки", Url = "/Lesson",  },
                new MenuItem { Icon = "oi oi-command",  Title = "Управление", Url = "/manage", SubItemFlag = true,
                    SubItems = new List<MenuItem>(){
                        new MenuItem{ Icon = "oi oi-chevron-right", Title = "Роли", Url = "/RoleManagement", },
                        new MenuItem{ Icon = "oi oi-chevron-right", Title = "Пользователи", Url = "/Users", },
                        new MenuItem{ Icon = "oi oi-chevron-right", Title = "Новости", Url = "/NewsManagement", },
                        new MenuItem{ Icon = "oi oi-chevron-right", Title = "Документы", Url = "/DocumentsManagement", },
                        //new MenuItem{ Icon = "oi oi-chevron-right", Title = "Конкурсы", Url = "/ContestManagment", },
                        new MenuItem{ Icon = "oi oi-chevron-right", Title = "Вопрос ответ", Url = "/FaqPostManagement", },
                        new MenuItem{ Icon = "oi oi-chevron-right", Title = "Контакты", Url = "/ContactsManagement", },

                    }, Role = "Admin",
                },
                //new MenuItem { Icon = "fas fa-tag",  Title = "География", Url = "/geo", SubItemFlag = true,
                //    SubItems = new List<MenuItem>(){
                //        new MenuItem{ Icon = "oi oi-chevron-right", Title = L["GeoLocation.many"], Url = "/geo/GeoLocation", },
                //        new MenuItem{ Icon = "oi oi-chevron-right", Title = L["GeoLocationType.many"], Url = "/geo/GeoLocationType", },
                //        new MenuItem{ Icon = "oi oi-chevron-right", Title = L["GeoMunicipality.many"], Url = "/geo/GeoMunicipality", },
                //        new MenuItem{ Icon = "oi oi-chevron-right", Title = L["GeoMunicType.many"], Url = "/geo/GeoMunicType", },
                //        new MenuItem{ Icon = "oi oi-chevron-right", Title = L["GeoRegion.many"], Url = "/geo/GeoRegion", },
                //        //new MenuItem{ Icon = "oi oi-chevron-right", Title = L["GeoRegionCenter.many"], Url = "/geo/GeoRegionCenter", },

                //    }, Role = "Admin",
                //},

                new MenuItem { Icon = "oi oi-cog",  Title = "Настройки", Url = "/settings", Role="Admin"},
                new MenuItem { Icon = "oi oi-envelope-open",  Title = "Письма", Url = "/FeedbackList", Role="Admin"},
                new MenuItem { Icon = "oi oi-comment-square",  Title = "Вопрос ответ", Url = "/faq",  },
                new MenuItem { Icon = "oi oi-compass",  Title = "Контакты", Url = "/contacts",  },
                
            };


            Load();
        }

        bool collapsed;

        void onCollapse(bool collapsed)
        {
            //Console.WriteLine(collapsed);
            this.collapsed = collapsed;
            _ = localStorage.SetItemAsync("main-layout-sidebar", collapsed);
        }

        async void Load()
        {
            try
            {
                collapsed = await localStorage.GetItemAsync<bool>("main-layout-sidebar");
                //Console.WriteLine("collapsed = " + collapsed);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("collapsed" + ex.Message);
            }
            StateHasChanged();
        }

    }
}
