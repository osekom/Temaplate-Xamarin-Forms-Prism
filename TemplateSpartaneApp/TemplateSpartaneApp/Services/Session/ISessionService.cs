using System.Threading.Tasks;
using Refit;
using TemplateSpartaneApp.Models.Catalogs;

namespace TemplateSpartaneApp.Services.Session
{
    public interface ISessionService
    {
        [Get("/Spartan_User/ListaSelAll?startRowIndex=1&maximumRows=1&Where=Username='{username}' COLLATE SQL_Latin1_General_CP1_CS_AS And Password='{password}' COLLATE SQL_Latin1_General_CP1_CS_AS")]
        [Headers("Authorization: Bearer")]
        Task<SpartanUserList> AuthUser(string username, string password);
    }
}
