namespace USTAPG.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Microcharts;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Input;
    using USTAPG.Models;
    using Xamarin.Forms;

    public class MeterViewModel : BaseViewModel
    {
        #region Atributos
        public int height;
        public LineChart gra1;
        public LineChart gra2;
        public LineChart gra3;
        public bool dia;
        public bool hora;
        public bool mes;
        public string mac;
        public string estrato;
        public string estadoS1;
        public string estadoS2;
        public string estadoS3;
        public string fechaFacS1;
        public string fechaFacS2;
        public string fechaFacS3;
        public string tarifaS3;
        public string tarifaS1;
        public string tarifaS2;
        public string aproximadoS1;
        public string aproximadoS2;
        public string aproximadoS3;
        public string servicio1;
        public string servicio2;
        public string servicio3;
        public string direccion;
        #endregion

        #region Propiedades
        public int FontSize { get; set; }
        public int Height
        {
            get { return this.height; }
            set { SetValue(ref this.height, value); }
        }
        public string Servicio1
        {
            get { return this.servicio1; }
            set { SetValue(ref this.servicio1, value); }
        }
        public string Servicio2
        {
            get { return this.servicio2; }
            set { SetValue(ref this.servicio2, value); }
        }
        public string Servicio3
        {
            get { return this.servicio3; }
            set { SetValue(ref this.servicio3, value); }
        }
        public string Direccion
        {
            get { return this.direccion; }
            set { SetValue(ref this.direccion, value); }
        }
        public MeterTable MeterT { get; set; }
        public string[] Meses { get; set; }
        public List<Servicio> S1 { get; set; }
        public List<Servicio> S2 { get; set; }
        public List<Servicio> S3 { get; set; }
        public string AcMes { get; set; }
        public string AcAnio { get; set; }
        public string AcDia { get; set; }
        public List<measureTable> Measure { get; set; }
        public List<InfoTable> Info { get; set; }
        public List<float> Gra1Y { get; set; }
        public List<float> Gra2Y { get; set; }
        public List<float> Gra3Y { get; set; }
        public List<string> Gra1X { get; set; }
        public List<string> Gra2X { get; set; }
        public List<string> Gra3X { get; set; }
        public LineChart Gra1
        {
            get { return this.gra1; }
            set { SetValue(ref this.gra1, value); }
        }
        public LineChart Gra2
        {
            get { return this.gra2; }
            set { SetValue(ref this.gra2, value); }
        }
        public LineChart Gra3
        {
            get { return this.gra3; }
            set { SetValue(ref this.gra3, value); }
        }
        public bool Dia
        {
            get { return this.dia; }
            set { SetValue(ref this.dia, value); }
        }
        public bool Hora
        {
            get { return this.hora; }
            set { SetValue(ref this.hora, value); }
        }
        public bool Mes
        {
            get { return this.mes; }
            set { SetValue(ref this.mes, value); }
        }
        public string Estrato
        {
            get { return this.estrato; }
            set { SetValue(ref this.estrato, value); }
        }
        public string FechaFacS1
        {
            get { return this.fechaFacS1; }
            set { SetValue(ref this.fechaFacS1, value); }
        }
        public string FechaFacS2
        {
            get { return this.fechaFacS2; }
            set { SetValue(ref this.fechaFacS2, value); }
        }
        public string FechaFacS3
        {
            get { return this.fechaFacS3; }
            set { SetValue(ref this.fechaFacS3, value); }
        }
        public string EstadoS1
        {
            get { return this.estadoS1; }
            set { SetValue(ref this.estadoS1, value); }
        }
        public string EstadoS2
        {
            get { return this.estadoS2; }
            set { SetValue(ref this.estadoS2, value); }
        }
        public string EstadoS3
        {
            get { return this.estadoS3; }
            set { SetValue(ref this.estadoS3, value); }
        }
        public string AproximadoS1
        {
            get { return this.aproximadoS1; }
            set { SetValue(ref this.aproximadoS1, value); }
        }
        public string AproximadoS3
        {
            get { return this.aproximadoS3; }
            set { SetValue(ref this.aproximadoS3, value); }
        }
        public string AproximadoS2
        {
            get { return this.aproximadoS2; }
            set { SetValue(ref this.aproximadoS2, value); }
        }
        public string TarifaS1
        {
            get { return this.tarifaS1; }
            set { SetValue(ref this.tarifaS1, value); }
        }
        public string TarifaS2
        {
            get { return this.tarifaS2; }
            set { SetValue(ref this.tarifaS2, value); }
        }
        public string TarifaS3
        {
            get { return this.tarifaS3; }
            set { SetValue(ref this.tarifaS3, value); }
        }
        public string MAC
        {
            get { return this.mac; }
            set { SetValue(ref this.mac, value); }
        }
        #endregion

        #region Comandos
        public ICommand RadioButtonCommand
        {
            get
            {
                return new RelayCommand(RevisarRadioButton);
            }
        }
        #endregion

        #region Constructor
        public MeterViewModel()
        {
            this.MAC = MainViewModel.GetIntance().SMAC;
            this.Servicio1 = ObtenerEstado(MainViewModel.GetIntance().Metertable.Services.Service1);
            this.Servicio2 = ObtenerEstado(MainViewModel.GetIntance().Metertable.Services.Service2);
            this.Servicio3 = ObtenerEstado(MainViewModel.GetIntance().Metertable.Services.Service3);
            this.Direccion = MainViewModel.GetIntance().Metertable.Address;
            this.MeterT = MainViewModel.GetIntance().Metertable;
            this.Measure = MainViewModel.GetIntance().MeasureTable;
            this.Info = MainViewModel.GetIntance().InfoTable;
            this.Estrato = this.Info[0].StatusClass;
            this.AproximadoS1 = this.Info[0].Service1.AproxMonth.ToString();
            this.AproximadoS2 = this.Info[0].Service2.AproxMonth.ToString();
            this.AproximadoS3 = this.Info[0].Service3.AproxMonth.ToString();
            this.FechaFacS1 = this.Info[0].Service1.BillDate;
            this.FechaFacS2 = this.Info[0].Service2.BillDate;
            this.FechaFacS3 = this.Info[0].Service3.BillDate;
            this.TarifaS1 = this.Info[0].Service1.Fare.ToString();
            this.TarifaS2 = this.Info[0].Service2.Fare.ToString();
            this.TarifaS3 = this.Info[0].Service3.Fare.ToString();
            this.EstadoS1 = ObtenerEstado(this.Measure[Measure.Count - 1].Servicio1.Status);
            this.EstadoS2 = ObtenerEstado(this.Measure[Measure.Count - 1].Servicio2.Status);
            this.EstadoS3 = ObtenerEstado(this.Measure[Measure.Count - 1].Servicio3.Status);
            this.S1 = new List<Servicio>();
            this.S2 = new List<Servicio>();
            this.S3 = new List<Servicio>();
            this.Gra1X = new List<string>();
            this.Gra2X = new List<string>();
            this.Gra3X = new List<string>();
            this.Gra1Y = new List<float>();
            this.Gra2Y = new List<float>();
            this.Gra3Y = new List<float>();
            DateTime Today = DateTime.Today;
            var split = Today.ToString("d").Split('/');
            Meses = new string[12] {"Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep",
            "Oct", "Nov", "Dic"};
            try
            {
                this.AcDia = split[1];
                this.AcMes = Meses[Convert.ToInt32(split[0]) - 1];
            }
            catch (Exception)
            {   
                this.Height = 400;
                this.AcDia = split[0];
                this.AcMes = Meses[Convert.ToInt32(split[1]) - 1];
            }
            AcAnio = split[2];
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    this.FontSize = 19;
                    this.Height = 400;
                    break;
                default:
                    this.FontSize = 30;
                    this.Height = 220;
                    break;
            }
            foreach (var M in this.Measure)
            {
                S1.Add(M.Servicio1);
                S2.Add(M.Servicio2);
                S3.Add(M.Servicio3);
            }
            this.Hora = true;
            this.Dia = false;
            this.Mes = false;
            var GHora1 = OrganizarHora(1);
            var GHora2 = OrganizarHora(2);
            var GHora3 = OrganizarHora(3);
            GHora1.Reverse();
            GHora3.Reverse();
            GHora2.Reverse();
            this.Gra1 = new LineChart()
            {
                Entries = GHora1,
                LabelColor = SKColor.Parse("#FFFF"),
                LabelTextSize = this.FontSize,
                BackgroundColor = SKColor.Parse("#1D56FF")
            };
            this.Gra2 = new LineChart()
            {
                Entries = GHora2,
                LabelColor = SKColor.Parse("#FFFF"),
                LabelTextSize = this.FontSize,
                BackgroundColor = SKColor.Parse("#1D56FF")
            };
            this.Gra3 = new LineChart()
            {
                Entries = GHora3,
                LabelColor = SKColor.Parse("#FFFF"),
                LabelTextSize = this.FontSize,
                BackgroundColor = SKColor.Parse("#1D56FF")
            };
        }
        #endregion

        #region Metodos
        public List<ChartEntry> OrganizarHora(int _NumService)
        {
            //Necesito los tres: Dia, mes y año
            List<Servicio> _temp = new List<Servicio>();
            if (_NumService == 1) _temp = S1;
            else if (_NumService == 2) _temp = S2;
            else _temp = S3;
            string AcDiaTemp = "";
            var f = Convert.ToInt32(AcDia) < 10 ? AcDiaTemp += " " + AcDia : AcDiaTemp += AcDia;
            var _diaMes = _temp.Where(m => m.Date.ToLower().Contains(AcDiaTemp + "/" + AcMes.ToLower() + "/" + AcAnio)).ToList();
            List<ChartEntry> Gra1entry = new List<ChartEntry>();
            int _hours = 23;
            int _count = 0;
            float _sum = 0;
            float _average = 0;
            for(int i = _diaMes.Count; i > 0; i--)
            {
                var li = _diaMes.Where(h => h.Date.Contains(_hours + ":")).ToList();
                if (li.Count != 0)
                {
                    foreach (var it in li)
                    {
                        _sum += it.Value;
                        _count++;
                    }
                    try
                    {
                        _average = _sum;
                        string GraLabel = "";
                        if (_NumService == 1) GraLabel = (_average / 1000).ToString("N1") + " kWh";
                        else if (_NumService == 2) GraLabel = Convert.ToInt32(_average) + " L";
                        else GraLabel = Convert.ToInt32(_average) + " L";
                        Gra1entry.Add(new ChartEntry(Convert.ToInt32(_average))
                        {
                            Label = _hours.ToString() + ":00",
                            ValueLabel = GraLabel,
                            Color = SKColor.Parse("#FFFFFF"),
                            ValueLabelColor = SKColor.Parse("#FFFFFF")
                        });
                        _diaMes.RemoveAll(a => a.Date.Contains(_hours + ":"));
                    }
                    catch (Exception)
                    { }
                    _hours--;
                    _sum = 0;
                    _count = 0;
                    _average = 0;
                }
            }
            return Gra1entry;
        }

        public List<ChartEntry> OrganizarDia(int _NumService)
        {
            //Necesito dos: mes y año
            List<Servicio> _temp = new List<Servicio>();
            if (_NumService == 1) _temp = S1;
            else if (_NumService == 2) _temp = S2;
            else _temp = S3;
            List<ChartEntry> Gra1entry = new List<ChartEntry>();
            var Mes = _temp.Where(m => m.Date.ToLower().Contains(AcMes.ToLower() + "/" + AcAnio)).ToList();
            var Dias = Mes.GroupBy(g => {
                int In = g.Date.IndexOf(" ");
                return g.Date.Substring(In);
            }).ToList();
            int _count = 0;
            float _sum = 0;
            float _average = 0;
            foreach (var D in Dias)
            {
                foreach (var H in D)
                {
                    _sum += H.Value;
                    _count++;
                }
                try
                {
                    _average = _sum;
                    string GraLabel = "";
                    if (_NumService == 1) GraLabel = (_average / 1000).ToString("N1") + " kWh";
                    else if (_NumService == 2) GraLabel = Convert.ToInt32(_average) + " L";
                    else GraLabel = Convert.ToInt32(_average) + " L";
                    Gra1entry.Add(new ChartEntry(Convert.ToInt32(_average))
                    {
                        Label = D.Key.Remove(D.Key.Length - 5, 5),
                        ValueLabel = GraLabel,
                        Color = SKColor.Parse("#FFFFFF"),
                        ValueLabelColor = SKColor.Parse("#FFFFFF")
                    });
                }
                catch (Exception)
                { }
                _sum = 0;
                _count = 0;
                _average = 0;
            }
            return Gra1entry;
        }

        public List<ChartEntry> OrganizarMes(int _NumService)
        {
            //Necesito solo el año
            List<Servicio> _temp = new List<Servicio>();
            if (_NumService == 1) _temp = S1;
            else if (_NumService == 2) _temp = S2;
            else _temp = S3;
            var _meses = _temp.Where(m => m.Date.ToLower().Contains(AcAnio)).ToList();
            List<ChartEntry> Gra1entry = new List<ChartEntry>();
            int _count = 0;
            float _sum = 0;
            float _average = 0;
            foreach (var M in Meses)
            {
                var Mes = _meses.Where(h => h.Date.Contains(M)).ToList();
                foreach (var MM in Mes)
                {
                    _sum += MM.Value;
                    _count++;
                }
                try
                {
                    _average = _sum;
                    string GraLabel = "";
                    if (_NumService == 1) GraLabel = (_average / 1000).ToString("N1") + " kWh";
                    else if (_NumService == 2) GraLabel = Convert.ToInt32(_average) + " L";
                    else GraLabel = Convert.ToInt32(_average) + " L";
                    Gra1entry.Add(new ChartEntry(Convert.ToInt32(_average))
                    {
                        Label = M,
                        ValueLabel = GraLabel,
                        Color = SKColor.Parse("#FFFFFF"),
                        ValueLabelColor = SKColor.Parse("#FFFFFF")
                    });
                }
                catch (Exception)
                { }
                _sum = 0;
                _count = 0;
                _average = 0;
            }
            return Gra1entry;
        }

        private void RevisarRadioButton()
        {
            if (this.Mes)
            {
                var GMes1 = OrganizarMes(1).ToList();
                var GMes2 = OrganizarMes(2).ToList();
                var GMes3 = OrganizarMes(3).ToList();
                this.Gra1 = new LineChart() { Entries = GMes1, 
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")};
                this.Gra2 = new LineChart() {
                    Entries = GMes2,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
                this.Gra3 = new LineChart() {
                    Entries = GMes3,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
            }
            else if (this.Hora)
            {
                //var GHora1 = OrganizarHora(1).OrderBy(o => o.Label).ToList();
                //var GHora2 = OrganizarHora(2).OrderBy(o => o.Label).ToList();
                //var GHora3 = OrganizarHora(3).OrderBy(o => o.Label).ToList();
                var GHora1 = OrganizarHora(1);
                var GHora2 = OrganizarHora(2);
                var GHora3 = OrganizarHora(3);
                GHora1.Reverse();
                GHora3.Reverse();
                GHora2.Reverse();
                this.Gra1 = new LineChart() { Entries = GHora1,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
                this.Gra2 = new LineChart() { Entries = GHora2,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
                this.Gra3 = new LineChart() { Entries = GHora3,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
            }
            else if(this.Dia)
            {
                var GDia1 = OrganizarDia(1).ToList();
                var GDia2 = OrganizarDia(2).ToList();
                var GDia3 = OrganizarDia(3).ToList();
                this.Gra1 = new LineChart() { Entries = GDia1,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
                this.Gra2 = new LineChart() { Entries = GDia2,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
                this.Gra3 = new LineChart() { Entries = GDia3,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = this.FontSize,
                    ValueLabelOrientation = Orientation.Vertical,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
            }
        }

        public string ObtenerEstado(int _status)
        {
            if (_status == 1) return "Activo";
            else if (_status == 0) return "Inactivo";
            else if (_status == 2) return "Activo (P)";
            else if (_status == 3) return "Activo (P)";
            else if (_status == 4) return "Activo (P)";
            else if (_status == 5) return "Activo (P)";
            else if (_status == 6) return "Activo (P)";
            else if (_status == 7) return "Activo (P)";
            else return "Activo (P)";
        }
        #endregion
    }
}
