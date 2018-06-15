using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            FeestLogic logic = new FeestLogic();
            HomeIndexViewModel viewModel = new HomeIndexViewModel();
            viewModel.Zalen = logic.FeestenPerZaal();
            return View(viewModel);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Fontys contact page.";

            FeestViewModel model = new FeestViewModel();
            FeestLogic logic = new FeestLogic();
            model.Zalen = logic.AlleZalen();

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
