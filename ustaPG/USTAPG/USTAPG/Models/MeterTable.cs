using System;
using System.Collections.Generic;
using System.Text;

namespace USTAPG.Models
{
    public class MeterTable
    {
        public string Address { get; set; }
        public string Gateway { get; set; }
        public string Status { get; set; }
        public ServiciosMeter Services { get; set; }
    }

    public class ServiciosMeter
    {
        public int Service1 { get; set; }
        public int Service2 { get; set; }
        public int Service3 { get; set; }
    }
}
