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
        private ArtiestContext _artiestContext;
        private ZaalContext _zaalContext;
        public FeestLogic()
        {
            _feestContext = new FeestContext();
            _artiestContext = new ArtiestContext();
            _zaalContext = new ZaalContext();
        }

        public void MaakFeest(Feest feest, int gebruikerId)
        {
            if (feest.Drank == false)
            {
                feest.DrankWensen = "";
            }
            if (feest.Eten == false)
            {
                feest.EtenWensen = "";
            }

            if (feest.Entree == false)
            {
                feest.EntreePrijs = 0;
            }

            if (feest.Consumptie == Feest.ConsumptieKeuze.Allin)
            {
                feest.ConsumptieBonPrijs = 0;
            }
            FeestId = _feestContext.Add(feest, gebruikerId);
        }

        public List<Zaal> GetAllZalen()
        {
            return _zaalContext.GetAllZalen();
        }

        public bool AddDatumLocaFeest(DateTime beginDatum, DateTime eindDatum,int zaalId , int feestId)
        {
            if (DatumCheck(beginDatum, eindDatum) == true)
            {
                return _feestContext.AddDatumLoca(beginDatum, eindDatum, zaalId, feestId);
            };

            return false;
        }

        public bool AddArtiestFeest(int feestId, int artiestId, Feest.MuziekKeuze keuze)
        {
            return _feestContext.AddArtiest(feestId, artiestId, keuze);
        }

        public List<Feest> GetAllFeesten()
        {
            return _feestContext.GetAllFeesten();
        }

        public Feest GetFeestMetId(int feestId)
        {
            return _feestContext.FeestMetId(feestId);
        }

        public List<Artiest> GetAllArtiesten()
        {
            return _artiestContext.GetArtiesten();
        }

        private bool DatumCheck(DateTime beginDatum, DateTime eindDatum)
        {
            var goedeDatum = false;
            foreach (var feest in GetAllFeesten())
            {
                if (beginDatum < eindDatum)
                {
                    if ((eindDatum < feest.BeginDatum && eindDatum < feest.EindDatum) || (beginDatum > feest.BeginDatum && beginDatum > feest.EindDatum))
                    {
                        goedeDatum = true;
                    }
                    else
                    {
                        goedeDatum = false;
                        break;
                    }
                }
            }

            if (goedeDatum == true)
            {
                return true;
            }
            
            return false;
        }
    }
}
