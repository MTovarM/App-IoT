namespace USTAPG.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class measureTable
    {
        public Servicio Servicio1 { get; set; }
        public Servicio Servicio2 { get; set; }
        public Servicio Servicio3 { get; set; }
    }

    public class Servicio
    {
        public string Date { get; set; }
        public int Status { get; set; }
        public float Value { get; set; }
    }
}
