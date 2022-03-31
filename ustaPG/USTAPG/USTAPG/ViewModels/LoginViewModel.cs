namespace USTAPG.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using USTAPG.Services;
    using USTAPG.Views;
    using Xamarin.Forms;
    using Plugin.Connectivity;
    using System;

    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private string _correo;
        private string _clave;
        private bool _iniciado;
        private bool _habilitado;
        #endregion

        #region Propiedades
        public SFirebase Firebase { get; set; }
        public bool Habilitado 
        {
            get { return this._habilitado; }
            set { SetValue(ref this._habilitado, value);}
        }
        public string Correo 
        {
            get { return this._correo; }
            set { SetValue(ref this._correo, value); }
        }
        public string Clave 
        {
            get { return this._clave; }
            set { SetValue(ref this._clave, value); }
        }
        public bool Recordar { get; set; }
        public bool Iniciado 
        {
            get { return this._iniciado; }
            set { SetValue(ref this._iniciado, value); }
        }
        #endregion

        #region Comandos
        public ICommand EntrarCommand 
        {
            get 
            {
                return new RelayCommand(Entrar);
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.Correo = "mtovarson@gmail.com";
            this.Clave = "Alfa199823";
            this.Habilitado = true;
            this.Recordar = true;
            //GenerarData();
        }
        #endregion

        public void GenerarData()
        {
            string t = "{";
            //Dia
            for (int i = 1; i <= 15; i++)
            {
                //Hora
                for (int ii = 1; ii <= 23; ii++)
                {
                    //minutos
                    for (int iii = 15; iii <= 45; iii += 15)
                    {
                        Random rnd = new Random();
                        int value = rnd.Next(1, 50);
                        t += "\"NovPchB" + ii + iii + i + "lD\": {" +
                                "\"Servicio1\": {" +
                                   "\"Date\": \"" + ii + ":" + iii + " "+ i + "/Jun/2022\"," +
                                   "\"Status\": 1," +
                                   "\"Value\":" + value +
                                "}," +
                                "\"Servicio2\": {" +
                                   "\"Date\": \"" + ii + ":" + iii + " " + i + "/Jun/2022\"," +
                                   "\"Status\": 1," +
                                   "\"Value\":" + value +
                                "}," +
                                "\"Servicio3\": {" +
                                   "\"Date\": \"" + ii + ":" + iii + " " + i + "/Jun/2022\"," +
                                   "\"Status\": 1," +
                                   "\"Value\":" + value +
                                "}" +
                            "},";
                    }
                }
            }
            t += "}";
            var y = t;
        }

        #region Métodos
        private async void Entrar()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error de conexión",
                    "Por favor habilita tu conexión a internet.",
                    "Aceptar");
                return;
            }
            var inter = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!inter)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error de conexión",
                    "Verifica tu conexión a internet.",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Correo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresa un correo por favor",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Clave))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresa una clave por favor",
                    "Aceptar");
                this.Clave = string.Empty;
                return;
            }
            this.Habilitado = false;
            this.Iniciado = true;
            
            try
            {
                Firebase = new SFirebase() { Email = this.Correo, Clave = this.Clave };
                Firebase.ClienteAutenticadoConEmail();
                //var a = await Firebase.GetMeasure("a4:cf:12:d9:3f:b7");
            }
            catch (System.Exception e)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No se conectó" + e.Message,
                    "Aceptar");
                return;
            }
            this.Habilitado = true;
            this.Iniciado = false;
            MainViewModel.GetIntance().MacMainPage = new MacViewModel(this.Correo, this.Clave);
            await Application.Current.MainPage.Navigation.PushAsync(new MACPage());
            this.Correo = string.Empty;
            this.Clave = string.Empty;
        }
        #endregion
    }
}
