using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class ForumLogic
    {
        private TekstContext _tekstContext;

        public ForumLogic()
        {
            _tekstContext = new TekstContext();
        }

        public List<Bericht> AlleBerichten()
        {
            return _tekstContext.GetAllBerichten();
        }

        public Bericht BerichtMetId(int id)
        {
            return _tekstContext.GetBerichtMetId(id);
        }

        public bool MaakBericht(Bericht bericht)
        {
            return _tekstContext.AddBericht(bericht);
        }

        public bool MaakReactie(Reactie reactie)
        {
            return _tekstContext.AddReactie(reactie);
        }

        public List<Reactie> ReactiesVanBericht(int berichtId)
        {
            return _tekstContext.ReactiesVanBericht(berichtId);
        }
    }
}
