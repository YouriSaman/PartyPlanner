using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace TestApp.ViewModels
{
    public class FeestViewModel
    {
        public int FeestId { get; set; }
        public int ArtiestId { get; set; }
        public int AantalPersonen { get; set; }
        public enum BetalingKeuze { Organisatie, Genodigden }

        private BetalingKeuze _betaling;
        public BetalingKeuze Betaling { get => _betaling; set => _betaling = value; }
        public bool Entree { get; set; }
        public int EntreeIndic { get; set; }
        public decimal EntreePrijs { get; set; }
        public enum ConsumptieKeuze { Bonnen, Geld, Allin }

        private ConsumptieKeuze _consumptie;
        public ConsumptieKeuze Consumptie { get => _consumptie; set => _consumptie = value; }
        public decimal ConsumptieBonPrijs { get; set; }
        public bool Versierd { get; set; }
        public int VersierdIndic { get; set; }
        public bool Drank { get; set; }
        public string DrankWensen { get; set; }
        public bool Eten { get; set; }
        public string EtenWensen { get; set; }
        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public string FeestTitel { get; set; }
        public List<Artiest> Artiesten { get; set; }
        public Feest Feest = new Feest();
        public List<Feest> Feesten = new List<Feest>();
        public int ZaalId { get; set; }
        public string ZaalNaam { get; set; }
        public string ArtiestNaam { get; set; }
        public PersonenCapaciteit AantalPerCapaciteit { get; set; }
        public List<Zaal> Zalen { get; set; }
        public MuziekKeuze Muziek { get => _muziekKeuze; set => _muziekKeuze = value; }

        public enum MuziekKeuze { Niks, Zaal, Artiest }

        private MuziekKeuze _muziekKeuze;

        public FeestViewModel()
        {
            Artiesten = new List<Artiest>();
            Zalen = new List<Zaal>();
        }
    }
}
