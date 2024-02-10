using Acr.UserDialogs;
using Plugin.Connectivity.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using TemplateSpartaneApp.Abstractions;
using TemplateSpartaneApp.Models.Catalogs;
using TemplateSpartaneApp.Services.Catalogs;

namespace TemplateSpartaneApp.ViewModels.Popups
{
    public class ProgressReportPopupViewModel : ViewModelBase
    {
        #region Vars
        private static string TAG = nameof(ProgressReportPopupViewModel);
        private readonly IProgressReportService _progressReportService;
        #endregion

        #region Vars Commands
        public DelegateCommand SaveCommand { get; set; }
        #endregion

        #region Properties
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                SetProperty(ref description, value);
            }
        }
        private ProgressReportModel progressReportModel;
        public ProgressReportModel ProgressReportModel
        {
            get { return progressReportModel; }
            set
            {
                SetProperty(ref progressReportModel, value);
            }
        }
        #endregion

        public ProgressReportPopupViewModel(IProgressReportService progressReportService, INavigationService navigationService, IUserDialogs userDialogsService, IConnectivity connectivity) : base(navigationService, userDialogsService, connectivity)
        {
            _progressReportService = progressReportService;
            SaveCommand = new DelegateCommand(SaveCommandExecute);
        }

        #region Methods Commands
        private async void SaveCommandExecute()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await UserDialogsService.AlertAsync("Add description", "Alert", "Ok");
                return;
            }
            var newProgressReport = new ProgressReportModel
            {
                Description = Description
            };
            var result = new ResponseBase<int>();
            if(ProgressReportModel != null)
            {
                newProgressReport.ReportId = ProgressReportModel.ReportId;
                result = await RunSafeApi<int>(_progressReportService.Put(ProgressReportModel.ReportId,newProgressReport));
            }
            else
            {
                result = await RunSafeApi<int>(_progressReportService.Post(newProgressReport));
            }
            if (result.Status == TypeReponse.Ok)
            {
                if (result.Response != -1)
                {
                    await UserDialogsService.AlertAsync("Success Task", "Success", "Ok");
                    var navigationParams = new NavigationParameters
                    {
                        {"refresh", true}
                    };
                    await NavigationService.GoBackAsync(navigationParams);
                }
                else
                {
                    await UserDialogsService.AlertAsync("Error", "Alert", "Ok");
                }
            }
        }
        #endregion

        #region Navigation Methods
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if(parameters.ContainsKey("currentProgressReport"))
            {
                parameters.TryGetValue("currentProgressReport", out progressReportModel);
                if(ProgressReportModel != null)
                {
                    Description = ProgressReportModel.Description;
                }
            }
        }
        #endregion
    }
}
