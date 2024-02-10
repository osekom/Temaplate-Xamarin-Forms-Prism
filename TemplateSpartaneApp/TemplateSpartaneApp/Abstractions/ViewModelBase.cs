﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity.Abstractions;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace TemplateSpartaneApp.Abstractions
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IPageLifecycleAware
    {

        #region Vars
        private static string TAG = nameof(ViewModelBase);
        protected INavigationService NavigationService { get; private set; }
        protected IUserDialogs UserDialogsService { get; private set; }
        protected IConnectivity Connectivity { get; set; }
        #endregion

        #region Vars Commands
        public DelegateCommand ReturnToPreviousPageCommand { get; private set; }
        public DelegateCommand ShowMenuCommand { get; set; }
        #endregion

        #region Properties
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion

        #region Constructor
        public ViewModelBase(INavigationService navigationService, IUserDialogs userDialogsService, IConnectivity connectivity)
        {
            NavigationService = navigationService;
            UserDialogsService = userDialogsService;
            Connectivity = connectivity;
            ReturnToPreviousPageCommand = new DelegateCommand(OnReturnToPreviousPageCommandExecuted);
            ShowMenuCommand = new DelegateCommand(() => App.Master.IsPresented = true);
        }
        #endregion

        #region Navigation
        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual void OnNavigatingTo(INavigationParameters parameters) { }

        public virtual void Destroy() { }
        #endregion

        #region Methods Commands
        private async void OnReturnToPreviousPageCommandExecuted()
        {
            await NavigationService.GoBackAsync();
        }
        #endregion

        #region Methods
        protected async Task<ResponseBase<T>> RunSafeApi<T>(Task<T> runMethod)
        {
            var result = new ResponseBase<T>
            {
                Status = TypeReponse.Error,
                Response = default(T)
            };
            if (Connectivity.IsConnected)
            {
                try
                {
                    result.Response = await runMethod;
                    if(!result.Response.Equals(null))
                    {
                        result.Status = TypeReponse.Ok;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message, TAG);
                }
            }
            else
            {
                result.Status = TypeReponse.ErroConnectivity;
                Debug.WriteLine("Error Connectivity", TAG);
            }
            return result;
        }
        #endregion

        #region Methods Cycle Life Page
        public virtual void OnAppearing(){}

        public virtual void OnDisappearing(){}
        #endregion

    }
}
