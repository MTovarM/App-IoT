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

    public class MacViewModel:BaseViewModel
    {
        #region Atributos
        private bool _iniciado;
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
        #endregion

        #region Comandos
        public ICommand MacQr
        {
            get
            {
                return new RelayCommand(MacQrM);
            }
        }
        #endregion

        #region Constructor
        public MacViewModel(string _correo, string _clave)
        {
            this.Correo = _correo;
            this.Clave = _clave;
            this.Iniciado = false;
        }
        #endregion

        #region Metodos
        private async void MacQrM()
        {
            try
            {
                List<measureTable> Datos = new List<measureTable>();
                List<InfoTable> Info = new List<InfoTable>();
                MeterTable Meter = new MeterTable();
                var qr = new ZXing.Mobile.MobileBarcodeScanner();
                qr.TopText = "Escanea el código por favor";
                qr.BottomText = "Proyecto de Grado USTA 2022";
                var lectura = await qr.Scan();
                if (lectura != null)
                {
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
                        Datos = await Firebase.GetMeasure(lectura.Text);//"a4:cf:12:d9:3f:b7");
                        Info = await Firebase.GetInfo(lectura.Text);
                        Meter = await Firebase.GetMeter(lectura.Text);
                        if (string.IsNullOrEmpty(Meter.Gateway)) await Application.Current.MainPage.DisplayAlert(
                            "Medidor",
                            "No se encontró información del medidor, por favor contacte a soporte.",
                            "Aceptar");
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
                    var t = lectura.Text;

                    MainViewModel.GetIntance().MeasureTable = Datos;
                    MainViewModel.GetIntance().InfoTable = Info;
                    MainViewModel.GetIntance().Metertable = Meter;
                    MainViewModel.GetIntance().Meter = new MeterViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new MenuTabbedPage());
                }
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
        #endregion
    }
}
