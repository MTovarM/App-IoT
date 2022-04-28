namespace USTAPG.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using ZXing.Net.Mobile.Forms;
    using Xamarin.Forms;
    using USTAPG.Views;
    using USTAPG.Services;
    using System.Collections.Generic;
    using USTAPG.Models;
    using System.Linq;
    using System.IO;
    using System.Threading.Tasks;

    public class MacViewModel:BaseViewModel
    {
        #region Atributos
        private bool _iniciado;
        private bool _habilitado;
        private bool _habilitado1;
        #endregion

        #region Propiedaes

        public SFirebase Firebase { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set;}
        public bool Iniciado
        {
            get { return this._iniciado; }
            set { SetValue(ref this._iniciado, value); }
        }
        public bool Habilitado
        {
            get { return this._habilitado; }
            set { SetValue(ref this._habilitado, value); }
        }
        public bool Habilitado1
        {
            get { return this._habilitado1; }
            set { SetValue(ref this._habilitado1, value); }
        }
        #endregion

        #region Comandos
        public ICommand MacQr
        {
            get
            {
                return new RelayCommand(MacQrM);
            }
        }
        public ICommand MacText
        {
            get
            {
                return new RelayCommand(MacTextM);
            }
        }
        #endregion

        #region Constructor
        public MacViewModel(string _correo, string _clave)
        {
            this.Correo = _correo;
            this.Clave = _clave;
            this.Iniciado = false;
            ValidarpriVez();
        }
        #endregion

        #region Metodos
        public void Botones(bool _value)
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                this.Habilitado = _value;
                this.Habilitado1 = false;
            }
            else
            {
                this.Habilitado = _value;
                this.Habilitado1 = _value;
            }
        }

        private void ValidarpriVez()
        {
            if (MainViewModel.GetIntance().YaExiste)
            {
                SiguientePaso(MainViewModel.GetIntance().U_Usuario[0].MAC, true);
            }
            else 
            {
                Botones(true);
                return; 
            }
        }
        
        private async void MacTextM()
        {
            MainViewModel.GetIntance().MacTextPage = new MACInTextViewModel(this.Correo, this.Clave);
            await Application.Current.MainPage.Navigation.PushAsync(new MACInTextPage());            
        }

        private async void MacQrM()
        {
            try
            {
                var qr = new ZXing.Mobile.MobileBarcodeScanner();
                qr.TopText = "Escanea el código por favor";
                qr.BottomText = "Proyecto de Grado USTA 2022";
                var lectura = await qr.Scan();
                if (lectura != null) SiguientePaso(lectura.Text, false);
                else { }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Error escaneando el código, intente de nuevo.",
                    "Aceptar");
                this.Iniciado = false;
            }
            this.Iniciado = false;
        }

        public async void GuardarDB(bool f)
        {
            if (f)
            {
                await MainViewModel.GetIntance().DB.UpdatePersonAsync(MainViewModel.GetIntance().U_Usuario[0]);
            }
            else
            {
                await MainViewModel.GetIntance().DB.SavePersonAsync(new Usuario
                {
                    User = MainViewModel.GetIntance().SUsuario,
                    Clave = MainViewModel.GetIntance().SClave,
                    MAC = MainViewModel.GetIntance().SMAC
                });
            }
        }

        public async void SiguientePaso(string lectura, bool _first)
        {
            Botones(false);
            List<measureTable> Datos = new List<measureTable>();
            List<InfoTable> Info = new List<InfoTable>();
            MeterTable Meter = new MeterTable();
            Encriptacion Codificacion = new Encriptacion();
            //var _text = Codificacion.Encriptar(lectura.Text);
            string UN_MAC = "";
            if (_first) UN_MAC = lectura;
            else UN_MAC = Codificacion.DesEncriptar(lectura);
            this.Iniciado = true;
            try
            {
                Firebase = new SFirebase() { Email = this.Correo, Clave = this.Clave };
            }
            catch (System.Exception e)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Hubo un problema al conectar la base de datos, revise su conexión a internet.",
                    "Aceptar");
                Botones(true);
                this.Iniciado = false;
                return;
            }
            try
            {
                Datos = await Firebase.GetMeasure(UN_MAC);//"a4:cf:12:d9:3f:b7");
                Info = await Firebase.GetInfo(UN_MAC);
                Meter = await Firebase.GetMeter(UN_MAC);
                if (string.IsNullOrEmpty(Meter.Gateway))
                {
                    await Application.Current.MainPage.DisplayAlert(
                    "Medidor",
                    "No se encontró información del medidor, por favor contacte a soporte.",
                    "Aceptar");
                    Botones(true);
                    this.Iniciado = false;
                    return;
                }
                if(!await VerificarUsuario(Firebase, UN_MAC))
                {
                    await Application.Current.MainPage.DisplayAlert(
                    "Sesion",
                    "No tiene permitido ingresar a la información de este medidor.  Contacte a soporte.",
                    "Aceptar");
                    Botones(true);
                    this.Iniciado = false;
                    return;
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No se encontró el medidor, intente de nuevo.",
                    "Aceptar");
                Botones(true);
                this.Iniciado = false;
                return;
            }

            MainViewModel.GetIntance().SMAC = UN_MAC;
            MainViewModel.GetIntance().MeasureTable = Datos;
            MainViewModel.GetIntance().InfoTable = Info;
            MainViewModel.GetIntance().Metertable = Meter;
            GuardarDB(MainViewModel.GetIntance().YaExiste);
            MainViewModel.GetIntance().Meter = new MeterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new MenuTabbedPage());
            Botones(true);
            this.Iniciado = false;
        }

        public async Task<bool> VerificarUsuario(SFirebase _fb, string _medidor)
        {
            bool val = false;
            var usuarios = await _fb.GetSesion(_medidor);
            foreach (var u in usuarios) if (u.Email == _fb.Email) return true;
            return val;
        }
        #endregion
    }
}
