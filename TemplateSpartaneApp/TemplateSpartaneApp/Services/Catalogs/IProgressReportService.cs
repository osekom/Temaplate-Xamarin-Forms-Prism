using System;
using System.Threading.Tasks;
using Refit;
using TemplateSpartaneApp.Models.Catalogs;

namespace TemplateSpartaneApp.Services.Catalogs
{
    public interface IProgressReportService
    {
        [Get("/Progress_Report/Get")]
        [Headers("Authorization: Bearer")]
        Task<ProgressReportModel> Get(int id);

        [Get("/Progress_Report/GetAll")]
        [Headers("Authorization: Bearer")]
        Task<ProgressReportList> GetAll();

        [Get("/Progress_Report/ListaSelAll")]
        [Headers("Authorization: Bearer")]
        Task<ProgressReportList> ListaSelAll(int startRowIndex, int maximumRows, string where = null, string order = null);

        [Post("/Progress_Report/Post")]
        [Headers("Authorization: Bearer")]
        Task<int> Post([Body] ProgressReportModel item);

        [Put("/Progress_Report/Put")]
        [Headers("Authorization: Bearer")]
        Task<int> Put(int id, [Body] ProgressReportModel item);

        [Delete("/Progress_Report/Delete")]
        [Headers("Authorization: Bearer")]
        Task<bool> Delete(int id);
    }
}
