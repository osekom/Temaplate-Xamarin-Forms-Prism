using Acr.UserDialogs;
using Plugin.Connectivity.Abstractions;
using Prism.Navigation;
using TemplateSpartaneApp.Abstractions;

namespace TemplateSpartaneApp.ViewModels.Session
{
    public class RecoverPasswordPageViewModel : ViewModelBase
    {
        public RecoverPasswordPageViewModel(INavigationService navigationService, IUserDialogs userDialogsService, IConnectivity connectivity) : base(navigationService, userDialogsService, connectivity)
        {
        }
    }
}
