namespace USTAPG.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using USTAPG.Services;
    using USTAPG.Views;
    using Xamarin.Forms;
    using Plugin.Connectivity;
    using System;
    using System.IO;
    using USTAPG.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private string _correo;
        private string _clave;
        private bool _iniciado;
        private bool _habilitado;
        #endregion

        #region Propiedades
        public bool IsUserCreated { get; set; }
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
            //this.Correo = "mtovarson@gmail.com";
            //this.Clave = "Alfa199823";
            this.Habilitado = true;
            this.Recordar = true;
            ValidarUsuario();
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
        public async void ValidarUsuario()
        {
            MainViewModel.GetIntance().DB = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "prueba1.db3"));
            var u = await GetUser();
            MainViewModel.GetIntance().U_Usuario = new List<Usuario>();
            if (u.Count > 0)
            {
                MainViewModel.GetIntance().U_Usuario.Add(u[0]);
                this.Correo = MainViewModel.GetIntance().U_Usuario[0].User;
                this.Clave = MainViewModel.GetIntance().U_Usuario[0].Clave;
            }
            else
            {
                this.Correo = string.Empty;
                this.Clave = string.Empty;
            }
        }

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

            if (this.Recordar)
            {
                if (MainViewModel.GetIntance().U_Usuario.Count > 0)
                {
                    MainViewModel.GetIntance().U_Usuario[0].Clave = this.Clave;
                    MainViewModel.GetIntance().U_Usuario[0].User = this.Correo;
                }
                else
                {
                    MainViewModel.GetIntance().U_Usuario.Add(new Usuario()
                    {
                        User = this.Correo,
                        Clave = this.Clave
                    });
                }
            }
            else
            {
                if (MainViewModel.GetIntance().U_Usuario.Count > 0)
                {
                    MainViewModel.GetIntance().U_Usuario[0].Clave = string.Empty;
                    MainViewModel.GetIntance().U_Usuario[0].User = string.Empty;
                }
                else
                {
                    MainViewModel.GetIntance().U_Usuario.Add(new Usuario()
                    {
                        User = string.Empty,
                        Clave = string.Empty
                    });
                }
            }
            MainViewModel.GetIntance().SUsuario = this.Correo;
            MainViewModel.GetIntance().SClave = this.Clave;
            MainViewModel.GetIntance().MacMainPage = new MacViewModel(this.Correo, this.Clave);
            await Application.Current.MainPage.Navigation.PushAsync(new MACPage());
            this.Correo = string.Empty;
            this.Clave = string.Empty;
        }

        public async Task<List<Usuario>> GetUser()
        {
            var a = await MainViewModel.GetIntance().DB.GetPeopleAsync<Usuario>();
            return a;
        }
        #endregion
    }
}
