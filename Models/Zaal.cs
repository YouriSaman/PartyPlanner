using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Zaal
    {
        public int ZaalId { get; set; }
        public string Naam { get; set; }
        public int Capaciteit { get; set; }
        public string Plaats { get; set; }
        public decimal Prijs { get; set; }
    }
}
