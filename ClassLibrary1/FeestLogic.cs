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
            if (feest.Drank == false) feest.DrankWensen = ""; //Geen drank --> geen wensen
            if (feest.Eten == false) feest.EtenWensen = ""; //Geen eten --> geen wensen

            if (feest.EntreeIndic == 0)
                feest.Entree = false; //Geen entree gekozen
            else if (feest.EntreeIndic == 1) feest.Entree = true; //Wel entree gekozen

            if (feest.VersierdIndic == 0)
                feest.Versierd = false; //Niet versierd gekozen
            else if (feest.VersierdIndic == 1) feest.Versierd = true; //Wel versierd gekozen

            if (feest.Entree == false) feest.EntreePrijs = 0; //Geen entree --> prijs = 0
            if (feest.Consumptie == Feest.ConsumptieKeuze.Allin) feest.ConsumptieBonPrijs = 0; //All-in betekend geen consumptieprijs

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
            if (keuze == Feest.MuziekKeuze.Niks || keuze == Feest.MuziekKeuze.Zaal)
            {
                artiestId = 0;
            }
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

        public List<Zaal> FeestenPerZaal()
        {
            return _feestContext.FeestenPerZaal();
        }

        public PersonenCapaciteit PersonenVsCapaciteit(int feestId)
        {
            return _feestContext.PersonenVsCapaciteit(feestId);
        }

        private bool DatumCheck(DateTime beginDatum, DateTime eindDatum)
        {
            var goedeDatum = false;
            foreach (var feest in GetAllFeesten())
            {
                if (beginDatum < eindDatum)
                {
                    if (eindDatum < feest.BeginDatum && eindDatum < feest.EindDatum || //Feest eindigt eerder dan andere feest
                        beginDatum > feest.BeginDatum && beginDatum > feest.EindDatum) //Feest begint later dan andere feest
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
