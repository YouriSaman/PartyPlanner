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
    public class ForumController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ForumViewModel viewModel = new ForumViewModel();
            ForumLogic logic = new ForumLogic();
            viewModel.Berichten = logic.AlleBerichten();
            return View(viewModel);
        }

        public IActionResult Bericht(int BerichtId)
        {
            ForumLogic logic = new ForumLogic();
            BerichtViewModel model = new BerichtViewModel();
            var bericht = logic.BerichtMetId(BerichtId);
            model.BerichtId = BerichtId;
            model.Titel = bericht.BerichtTitel;
            model.Tekst = bericht.Tekstbericht;
            model.Gebruikersnaam = bericht.Gebruikersnaam;
            model.Reacties = logic.ReactiesVanBericht(BerichtId);
            return View(model);
        }

        public IActionResult MaakBericht()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MaakBericht(BerichtViewModel model)
        {
            Bericht bericht = new Bericht();
            bericht.Tekstbericht = model.Tekst;
            bericht.BerichtTitel = model.Titel;
            bericht.Gebruikersnaam = User.Identity.Name;
            bericht.GebruikerId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            ForumLogic logic = new ForumLogic();
            logic.MaakBericht(bericht);

            return RedirectToAction("Index");
        }

        public IActionResult MaakReactie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MaakReactie(BerichtViewModel viewModel, int BerichtId)
        {
            var logic = new ForumLogic();
            var model = new BerichtViewModel();
            model.Reactie.BerichtId = BerichtId;
            model.Reactie.Gebruikersnaam = User.Identity.Name;
            model.Reactie.Tekstbericht = viewModel.Reactie.Tekstbericht;
            model.Reactie.GebruikerId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            logic.MaakReactie(model.Reactie);
            return RedirectToAction("Bericht", new{ BerichtId } );
        }
    }
}