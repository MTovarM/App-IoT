namespace USTAPG.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using USTAPG.Models;

    public class MainViewModel
    {
        #region Propiedades
        public List<measureTable> MeasureTable { get; set; }
        public List<InfoTable> InfoTable { get; set; }
        public MeterTable Metertable { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public MacViewModel MacMainPage { get; set; }
        public MeterViewModel Meter { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        public static MainViewModel Instance;

        public static MainViewModel GetIntance() 
        {
            if (Instance == null) return new MainViewModel();
            return Instance;
        }
        #endregion
    }
}
