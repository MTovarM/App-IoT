namespace USTAPG.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    class LoginViewModel : INotifyPropertyChanged
    {
        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Atributos
        private string _clave;
        private bool _iniciado;
        private bool _habilitado;
        #endregion

        #region Propiedades
        public bool Habilitado 
        {
            get { return this._habilitado; }
            set
            {
                if (this._habilitado != value)
                {
                    this._habilitado = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(this.Habilitado)));
                }
            }
        }
        public string Correo { get; set; }
        public string Clave 
        { 
            get{return this._clave;}
            set
            {
                if (this._clave != value)
                {
                    this._clave = value;
                    PropertyChanged?.Invoke(this, 
                        new PropertyChangedEventArgs(nameof(this.Clave)));
                }
            }
        }
        public bool Recordar { get; set; }
        public bool Iniciado 
        {
            get { return this._iniciado; }
            set
            {
                if (this._iniciado != value)
                {
                    this._iniciado = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(this.Iniciado)));
                }
            }
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
            this.Habilitado = true;
            this.Recordar = true;
        }
        #endregion

        #region Métodos
        private async void Entrar()
        {
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
            this.Iniciado = true;
            this.Habilitado = false;
        }
        #endregion
    }
}
