using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class FeestLogic
    {
        public int FeestId { get; set; }
        public int AantalPersonen { get; set; }
        public enum BetalingKeuze { Organisatie, Genodigden }

        private Feest.BetalingKeuze _betaling;
        public Feest.BetalingKeuze Betaling { get => _betaling; set => _betaling = value; }
        public bool Entree { get; set; }
        public decimal EntreePrijs { get; set; }
        public enum ConsumptieKeuze { Bonnen, Geld, Allin }

        private Feest.ConsumptieKeuze _consumptie;
        public Feest.ConsumptieKeuze Consumptie { get => _consumptie; set => _consumptie = value; }
        public decimal ConsumptieBonPrijs { get; set; }
        public bool Versierd { get; set; }
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

        private FeestContext _feestContext;
        public FeestLogic()
        {
            _feestContext = new FeestContext();
        }

        public void MaakFeest(Feest feest)
        {
           FeestId = _feestContext.Add(feest);
        }

        public List<Feest> GetAllFeesten()
        {
            return _feestContext.GetAllFeesten();
        }

        public bool DatumCheck()
        {
            foreach (var feest in GetAllFeesten())
            {
                if (BeginDatum < EindDatum)
                {
                    if ((EindDatum < feest.BeginDatum && EindDatum < feest.EindDatum) || (BeginDatum > feest.BeginDatum && BeginDatum > feest.EindDatum))
                    {
                        return true;
                    }

                    return false;
                }

                break;
            }
            return false;
        }
    }
}
