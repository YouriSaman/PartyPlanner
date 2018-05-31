using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace TestApp.ViewModels
{
    public class ForumViewModel
    {
        public string Titel { get; set; }
        public string Tekst { get; set; }
        public int GebruikerId { get; set; }
        public string Gebruikersnaam { get; set; }
        public List<Bericht> Berichten { get; set; }
    }
}
