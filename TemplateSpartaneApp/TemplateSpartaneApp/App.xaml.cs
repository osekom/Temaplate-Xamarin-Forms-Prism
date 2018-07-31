using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Acr.UserDialogs;
using Plugin.Connectivity.Abstractions;
using Plugin.Settings.Abstractions;
using Plugin.Connectivity;
using Plugin.Settings;
using TemplateSpartaneApp.LocalData;
using SQLite;
using System;
using System.Diagnostics;
using TemplateSpartaneApp.Helpers;
using TemplateSpartaneApp.Views.Home;
using TemplateSpartaneApp.ViewModels.Home;
using TemplateSpartaneApp.Views.Session;
using TemplateSpartaneApp.ViewModels.Session;
using TemplateSpartaneApp.Services.Session;
using TemplateSpartaneApp.Services.Catalogs;
using Prism.Plugin.Popups;
using TemplateSpartaneApp.Views.Popups;
using TemplateSpartaneApp.ViewModels.Popups;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TemplateSpartaneApp
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        #region Vars
        public static string TAG = nameof(App);
        public static MasterDetailPage Master { get; set; }
        public static App CurrentInstance { get; private set; }
        public static SQLiteConnection SQLiteConnect;
        #endregion

        protected override async void OnInitialized()
        {
            InitializeComponent();
            CurrentInstance = this;
            AppSettings.Instance.Initialize(Container.Resolve<ISettings>());
            Profile.Instance.Initialize(Container.Resolve<ISettings>());
            InitServiceSQLite();
            if (AppSettings.Instance.Logged)
            {
                await NavigationService.NavigateAsync(new Uri("/Index/Navigation/Home", UriKind.Absolute));
            }
            else
            {
                await NavigationService.NavigateAsync(new Uri("/Navigation/LogIn", UriKind.Absolute));
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            #region Navigation
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>("Index");
            containerRegistry.RegisterForNavigation<NavigationPage>("Navigation");
            #endregion

            #region Pages
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>("Home");
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>("LogIn");
            containerRegistry.RegisterForNavigation<RecoverPasswordPage, RecoverPasswordPageViewModel>("RecoverPassword");
            #endregion

            #region Popups
            containerRegistry.RegisterForNavigation<ProgressReportPopup, ProgressReportPopupViewModel>();
            #endregion

            #region Instances
            containerRegistry.RegisterInstance(typeof(IUserDialogs), UserDialogs.Instance);
            containerRegistry.RegisterInstance(typeof(IConnectivity), CrossConnectivity.Current);
            containerRegistry.RegisterInstance(typeof(ISettings), CrossSettings.Current);
            containerRegistry.RegisterPopupNavigationService();
            #endregion

            #region Services
            containerRegistry.Register<ISessionService, SessionService>();
            containerRegistry.Register<IProgressReportService, ProgressReportService>();
            #endregion

            #region Repositories
            #endregion
        }

        private void InitServiceSQLite()
        {
            try
            {
                SQLiteConnect = new SQLiteConnection(Container.Resolve<IPathService>().GetDatabasePath());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, TAG);
            }
        }

    }
}
