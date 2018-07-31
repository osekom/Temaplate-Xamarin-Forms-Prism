using Xamarin.Forms;

namespace TemplateSpartaneApp.Views.Home
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            App.Master = this;
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
