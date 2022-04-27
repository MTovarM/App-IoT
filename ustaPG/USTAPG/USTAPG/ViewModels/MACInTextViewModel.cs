namespace USTAPG.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using System;
    using USTAPG.Views;
    using USTAPG.Services;
    using System.Collections.Generic;
    using USTAPG.Models;
    using System.Linq;
    using System.IO;

    public class MACInTextViewModel:BaseViewModel
    {
        #region Atributos
        public string mac;
        public bool _habilitado;
        private bool _iniciado;
        #endregion

        #region Propíedades
        public string Correo { get; set; }
        public string Clave { get; set; }
        public SFirebase Firebase { get; set; }
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
        public string MAC 
        {
            get { return this.mac; }
            set { SetValue(ref this.mac, value);}
        }
        #endregion

        #region Comandos
        public ICommand Hecho
        {
            get
            {
                return new RelayCommand(HechoM);
            }
        }
        #endregion

        #region Constructor
        public MACInTextViewModel(string _correo, string _clave)
        {
            this.Clave = _clave;
            this.Correo = _correo;
            this.MAC = string.Empty;
            this.Habilitado = true;
        }
        #endregion

        #region Metodos
        private async void HechoM()
        {
            if (string.IsNullOrEmpty(MAC))
            {
                await Application.Current.MainPage.DisplayAlert(
                       "Error",
                       "Ingrese la dirección MAC del equipo.",
                       "Aceptar");
                return;
            }
            MainViewModel.GetIntance().SMAC = this.MAC;
            SiguientePaso(this.MAC, true);
        }

        public async void SiguientePaso(string lectura, bool _first)
        {
            List<measureTable> Datos = new List<measureTable>();
            List<InfoTable> Info = new List<InfoTable>();
            MeterTable Meter = new MeterTable();
            Encriptacion Codificacion = new Encriptacion();
            //var _text = Codificacion.Encriptar(lectura.Text);
            string UN_MAC = lectura;
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
                    "No se encontró información del medidor, verifique la dirección ingresada o contacte a soporte.",
                    "Aceptar");
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
                this.Iniciado = false;
                return;
            }

            MainViewModel.GetIntance().SMAC = UN_MAC;
            MainViewModel.GetIntance().MeasureTable = Datos;
            MainViewModel.GetIntance().InfoTable = Info;
            MainViewModel.GetIntance().Metertable = Meter;
            await MainViewModel.GetIntance().DB.SavePersonAsync(new Usuario
            {
                User = MainViewModel.GetIntance().SUsuario,
                Clave = MainViewModel.GetIntance().SClave,
                MAC = MainViewModel.GetIntance().SMAC
            });
            MainViewModel.GetIntance().Meter = new MeterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new MenuTabbedPage());
            this.Iniciado = false;
        }
        #endregion
    }
}
