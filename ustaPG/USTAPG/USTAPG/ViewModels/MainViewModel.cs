namespace USTAPG.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class MainViewModel
    {
        #region Propiedades
        public LoginViewModel Login { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            this.Login = new LoginViewModel();
        }
        #endregion
    }
}
