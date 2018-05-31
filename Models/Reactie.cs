using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Reactie : Tekst
    {
        public int ReactieId { get; set; }
        public int BerichtId { get; set; }
    }
}
