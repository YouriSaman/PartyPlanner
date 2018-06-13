using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Test
{
    public class FeestTestContext
    {
        public static List<Feest> UnitTestFeesten = new List<Feest>();

        public void VulUnitTestFeestLijst()
        {
            UnitTestFeesten.Add(new Feest(11, Feest.BetalingKeuze.Organisatie, false, 0, Feest.ConsumptieKeuze.Allin, 0, true, true, "Bier en wijn", false, "", DateTime.Now.AddDays(10), DateTime.Now.AddDays(11), null));
        }

        public bool DatumCheck(DateTime beginDatum, DateTime eindDatum)
        {
            var goedeDatum = false;
            foreach (var feest in UnitTestFeesten)
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

            if (goedeDatum)
            {
                return true;
            }

            return false;
        }
    }
}
