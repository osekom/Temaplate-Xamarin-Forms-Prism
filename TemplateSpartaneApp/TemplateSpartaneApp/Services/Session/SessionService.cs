using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using TemplateSpartaneApp.Helpers;
using TemplateSpartaneApp.Models.Catalogs;
using TemplateSpartaneApp.Settings;

namespace TemplateSpartaneApp.Services.Session
{
    public class SessionService : ISessionService
    {
        private readonly ISessionService sessionService;

        public SessionService()
        {
            sessionService = RestService.For<ISessionService>(new HttpClient(new HttpLoggingHandler(TokenManager.GetToken)) { BaseAddress = new Uri(AppConfiguration.Values.BaseUrl) }, new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CustomResolver()
                }
            });
        }

        public Task<SpartanUserList> AuthUser(string username, string password)
        {
            return sessionService.AuthUser(username, HelperEncrypt.EncryptPassword(password));
        }

    }
}
