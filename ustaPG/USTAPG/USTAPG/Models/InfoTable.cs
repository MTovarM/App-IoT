namespace USTAPG.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InfoTable
    {
        #region Propiedades
        public string StatusClass { get; set; }
        public Services Service1 { get; set; }
        public Services Service2 { get; set; }
        public Services Service3 { get; set; }
        #endregion
    }

    public class Services
    {
        #region Propiedades
        public float AproxMonth { get; set; }
        public string BillDate { get; set; }
        public float Fare { get; set; }
        #endregion
    }
}
