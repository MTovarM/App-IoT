namespace USTAPG.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Microcharts;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using USTAPG.Models;

    public class MeterViewModel : BaseViewModel
    {
        #region Atributos
        public BarChart gra1;
        public BarChart gra2;
        public BarChart gra3;
        public bool dia;
        public bool hora;
        public bool mes;
        public string estrato;
        public string estadoS1;
        public float tarifaS1;
        public float aproximadoS1;
        public string fechaFacS1;
        public string estadoS2;
        public float tarifaS2;
        public float aproximadoS2;
        public string fechaFacS2;
        public string estadoS3;
        public float tarifaS3;
        public float aproximadoS3;
        public string fechaFacS3;
        #endregion

        #region Propiedades
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
        public BarChart Gra1
        {
            get { return this.gra1; }
            set { SetValue(ref this.gra1, value); }
        }
        public BarChart Gra2
        {
            get { return this.gra2; }
            set { SetValue(ref this.gra2, value); }
        }
        public BarChart Gra3
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
        public string EstadoS1
        {
            get { return this.estadoS1; }
            set { SetValue(ref this.estadoS1, value); }
        }
        public float TarifaS1
        {
            get { return this.tarifaS1; }
            set { SetValue(ref this.tarifaS1, value); }
        }
        public float AproximadoS1
        {
            get { return this.aproximadoS1; }
            set { SetValue(ref this.aproximadoS1, value); }
        }
        public string FechaFacS1
        {
            get { return this.fechaFacS1; }
            set { SetValue(ref this.fechaFacS1, value); }
        }
        public string EstadoS2
        {
            get { return this.estadoS2; }
            set { SetValue(ref this.estadoS2, value); }
        }
        public float TarifaS2
        {
            get { return this.tarifaS2; }
            set { SetValue(ref this.tarifaS2, value); }
        }
        public float AproximadoS2
        {
            get { return this.aproximadoS2; }
            set { SetValue(ref this.aproximadoS2, value); }
        }
        public string FechaFacS2
        {
            get { return this.fechaFacS2; }
            set { SetValue(ref this.fechaFacS2, value); }
        }
        public string EstadoS3
        {
            get { return this.estadoS3; }
            set { SetValue(ref this.estadoS3, value); }
        }
        public float TarifaS3
        {
            get { return this.tarifaS3; }
            set { SetValue(ref this.tarifaS3, value); }
        }
        public float AproximadoS3
        {
            get { return this.aproximadoS3; }
            set { SetValue(ref this.aproximadoS3, value); }
        }
        public string FechaFacS3
        {
            get { return this.fechaFacS3; }
            set { SetValue(ref this.fechaFacS3, value); }
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
            this.Measure = MainViewModel.GetIntance().MeasureTable;
            this.Info = MainViewModel.GetIntance().InfoTable;
            this.Estrato = this.Info[0].StatusClass;
            this.AproximadoS1 = this.Info[0].Service1.AproxMonth;
            this.AproximadoS2 = this.Info[0].Service2.AproxMonth;
            this.AproximadoS3 = this.Info[0].Service3.AproxMonth;
            this.FechaFacS1 = this.Info[0].Service1.BillDate;
            this.FechaFacS2 = this.Info[0].Service2.BillDate;
            this.FechaFacS3 = this.Info[0].Service3.BillDate;
            this.TarifaS1 = this.Info[0].Service1.Fare;
            this.TarifaS2 = this.Info[0].Service2.Fare;
            this.TarifaS3 = this.Info[0].Service3.Fare;
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
            AcDia = split[0];
            AcMes = Meses[Convert.ToInt32(split[1]) - 1];
            AcAnio = split[2];
            foreach (var M in this.Measure)
            {
                S1.Add(M.Servicio1);
                S2.Add(M.Servicio2);
                S3.Add(M.Servicio3);
            }
            this.Hora = true;
            this.Dia = false;
            this.Mes = false;
        }
        #endregion

        #region Metodos
        public List<ChartEntry> OrganizarHora(int _NumService)
        {
            //Nececito los tres: Dia, mes y año
            AcMes = "May";
            AcDia = "27";
            //-----------------------------------

            List<Servicio> _temp = new List<Servicio>();
            if (_NumService == 1) _temp = S1;
            else if (_NumService == 2) _temp = S2;
            else _temp = S3;
            var _diaMes = _temp.Where(m => m.Date.ToLower().Contains(AcDia + "/" + AcMes.ToLower() + "/" + AcAnio)).ToList();
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
                        _average = _sum / _count;
                        Gra1entry.Add(new ChartEntry(Convert.ToInt32(_average))
                        {
                            Label = _hours.ToString() + ":00",
                            ValueLabel = _average.ToString(),
                            Color = SKColor.Parse("#FFFFFF")
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
            AcMes = "May";
            //---------------------------------

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
                    _average = _sum / _count;
                    Gra1entry.Add(new ChartEntry(Convert.ToInt32(_average))
                    {
                        Label = D.Key,
                        ValueLabel = _average.ToString(),
                        Color = SKColor.Parse("#FFFFFF")
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
            //---------------------------------

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
                    _average = _sum / _count;
                    Gra1entry.Add(new ChartEntry(Convert.ToInt32(_average))
                    {
                        Label = M,
                        ValueLabel = _average.ToString(),
                        Color = SKColor.Parse("#FFFFFF")
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
                this.Gra1 = new BarChart() { Entries = GMes1, 
                    LabelColor = SKColor.Parse("#FFFF"), 
                    LabelTextSize = 34,
                    ValueLabelOrientation = Orientation.Horizontal,
                    BackgroundColor = SKColor.Parse("#1D56FF")};
                this.Gra2 = new BarChart() { Entries = GMes2 };
                this.Gra3 = new BarChart() { Entries = GMes3 };
            }
            else if (this.Hora)
            {
                var GHora1 = OrganizarHora(1).OrderBy(o => o.Label).ToList();
                var GHora2 = OrganizarHora(2).OrderBy(o => o.Label).ToList();
                var GHora3 = OrganizarHora(3).OrderBy(o => o.Label).ToList();
                GHora1.Reverse();
                GHora3.Reverse();
                GHora2.Reverse();
                this.Gra1 = new BarChart() { Entries = GHora1,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = 34,
                    ValueLabelOrientation = Orientation.Horizontal,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
                this.Gra2 = new BarChart() { Entries = GHora2 };
                this.Gra3 = new BarChart() { Entries = GHora3 };
            }
            else if(this.Dia)
            {
                var GDia1 = OrganizarDia(1).ToList();
                var GDia2 = OrganizarDia(2).ToList();
                var GDia3 = OrganizarDia(3).ToList();
                this.Gra1 = new BarChart() { Entries = GDia1,
                    LabelColor = SKColor.Parse("#FFFF"),
                    LabelTextSize = 34,
                    ValueLabelOrientation = Orientation.Horizontal,
                    BackgroundColor = SKColor.Parse("#1D56FF")
                };
                this.Gra2 = new BarChart() { Entries = GDia2 };
                this.Gra3 = new BarChart() { Entries = GDia3 };
            }
        }

        public string ObtenerEstado(int _status)
        {
            if (_status == 0) return "Activo";
            else return "Inactivo";
        }
        #endregion
    }
}
