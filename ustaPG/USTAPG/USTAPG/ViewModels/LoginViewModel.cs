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
            set { SetValue(ref this._habilitado, value); }
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
            //GenerarData();
            //Encriptacion Codificacion = new Encriptacion();
            //Codificacion.Encriptar("a4:cf:12:d9:3f:b7");
            ValidarUsuario();
        }
        #endregion

        public void GenerarData()
        {
            //string[] Meses = new string[12] {"Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep",
            //"Oct", "Nov", "Dic"};
            string t = "{";
            string[] Meses = new string[6] { "Ene", "Feb", "Mar", "Abr", "May","Jun" };
            string _InfoTable = "\"InfoTable\": {" +
                    "\"a4:cf:12:d9:3e:88\": {" +
                        "\"Val\": {" +
                          "\"Service1\": {" +
                            "\"AproxMonth\": 500.8," +
                            "\"BillDate\": \"08/03/2022\"," +
                            "\"Fare\": 33.45" +
                          "}," +
                          "\"Service2\": {" +
                            "\"AproxMonth\": 4566.4," +
                            "\"BillDate\": \"08/03/2022\"," +
                            "\"Fare\": 44" +
                          "}," +
                          "\"Service3\": {" +
                            "\"AproxMonth\": 133," +
                            "\"BillDate\": \"08/03/2022\"," +
                            "\"Fare\": 339.99" +
                          "}," +
                          "\"StatusClass\": 6" +
                        "}" +
                    "}," +
                    "\"a4:cf:12:d9:3e:1b\": {" +
                          "\"Val\": {" +
                            "\"Service1\": {" +
                              "\"AproxMonth\": 500.8," +
                              "\"BillDate\": \"08/03/2022\"," +
                              "\"Fare\": 33.45" +
                            "}," +
                            "\"Service2\": {" +
                              "\"AproxMonth\": 4566.4," +
                              "\"BillDate\": \"08/03/2022\"," +
                              "\"Fare\": 44" +
                            "}," +
                            "\"Service3\": {" +
                              "\"AproxMonth\": 133," +
                              "\"BillDate\": \"08/03/2022\"," +
                              "\"Fare\": 339.99" +
                            "}," +
                            "\"StatusClass\": 6" +
                          "}" +
                        "}," +
                    "\"a4:cf:12:d9:3f:b7\": {" +
                          "\"Val\": {" +
                            "\"Service1\": {" +
                              "\"AproxMonth\": 500.8," +
                              "\"BillDate\": \"08/03/2022\"," +
                              "\"Fare\": 33.45" +
                            "}," +
                            "\"Service2\": {" +
                              "\"AproxMonth\": 4566.4," +
                              "\"BillDate\": \"08/03/2022\"," +
                              "\"Fare\": 44" +
                            "}," +
                            "\"Service3\": {" +
                              "\"AproxMonth\": 133," +
                              "\"BillDate\": \"08/03/2022\"," +
                              "\"Fare\": 339.99" +
                            "}," +
                            "\"StatusClass\": 6" +
                          "}" +
                        "}" +
                      "}";
            t += _InfoTable + ",";
            string _MeterTable = "\"meterTable\": {" +
                    "\"a4:cf:12:d9:3e:88\": {" +
                      "\"Address\": \"Calle 33 sur 87c 29 apto 103 torre 16\"," +
                      "\"Gateway\": \"E8:DB:84:9D:2D:3E\"," +
                      "\"Services\": {" +
                        "\"Service1\": 1," +
                        "\"Service2\": 0," +
                        "\"Service3\": 2" +
                      "}," +
                      "\"Status\": \"1\"" +
                    "}," +
                    "\"a4:cf:12:d9:3f:b7\": {" +
                      "\"Address\": \"Calle 33 sur 87c 29 apto 103 torre 15\"," +
                      "\"Gateway\": \"E8:DB:84:9D:2D:3E\"," +
                      "\"Services\": {" +
                        "\"Service1\": 1," +
                        "\"Service2\": 1," +
                        "\"Service3\": 1" +
                      "}," +
                      "\"Status\": \"1\"" +
                    "}," +
                    "\"a4:cf:12:d9:3e:1b\": {" +
                      "\"Address\": \"Calle 33 sur 87c 29 apto 103 torre 15\"," +
                      "\"Gateway\": \"E8:DB:84:9D:2D:3E\"," +
                      "\"Services\": {" +
                        "\"Service1\": 1," +
                        "\"Service2\": 1," +
                        "\"Service3\": 1" +
                      "}," +
                      "\"Status\": \"1\"" +
                    "}" +
                "}";
            t += _MeterTable + ",";
            string _StatusTable = "\"statusTable\": {" +
              "\"a4:cf:12:d9:3e:1b\": {" +
                "\"Services\": {" +
                  "\"Service1\": 0," +
                  "\"Service2\": 0," +
                  "\"Service3\": 0" +
                "}," +
                "\"Status\": \"1bL\"" +
              "}," +
              "\"a4:cf:12:d9:3e:88\": {" +
                "\"Services\": {" +
                  "\"Service1\": 1," +
                  "\"Service2\": 0," +
                  "\"Service3\": 0" +
                "}," +
                "\"Status\": \"servicio\"" +
              "}," +
              "\"a4:cf:12:d9:3f:b7\": {" +
                "\"Services\": {" +
                  "\"Service1\": 1," +
                  "\"Service2\": 1," +
                  "\"Service3\": 1" +
                "}," +
                "\"Status\": \"servicio\"" +
              "}" +
            "}";
            t += _StatusTable + ",";
            t += "\"measureTable\": {" +
                 "\"a4:cf:12:d9:3e:88\": ";

            string medidass = "{";
            foreach (var mes in Meses)
            {
                //Dia
                for (int i = 1; i <= 30; i++)
                {
                    var seed1 = Environment.TickCount;
                    var seed2 = Environment.TickCount;
                    var seed3 = Environment.TickCount;
                    Random s1 = new Random(seed1);
                    Random s2 = new Random(seed2);
                    Random s3 = new Random(seed3);

                    //Hora
                    for (int ii = 0; ii <= 23; ii++)
                    {
                        //minutos
                        for (int iii = 15; iii <= 45; iii += 15)
                        {
                            double S1_Measu = 0.0;
                            double S2_Measu = 0.0;
                            double S3_Measu = 0.0;
                            //Electricidad##############
                            // 6 horas 100
                            //12 horas 280
                            // 6 horas 400
                            //164 kWh Total Mensual
                            //6.6 KWh promedio mensual-> 2.2 cada 15 minutos 
                            if (ii <= 5)//de 00:00 a 5:00
                            {
                                double s1dvalue = s1.Next(65, 100);
                                S1_Measu = s1dvalue/3;
                            }
                            else if (ii > 5 && ii < 19) //de 6:00 a 18:00
                            {
                                double s1value = s1.Next(200, 280);
                                S1_Measu = s1value/3;
                            }
                            else //de 19:00 a 23:59
                            {
                                double s1value = s1.Next(350, 400);
                                S1_Measu = s1value/3;
                            }
                            //GAS  - 30 m3 mensuales -> 30000 L
                            //1 m3 diario -> 0.042 m3 hora (0.014) 14000 cm3
                            //AGUA - 18 m3 (18.000 L) mensuales - 0.6 m3 (600 L) diarios 
                            if (ii <= 5 || ii >= 21)//de 00:00 a 5:59 y 21:00 a 23:59
                            {
                                double s2dvalue = s2.Next(0, 3);
                                S2_Measu = s2dvalue;
                                int s3value = s3.Next(1, 4);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            else if (ii > 5 && ii < 12)//de 06:00 a 11:59
                            {
                                double s2dvalue = s2.Next(12, 17);
                                S2_Measu = s2dvalue;
                                int s3value = s3.Next(13, 17);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            else //de 12:00 a 20:59
                            {
                                double s2value = s2.Next(7, 12);
                                S2_Measu = s2value;
                                int s3value = s3.Next(8, 13);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            medidass += "\""+ mes +"mEd1D4" + ii + iii + i + "mEd1D4\": {" +
                                    "\"Servicio1\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S1_Measu +
                                    "}," +
                                    "\"Servicio2\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S2_Measu +
                                    "}," +
                                    "\"Servicio3\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S3_Measu +
                                    "}";
                            //if (iii == 45) medidass += "}";
                            medidass += "},";
                            //else medidass += "},";

                        }
                    }
                }
                //medidass += "}";
            }
            medidass.Remove(medidass.Length - 1);
            t += medidass + "},";

            t += "\"a4:cf:12:d9:3f:b7\":";
            string medidass2 = "{";
            foreach (var mes in Meses)
            {
                //Dia
                for (int i = 1; i <= 30; i++)
                {
                    var seed1 = Environment.TickCount;
                    var seed2 = Environment.TickCount;
                    var seed3 = Environment.TickCount;
                    Random s1 = new Random(seed1);
                    Random s2 = new Random(seed2);
                    Random s3 = new Random(seed3);
                    //Hora
                    for (int ii = 0; ii <= 23; ii++)
                    {
                        //minutos
                        for (int iii = 15; iii <= 45; iii += 15)
                        {
                            double S1_Measu = 0.0;
                            double S2_Measu = 0.0;
                            double S3_Measu = 0.0;
                            //Electricidad##############
                            //6.6 KWh promedio mensual-> 2.2 cada 15 minutos
                            if (ii <= 5)//de 00:00 a 5:00
                            {
                                double s1dvalue = s1.Next(65, 100);
                                S1_Measu = s1dvalue / 3;
                            }
                            else if (ii > 5 && ii < 19) //de 6:00 a 18:00
                            {
                                double s1value = s1.Next(200, 280);
                                S1_Measu = s1value / 3;
                            }
                            else //de 19:00 a 23:59
                            {
                                double s1value = s1.Next(350, 400);
                                S1_Measu = s1value / 3;
                            }
                            //GAS  - 30 m3 mensuales -> 1 m3 diario -> 0.042 m3 hora (0.014)
                            //AGUA - 18 m3 (18.000 L) mensuales - 0.6 m3 (600 L) diarios 
                            if (ii <= 5 || ii >= 21)//de 00:00 a 5:59 y 21:00 a 23:59
                            {
                                double s2dvalue = s2.Next(0, 3);
                                S2_Measu = s2dvalue;
                                int s3value = s3.Next(1, 4);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            else if (ii > 5 && ii < 12)//de 06:00 a 11:59
                            {
                                double s2dvalue = s2.Next(12, 17);
                                S2_Measu = s2dvalue;
                                int s3value = s3.Next(13, 17);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            else //de 12:00 a 20:59
                            {
                                double s2value = s2.Next(7, 12);
                                S2_Measu = s2value;
                                int s3value = s3.Next(8, 13);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            medidass2 += "\"" + mes + "mEd1D4" + ii + iii + i + "mEd1D4\": {" +
                                    "\"Servicio1\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S1_Measu +
                                    "}," +
                                    "\"Servicio2\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S2_Measu +
                                    "}," +
                                    "\"Servicio3\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S3_Measu +
                                    "}";
                            //if (iii == 45) medidass += "}";
                            medidass2 += "},";
                            //else medidass += "},";

                        }
                    }
                }
                //medidass += "}";
            }
            medidass2.Remove(medidass2.Length - 1); 
            t += medidass2 + "},";

            t += "\"a4:cf:12:d9:3e:1b\":";
            string medidass3 = "{";
            foreach (var mes in Meses)
            {
                var seed1 = Environment.TickCount;
                var seed2 = Environment.TickCount;
                var seed3 = Environment.TickCount;
                Random s1 = new Random(seed1);
                Random s2 = new Random(seed2);
                Random s3 = new Random(seed3);
                //Dia
                for (int i = 1; i <= 30; i++)
                {
                    //Hora
                    for (int ii = 0; ii <= 23; ii++)
                    {
                        //minutos
                        for (int iii = 15; iii <= 45; iii += 15)
                        {
                            double S1_Measu = 0.0;
                            double S2_Measu = 0.0;
                            double S3_Measu = 0.0;
                            //Electricidad##############
                            //6.6 KWh promedio mensual-> 2.2 cada 15 minutos
                            if (ii <= 5)//de 00:00 a 5:00
                            {
                                double s1dvalue = s1.Next(65, 100);
                                S1_Measu = s1dvalue / 3;
                            }
                            else if (ii > 5 && ii < 19) //de 6:00 a 18:00
                            {
                                double s1value = s1.Next(200, 280);
                                S1_Measu = s1value / 3;
                            }
                            else //de 19:00 a 23:59
                            {
                                double s1value = s1.Next(350, 400);
                                S1_Measu = s1value / 3;
                            }
                            //GAS  - 30 m3 mensuales -> 1 m3 diario -> 0.042 m3 hora (0.014)
                            //AGUA - 18 m3 (18.000 L) mensuales - 0.6 m3 (600 L) diarios 
                            if (ii <= 5 || ii >= 21)//de 00:00 a 5:59 y 21:00 a 23:59
                            {
                                double s2dvalue = s2.Next(0, 3);
                                S2_Measu = s2dvalue;
                                int s3value = s3.Next(1, 4);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            else if (ii > 5 && ii < 12)//de 06:00 a 11:59
                            {
                                double s2dvalue = s2.Next(12, 17);
                                S2_Measu = s2dvalue;
                                int s3value = s3.Next(13, 17);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            else //de 12:00 a 20:59
                            {
                                double s2value = s2.Next(7, 12);
                                S2_Measu = s2value;
                                int s3value = s3.Next(8, 13);
                                S3_Measu = Convert.ToDouble(s3value*2);
                            }
                            medidass3 += "\"" + mes + "mEd1D4" + ii + iii + i + "mEd1D4\": {" +
                                    "\"Servicio1\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S1_Measu +
                                    "}," +
                                    "\"Servicio2\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S2_Measu +
                                    "}," +
                                    "\"Servicio3\": {" +
                                       "\"Date\": \"" + ii + ":" + iii + " " + i + "/" + mes + "/2022\"," +
                                       "\"Status\": 1," +
                                       "\"Value\":" + S3_Measu +
                                    "}";
                            //if (iii == 45) medidass += "}";
                            medidass3 += "},";
                            //else medidass += "},";

                        }
                    }
                }
                //medidass += "}";
            }
            medidass3.Remove(medidass3.Length - 1);
            t += medidass3 + "}";

            t += "}}";
            //var ya = t;
        var ya = t;
    }

    #region Métodos
    public async void ValidarUsuario()
    {
        MainViewModel.GetIntance().DB = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Ultima.db3"));
        var u = await GetUser();
        MainViewModel.GetIntance().U_Usuario = new List<Usuario>();
        if (u.Count > 0)
        {
            MainViewModel.GetIntance().YaExiste = true;
            MainViewModel.GetIntance().U_Usuario.Add(u[0]);
            this.Correo = MainViewModel.GetIntance().U_Usuario[0].User;
            this.Clave = MainViewModel.GetIntance().U_Usuario[0].Clave;
        }
        else
        {
            MainViewModel.GetIntance().YaExiste = false;
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

        Firebase = new SFirebase() { Email = this.Correo, Clave = this.Clave };
        var a = await Firebase.LoginWithEmail(false);
        //var a = await Firebase.GetMeasure("a4:cf:12:d9:3f:b7");
        if (string.IsNullOrEmpty(a))
        {
            await Application.Current.MainPage.DisplayAlert(
            "Error",
            "Datos incorrectos, por favor ingrese el correo y la contraseña correspondiente.",
            "Aceptar");
            this.Correo = string.Empty;
            this.Clave = string.Empty;
            this.Habilitado = true;
            this.Iniciado = false;
            return;
        }

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
        this.Habilitado = true;
        this.Iniciado = false;
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
