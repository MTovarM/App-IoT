namespace USTAPG
{
    using Xamarin.Forms;
    using Views;

    public partial class App : Application
    {
        #region Constructor
        public App()
        {
            Device.SetFlags(new[] { "RadioButton_Experimental" });
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
        #endregion

        #region Métodos
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        } 
        #endregion
    }
}
