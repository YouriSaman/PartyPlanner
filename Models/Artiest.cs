using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Artiest
    {
        public string Naam { get; set; }
        public double Prijs { get; set; }
        public bool Beschikbaar { get; set; }

        public Artiest(string naam, double prijs, bool beschikbaar)
        {
            Naam = naam;
            Prijs = prijs;
            Beschikbaar = beschikbaar;
        }
    }
}
