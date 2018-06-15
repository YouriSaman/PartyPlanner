using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class FeestController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            FeestViewModel viewModel = new FeestViewModel();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(Feest feest)
        {
            FeestLogic logic = new FeestLogic();
            int id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            logic.MaakFeest(feest, id);
            return RedirectToAction("LocaDate", logic);
        }

        [Authorize]
        public IActionResult LocaDate(FeestLogic logic)
        {
            FeestViewModel model = new FeestViewModel();
            model.FeestId = logic.FeestId;
            model.Zalen = logic.AlleZalen();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult LocaDate(FeestViewModel viewModel)
        {
            FeestLogic logic = new FeestLogic();
            var beginDatum = viewModel.BeginDatum;
            var eindDatum = viewModel.EindDatum;
            var feestId = viewModel.FeestId;
            var zaalId = viewModel.ZaalId;
            viewModel.Zalen = logic.AlleZalen();

            //Check of er geen feesten voor of na zijn en dan wordt dit pas toegevoegd aan het feest
            if (logic.VoegDatumLocaToe(beginDatum, eindDatum, zaalId , feestId) == true)
            {
                return RedirectToAction("Muziek", viewModel);
            }

            ViewData["InvalidDate"] = "Één of meerdere datums overlappen, pas de datum(s) aan!";
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Muziek(FeestViewModel viewModel)
        {
            FeestLogic logic = new FeestLogic();
            var artiesten = logic.AlleArtiesten();
            int feestId = viewModel.FeestId;
            logic.FeestId = feestId;

            FeestViewModel model = new FeestViewModel();
            model.FeestId = feestId;
            model.Artiesten = artiesten;

            
            return View(model);
        }

        [HttpPost]
        public IActionResult Muziek(int ArtiestId, int FeestId, Feest.MuziekKeuze Muziek)
        {
            FeestLogic logic = new FeestLogic();
            logic.VoegArtiestToeAanFeest(FeestId, ArtiestId, Muziek);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult View(int FeestId)
        {
            FeestViewModel viewModel = new FeestViewModel();
            FeestLogic logic = new FeestLogic();
            viewModel.Feest = logic.FeestMetId(FeestId);

            //Zaalnaam ophalen
            Zaal zaal = logic.ZaalMetId(viewModel.Feest.ZaalId);
            if (zaal != null)
            {
                viewModel.ZaalNaam = zaal.Naam;
            }
            else
            {
                viewModel.ZaalNaam = "";
            }
            
            //Ariestnaam ophalen
            Artiest artiest = logic.ArtiestMetId(viewModel.Feest.ArtiestId);
            if (artiest != null)
            {
                viewModel.ArtiestNaam = artiest.Naam;
            }
            else
            {
                viewModel.ArtiestNaam = "";
            }
            
            //Als er geen zaal is gekozen dan is er ook geen ratio, hier wordt dat goed afgehandeld
            if (logic.PersonenVsCapaciteit(FeestId) != null)
            {
                viewModel.AantalPerCapaciteit = logic.PersonenVsCapaciteit(FeestId);
            }
            else
            {
                viewModel.AantalPerCapaciteit = new PersonenCapaciteit(0, 0);
            }
            return View(viewModel);
        }
    }
}