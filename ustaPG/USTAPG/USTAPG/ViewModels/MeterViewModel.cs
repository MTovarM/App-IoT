namespace USTAPG.ViewModels
{
    using Microcharts;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            this.Dia = true;
            this.Hora = false;
            this.Mes = false;
            this.Gra1X = new List<string>();
            this.Gra2X = new List<string>();
            this.Gra3X = new List<string>();
            this.Gra1Y = new List<float>();
            this.Gra2Y = new List<float>();
            this.Gra3Y = new List<float>();
            DateTime Today = DateTime.Today;
            var split = Today.ToString("d").Split('/');
            string[] meses = new string[12] {"Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep",
            "Oct", "Nov", "Dic"};
            AcDia = split[0];
            AcMes = meses[Convert.ToInt32(split[1]) - 1];
            AcAnio = split[2];
            Graficar();
        }
        #endregion

        #region Metodos
        public void Graficar()
        {
            foreach (var M in this.Measure)
            {
                S1.Add(M.Servicio1);
                S2.Add(M.Servicio2);
                S3.Add(M.Servicio3);
            }
            List<ChartEntry> Gra1entry = new List<ChartEntry>();
            for (int i = 0; i < Gra1X.Count; i++)
            {
                Gra1entry.Add(new ChartEntry(Gra1Y[i]) {
                    Label = this.Gra1X[i],
                    ValueLabel = this.Gra1Y[i].ToString(),
                    Color = SKColor.Parse("#F9A825")
                });
            }
            var v = OrganizarDia(1).OrderBy(o => o.Label);
            //this.Gra1 = new BarChart() { Entries = Gra1entry };
            this.Gra1 = new BarChart() { Entries = v };
        }

        public List<ChartEntry> OrganizarDia(int _NumService)
        {
            List<Servicio> _temp = new List<Servicio>();
            if (_NumService == 1) _temp = S1;
            else if (_NumService == 2) _temp = S2;
            else _temp = S3;
            AcMes = "May";
            AcDia = "27";
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
                    _average = _sum / _count;
                    Gra1entry.Add(new ChartEntry(Convert.ToInt32(_average))
                    {
                        Label = _hours.ToString() + ":00",
                        ValueLabel = _average.ToString(),
                        Color = SKColor.Parse("#F9A825")
                    });
                    _diaMes.RemoveAll(a => a.Date.Contains(_hours + ":"));
                    _hours--;
                    _average = 0;
                    _count = 0;
                    _sum = 0;
                }
            }
            return Gra1entry;
        }

        public string ObtenerEstado(int _status)
        {
            if (_status == 0) return "Activo";
            else return "Inactivo";
        }
        #endregion
    }
}
