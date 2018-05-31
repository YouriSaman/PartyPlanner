using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Bericht : Tekst
    {
        //public string AfbeeldingUrl
        public int BerichtId { get; set; }
        public string BerichtTitel { get; set; }
        public List<Reactie> Reacties { get; set; }
    }
}
