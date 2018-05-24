using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class FeestController : Controller
    {
        private Logic.Logic logic = new Logic.Logic();
        public IActionResult Index()
        {
            FeestViewModel viewModel = new FeestViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(Feest feest)
        {
            FeestLogic logic = new FeestLogic();
            //FeestViewModel viewModel = new FeestViewModel();
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
            
            logic.MaakFeest(feest);
            return RedirectToAction("LocaDate", logic);
        }

        //public IActionResult LocaDate(FeestViewModel viewModel)
        //{
        //    Feest feest = new Feest();
        //    feest.AantalPersonen = viewModel.AantalPersonen;
        //    if (feest.Drank == false)
        //    {
        //        feest.DrankWensen = "";
        //    }
        //    if (feest.Eten == false)
        //    {
        //        feest.EtenWensen = "";
        //    }

        //    foreach (var Feest in viewModel.GetAllFeesten())
        //    {
        //        viewModel.Feesten.Add(Feest);
        //    }
        //    viewModel.Feesten.Add(feest);
        //    return View(viewModel);
        //}

        
        public IActionResult LocaDate(FeestLogic logic)
        {
            FeestViewModel viewModel = new FeestViewModel();
            Feest feest = new Feest();
            viewModel.BeginDatum = feest.BeginDatum;
            viewModel.EindDatum = feest.EindDatum;

            if (logic.DatumCheck() == true)
            {
                return RedirectToAction("Muziek");
            }
            return View();

        }


        public IActionResult Muziek(FeestViewModel viewModel)
        {
            List<Artiest> artiesten = new List<Artiest>
            {
                new Artiest("Artiest1", 15.75, true),
                new Artiest("Artiest2", 25, false)
            };

            FeestViewModel viewModelMuziek = new FeestViewModel();
            viewModelMuziek.Artiesten = artiesten;
            return View(viewModelMuziek);
        }

        //[HttpPost]
        //public IActionResult Muziek(Feest feest)
        //{
            
        //    return View();

        //}
    }
}