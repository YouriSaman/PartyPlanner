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
            model.Zalen = logic.GetAllZalen();
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
            viewModel.Zalen = logic.GetAllZalen();

            if (logic.AddDatumLocaFeest(beginDatum, eindDatum, zaalId , feestId) == true)
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
            var artiesten = logic.GetAllArtiesten();
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
            logic.AddArtiestFeest(FeestId, ArtiestId, Muziek);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int FeestId)
        {
            FeestViewModel viewModel = new FeestViewModel();
            FeestLogic logic = new FeestLogic();
            viewModel.Feest = logic.GetFeestMetId(FeestId);
            viewModel.AantalPerCapaciteit = logic.PersonenVsCapaciteit(FeestId);
            return View(viewModel);
        }
    }
}