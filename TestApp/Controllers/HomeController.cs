using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            GebruikerLogic logic = new GebruikerLogic();
            HomeIndexViewModel viewModelIndex = new HomeIndexViewModel();
            viewModelIndex.gebruikers = logic.GetAllGebruikers();
            return View(viewModelIndex);
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
