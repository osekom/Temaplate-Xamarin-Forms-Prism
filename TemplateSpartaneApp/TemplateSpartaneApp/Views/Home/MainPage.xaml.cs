using Xamarin.Forms;

namespace TemplateSpartaneApp.Views.Home
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}