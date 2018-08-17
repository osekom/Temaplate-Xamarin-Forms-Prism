using Acr.UserDialogs;
using Plugin.Connectivity.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using TemplateSpartaneApp.Abstractions;
using TemplateSpartaneApp.Models.Catalogs;
using TemplateSpartaneApp.Services.Catalogs;
using TemplateSpartaneApp.Views.Popups;

namespace TemplateSpartaneApp.ViewModels.Home
{
    public class MainPageViewModel : ViewModelBase
    {

        #region Vars
        public ObservableCollectionExt<ProgressReportModel> Items { get; set; }
        private readonly IProgressReportService _progressReportService;
        #endregion

        #region Vars Commands
        public DelegateCommand SelectItemCommand { get; set; }
        public DelegateCommand AddItemCommand { get; set; }
        #endregion

        #region Properties
        private ProgressReportModel progressReportModel;
        public ProgressReportModel ProgressReportModel
        {
            get { return progressReportModel; }
            set
            {
                SetProperty(ref progressReportModel, value);
                SelectItemCommand.Execute();
            }
        }
        #endregion

        #region Contructor
        public MainPageViewModel(IProgressReportService progressReportService,
                                 INavigationService navigationService,
                                 IUserDialogs userDialogsService,
                                 IConnectivity connectivity) : base(navigationService, userDialogsService, connectivity)
        {
            _progressReportService = progressReportService;
            SelectItemCommand = new DelegateCommand(SelectItemCommandExecute);
            AddItemCommand = new DelegateCommand(AddItemCommandExecute);
            Items = new ObservableCollectionExt<ProgressReportModel>();
        }
        #endregion

        #region Populating
        private async void PopulatingProgressReportList()
        {
            var result = await RunSafeApi<ProgressReportList>(_progressReportService.ListaSelAll(1, 100));
            if (result.Status == TypeReponse.Ok)
            {
                if (result.Response.Message != null)
                {
                    UserDialogsService.Alert(result.Response.Message, "Alert", "Ok");
                    return;
                }
                if (result.Response.ProgressReports != null && result.Response.RowCount > 0)
                {
                    Items.Reset(result.Response.ProgressReports);
                }
                else
                {
                    UserDialogsService.Alert("No Data", "Alert", "Ok");
                }
            }
        }
        #endregion

        #region Commands Methods
        private async void SelectItemCommandExecute()
        {
            if (ProgressReportModel == null) return;
            var result = await UserDialogsService.ConfirmAsync("What do you want to do?", "Alert", "Edit", "Delete");
            if (result)
            {
                var navigationParams = new NavigationParameters
                {
                    {"currentProgressReport", ProgressReportModel}
                };
                await NavigationService.NavigateAsync(nameof(ProgressReportPopup), navigationParams);
            }
            else
            {
                var resultDelete = await RunSafeApi<bool>(_progressReportService.Delete(ProgressReportModel.ReportId));
                if (resultDelete.Status == TypeReponse.Ok)
                {
                    if (resultDelete.Response)
                    {
                        await UserDialogsService.AlertAsync("Delete success", "Success", "Ok");
                        Items.Remove(ProgressReportModel);
                    }
                    else
                    {
                        await UserDialogsService.AlertAsync("Delete error", "Alert", "Ok");
                    }
                }
            }
            progressReportModel = null;
        }
        private void AddItemCommandExecute()
        {
            NavigationService.NavigateAsync(nameof(ProgressReportPopup));
        }
        #endregion

        #region Navigation Params
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("refresh"))
            {
                if (parameters.GetValue<bool>("refresh"))
                {
                    PopulatingProgressReportList();
                }
            }
        }
        #endregion

        #region Methods Life Cycle Page
        public override void OnAppearing()
        {
            PopulatingProgressReportList();
        }
        #endregion

    }
}
