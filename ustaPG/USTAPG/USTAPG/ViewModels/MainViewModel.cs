namespace USTAPG.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using USTAPG.Models;
    using USTAPG.Services;

    public class MainViewModel
    {
        #region 
        public List<Usuario> U_Usuario { get; set; }
        public DataBase DB { get; set; }
        public Usuario UserDB { get; set; }
        public List<measureTable> MeasureTable { get; set; }
        public List<InfoTable> InfoTable { get; set; }
        public MeterTable Metertable { get; set; }
        public string SMAC { get; set; }
        public string SUsuario { get; set; }
        public string SClave { get; set; }
        #endregion

        #region ViewModels
        public bool YaExiste { get; set; }
        public LoginViewModel Login { get; set; }
        public MacViewModel MacMainPage { get; set; }
        public MeterViewModel Meter { get; set; }
        public MACInTextViewModel MacTextPage { get; set; }
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
