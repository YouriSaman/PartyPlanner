using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class AccountController : Controller
    {
        Logic.Logic logic = new Logic.Logic();
        AccountViewModel viewModel = new AccountViewModel();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            AccountViewModel loginViewModel = new AccountViewModel();
            loginViewModel.Gebruikersnaam = logic.gebruikersnaam;
            loginViewModel.Wachtwoord = logic.wachtwoord;

            return View();
        }

        public IActionResult LoginSucces()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Gebruiker gebruiker)
        {
            AccountViewModel loginViewModel = new AccountViewModel();
            GebruikerLogic logic = new GebruikerLogic();
            loginViewModel.Gebruikersnaam = gebruiker.Gebruikersnaam;
            loginViewModel.Wachtwoord = gebruiker.Wachtwoord;
            if (logic.LoginCheck(loginViewModel.Gebruikersnaam, loginViewModel.Wachtwoord) == true)
            {
                PerformLogin(gebruiker);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registreer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registreer(Gebruiker gebruiker)
        {
            GebruikerLogic logic = new GebruikerLogic();
            logic.Gebruikersnaam = gebruiker.Gebruikersnaam;
            logic.Email = gebruiker.Email;
            logic.Wachtwoord = gebruiker.Wachtwoord;
            logic.Naam = gebruiker.Naam;
            logic.Straat = gebruiker.Straat;
            logic.Postcode = gebruiker.Postcode;
            logic.Huisnummer = gebruiker.Huisnummer;
            logic.Woonplaats = gebruiker.Woonplaats;

            if (logic.AccountCheck() == true)
            {
                logic.AddGebruiker(gebruiker);
                return RedirectToAction("Login");

            }

            return RedirectToAction("LoginSucces");
        }

        public IActionResult RegistreerSucces()
        {
            return View(viewModel);
        }

        private void PerformLogin(Gebruiker gebruiker)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, gebruiker.Gebruikersnaam),
                new Claim(ClaimTypes.Role, gebruiker.Admin ? "Admin" : "Gebruiker"),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)).Wait();
        }
    }
}