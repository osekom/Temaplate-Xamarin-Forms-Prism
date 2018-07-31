using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using TemplateSpartaneApp.Models.Catalogs;
using TemplateSpartaneApp.Settings;

namespace TemplateSpartaneApp.Services.Catalogs
{
    public class ProgressReportService : IProgressReportService
    {

        private readonly IProgressReportService progressReportService;

        public ProgressReportService()
        {
            progressReportService = RestService.For<IProgressReportService>(new HttpClient(new HttpLoggingHandler(TokenManager.GetToken)) { BaseAddress = new Uri(AppConfiguration.Values.BaseUrl) }, new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CustomResolver()
                }
            });
        }

        public Task<bool> Delete(int id)
        {
            return progressReportService.Delete(id);
        }

        public Task<ProgressReportModel> Get(int id)
        {
            return progressReportService.Get(id);
        }

        public Task<ProgressReportList> GetAll()
        {
            return progressReportService.GetAll();
        }

        public Task<ProgressReportList> ListaSelAll(int startRowIndex, int maximumRows, string where = null, string order = null)
        {
            return progressReportService.ListaSelAll(startRowIndex, maximumRows, where, order);
        }

        public Task<int> Post([Body] ProgressReportModel item)
        {
            return progressReportService.Post(item);
        }

        public Task<int> Put(int id, [Body] ProgressReportModel item)
        {
            return progressReportService.Put(id, item);
        }
    }
}
