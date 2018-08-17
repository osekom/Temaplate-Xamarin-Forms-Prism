using System;
using System.Diagnostics;
using Acr.UserDialogs;
using Plugin.Connectivity.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using TemplateSpartaneApp.Abstractions;
using TemplateSpartaneApp.LocalData;
using TemplateSpartaneApp.Models.Catalogs;
using TemplateSpartaneApp.Services.Session;

namespace TemplateSpartaneApp.ViewModels.Session
{
    public class LogInPageViewModel : ViewModelBase
    {
        
        #region Vars
        private static string TAG = nameof(LogInPageViewModel);
        private ISessionService _sessionService;
        #endregion

        #region Vars Commands
        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand RecoverPassCommand { get; set; }
        #endregion

        #region Properties
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                SetProperty(ref username, value);
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
            }
        }
        private bool remenber;
        public bool Remenber
        {
            get { return remenber; }
            set
            {
                SetProperty(ref remenber, value);
            }
        }
        #endregion

        #region Contructor
        public LogInPageViewModel(ISessionService sessionService, INavigationService navigationService, IUserDialogs userDialogsService, IConnectivity connectivity) : base(navigationService, userDialogsService, connectivity)
        {
            _sessionService = sessionService;
            LogInCommand = new DelegateCommand(LogInCommandExecuted);
            RecoverPassCommand = new DelegateCommand(() => NavigationService.NavigateAsync("RecoverPassword"));
        }
        #endregion

        #region Commands Methods
        private async void LogInCommandExecuted()
        {
            UserDialogsService.ShowLoading("Loading");
            if(string.IsNullOrEmpty(Username))
            {
                UserDialogsService.Alert("User field not entered.", "Alert", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                UserDialogsService.Alert("Password field not entered.", "Alert", "Aceptar");
                return;
            }
            var resp = await RunSafeApi<SpartanUserList>(_sessionService.AuthUser(Username, Password));
            UserDialogsService.HideLoading();
            if (resp.Status == TypeReponse.Ok)
            {
                if (resp.Response.Message != null)
                {
                    UserDialogsService.Alert(resp.Response.Message, "Alert", "Ok");
                    Debug.WriteLine(resp.Response.Message, TAG);
                    return;
                }
                if (resp.Response.SpartanUsers != null && resp.Response.RowCount > 0)
                {
                    var user = resp.Response.SpartanUsers[0];
                    if (user.Username.Equals(Username))
                    {
                        SaveSession(user);
                        await NavigationService.NavigateAsync(new Uri("/Index/Navigation/Home", UriKind.Absolute));
                    }
                    else
                    {
                        UserDialogsService.Alert("Error password.", "Alerta", "Aceptar");
                    }
                }
                else
                {
                    UserDialogsService.Alert("User not register.", "Alerta", "Aceptar");
                }
            }
        }
        #endregion

        #region Methods
        private void SaveSession(UserSpartaneModel user)
        {
            Profile.Instance.Identifier = user.IdUser;
            Profile.Instance.Email = user.Email;
            Profile.Instance.Name = user.Name;
            Profile.Instance.UserName = Remenber ? Username : string.Empty;

            AppSettings.Instance.Logged = Remenber;
            AppSettings.Instance.RememberUserName = Remenber;
        }
        #endregion

    }
}
