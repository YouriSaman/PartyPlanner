using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Tekst
    {
        public int GebruikerId { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Tekstbericht { get; set; }
        public int Likes { get; set; }
    }
}
