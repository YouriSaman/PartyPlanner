using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace TestApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public string gebruikersnaam { get; set; }
        public string wachtwoord { get; set; }

        public List<Gebruiker> gebruikers { get; set; }
        
    }
}
