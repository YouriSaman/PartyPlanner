using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data
{
    public class Data
    {
        private List<Gebruiker> gebruikers = new List<Gebruiker>();
        private List<Feest> feesten = new List<Feest>();

        public Data()
        {
            gebruikers.Add(new Gebruiker(1, "Youri", "Saman", "mail1@gmail.com", "Henk", "sdkjfak", "sdfajk", "4587LK", "DJka", false));
            gebruikers.Add(new Gebruiker(2, "Keesje", "kees1", "mail2@gmail.com", "Kees", "sdkjfak", "sdfajk","4587LK", "DJka", true));
            feesten.Add(new Feest(10, Feest.BetalingKeuze.Organisatie, true, 8, Feest.ConsumptieKeuze.Bonnen, 2, true, true, "appelsap, smirnoff ice", true, "appels, chips",DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null));
        }

        public List<Gebruiker> GetAllGebruikers()
        {
            return gebruikers;
        }
        
        public List<Feest> GetAllFeesten()
        {
            return feesten;
        }
    }
}
