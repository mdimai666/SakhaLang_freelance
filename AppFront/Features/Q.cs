using AppFront.AuthProviders;
using AppFront.Models;
using AppShared.Models;
using AppShared.ViewModels;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppFront
{
    public class Q
    {
        private static Profile _profile;
        public static Profile Profile => _profile ?? _anonymous;

        public static Profile _anonymous = new Profile
        {
            Id = "0",
            DisplayName = "anonymous",
            Phone = "",
            Username = "anonymous",
            Email = "",
        };

        public static Emitter Root = new Emitter();

        public static string AuthToken;

        static UserFromClaims _user = null;

        public static UserFromClaims User { get => _user; }


        static InitialSiteDataViewModel _site;
        public static InitialSiteDataViewModel Site => _site;

        static IServiceProvider _serviceProvider;
        static ILogger _logger;


        public static void AddSrvProv(IServiceProvider sp)
        {
            _serviceProvider = sp;
            //_logger = sp.GetService<ILogger>();//not work
        }

        public static void LogWarn(string text)
        {
            //_logger.LogWarning(text);//not work
        }

        internal static void UpdateInitialSiteData(InitialSiteDataViewModel vm)
        {
            if (vm == null)
            {
                Console.WriteLine("InitialSiteDataViewModel is null");
                return;
            }
            _site = vm;
            Root.Emit(Site.GetType(), vm);

        }

        public static void UpdateProfile(Profile profile)
        {
            if (profile == null)
            {
                _profile = _anonymous;
                Root.Emit(_profile.GetType(), _profile);
                return;
            }

            // Console.WriteLine("UpdateProfile");
            _profile = profile;
            Root.Emit(_profile.GetType(), _profile);
        }

        public static void UpdateNotifyCount(int count)
        {
            Root.Emit("UpdateNotifyCount", count);
        }

        public static void UpdateClaimUser(ClaimsPrincipal claimsPrincipal)
        {
            _user = new UserFromClaims(claimsPrincipal);
            Root.Emit(_user.GetType(), _user);
        }

        public static string ServerUrlJoin(string path)
        {
#if DEBUG
            string domain = "http://localhost:5833/";
#else
        string domain = "https://dev.do.sakha.education:81/";
#endif
            if (path.Contains("://"))
            {
                path = path.Replace("https://dev.do.sakha.education:81", "");
                path = path.Replace("https://do.sakha.education", "");
            }

            return domain + path.TrimStart('/');
        }

    }


}
