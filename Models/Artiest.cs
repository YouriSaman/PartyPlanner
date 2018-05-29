using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Artiest
    {
        public int ArtiestId { get; set; }
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public bool Beschikbaar { get; set; }

        public Artiest() { }

        public Artiest(int id, string naam, decimal prijs, bool beschikbaar)
        {
            ArtiestId = id;
            Naam = naam;
            Prijs = prijs;
            Beschikbaar = beschikbaar;
        }
    }
}
