using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace TestApp.ViewModels
{
    public class BerichtViewModel
    {
        public int BerichtId { get; set; }
        public string Titel { get; set; }
        public string Tekst { get; set; }
        public int GebruikerId { get; set; }
        public string Gebruikersnaam { get; set; }
        public List<Bericht> Berichten { get; set; }
        public List<Reactie> Reacties { get; set; }
        public Reactie Reactie { get; set; }

        public BerichtViewModel()
        {
            Reactie = new Reactie();
        }
    }
}
