using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private Logic.Logic logic = new Logic.Logic();

        public IActionResult Index()
        {
            HomeIndexViewModel viewModelIndex = new HomeIndexViewModel();
            viewModelIndex.gebruikers = logic.GetAllGebruikers();
            return View(viewModelIndex);
        }

        public IActionResult Login()
        {
            HomeIndexViewModel viewModelLogin = new HomeIndexViewModel();
            viewModelLogin.gebruikersnaam = logic.gebruikersnaam;
            viewModelLogin.wachtwoord = logic.wachtwoord;
            return View();
        }

        public IActionResult LoginSucces()
        {
            return View();
        }

        public IActionResult EditGebruiker(int id)
        {
            EditGebruikerViewModel viewModel = new EditGebruikerViewModel();
            
            return View(viewModel);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
