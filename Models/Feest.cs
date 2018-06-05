using System;
using System.Collections.Generic;

namespace Models
{
    public class Feest
    {
        public int FeestId { get; set; }
        public int AantalPersonen { get; set; }
        public enum BetalingKeuze { Organisatie, Genodigden }

        private BetalingKeuze _betaling;
        public BetalingKeuze Betaling { get => _betaling; set => _betaling = value; }
        public bool Entree { get; set; }
        public int EntreeIndic { get; set; }
        public decimal EntreePrijs { get; set; }
        public enum ConsumptieKeuze { Allin, Geld, Bonnen }

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
        public DateTime BeginTijd { get; set; }
        public DateTime EindDatum { get; set; }
        public DateTime EindTijd { get; set; }
        public string FeestTitel { get; set; }
        public List<Artiest> Artiesten { get; set; }
        public int ArtiestId { get; set; }
        public int GebruikerId { get; set; }
        public int ZaalId { get; set; }
        public MuziekKeuze Muziek { get => _muziekKeuze; set => _muziekKeuze = value; }

        public enum MuziekKeuze { Niks, Zaal, Artiest}

        private MuziekKeuze _muziekKeuze;


        public Feest() { }

        public Feest(
            int aantalPersonen,
            BetalingKeuze betalingKeuze,
            bool entree,
            decimal entreePrijs,
            ConsumptieKeuze consumptie,
            decimal consumptieBonPrijs,
            bool versierd,
            bool drank,
            string drankWensen,
            bool eten,
            string etenWensen,
            DateTime beginDatum,
            DateTime beginTijd,
            DateTime eindDatum,
            DateTime eindTijd,
            List<Artiest> artiesten)
        {
            AantalPersonen = aantalPersonen;
            _betaling = betalingKeuze;
            Entree = entree;
            EntreePrijs = entreePrijs;
            Consumptie = consumptie; 
            ConsumptieBonPrijs = consumptieBonPrijs;
            Versierd = versierd;
            Drank = drank;
            DrankWensen = drankWensen;
            Eten = eten;
            EtenWensen = etenWensen;
            BeginDatum = beginDatum;
            BeginTijd = beginTijd;
            EindDatum = eindDatum;
            EindTijd = eindTijd;
            Artiesten = artiesten;
        }
    }
}
