using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PersonenCapaciteit
    {
        public int AantalPersonen { get; set; }
        public int Capaciteit { get; set; }

        public PersonenCapaciteit(int aantalPersonen, int capaciteit)
        {
            AantalPersonen = aantalPersonen;
            Capaciteit = capaciteit;
        }

        public PersonenCapaciteit() {}
    }
}
