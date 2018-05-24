using System;
using Data;
using Models;
using System.Collections.Generic;

namespace Logic
{
    public class Logic
    {
        Data.Data data = new Data.Data();
        public string gebruikersnaam { get; set; }
        public string wachtwoord { get; set; }
        public string nieuweGebruikersnaam { get; set; }
        public string nieuwWachtwoord { get; set; }

        public List<Gebruiker> GetAllGebruikers()
        {
            return data.GetAllGebruikers();
        }

        public List<Feest> GetAllFeesten()
        {
            return data.GetAllFeesten();
        }

        public bool LoginCheck()
        {
            if (gebruikersnaam == "Admin" && wachtwoord == "admin1")
            {
                return true;
            }

            return false;
        }

        public bool AccountCheck()
        {
            foreach (var gebruiker in GetAllGebruikers())
            {
                if (gebruiker.Gebruikersnaam != nieuweGebruikersnaam)
                {
                    return true;    
                }

            }

            return false;
        }

    }
}
