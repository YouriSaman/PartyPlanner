﻿using System;
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
        AccountViewModel viewModel = new AccountViewModel();

        public IActionResult Index()
        {
            int gebruikerId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            GebruikerLogic logic = new GebruikerLogic();
            var feesten = logic.FeestenGebruiker(gebruikerId);
            viewModel.feestViewModel.Feesten = feesten;
            var gebruiker = logic.ProfielGebruiker(gebruikerId);
            gebruiker.GebruikerId = gebruikerId;
            viewModel.Gebruiker = gebruiker;
            return View(viewModel);
        }

        public IActionResult Login()
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
                var gebruikerAccount = logic.AccountGebruiker(loginViewModel.Gebruikersnaam);
                PerformLogin(gebruikerAccount);
                return RedirectToAction("Index", "Home");
            }

            ViewData["InvalidLogin"] = "Één of meerdere velden zijn niet correct ingevuld!";
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

            if (logic.AccountCheck(gebruiker) == true)
            {
                logic.AddGebruiker(gebruiker);
                return RedirectToAction("Login");
            }

            ViewData["InvalidRegister"] = "Er is iets fout gegaan bij het aanmaken van een nieuw account, pas de velden aan en probeer het opnieuw!";
            return View();
        }

        public IActionResult VeranderAccount()
        {
            int gebruikerId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            GebruikerLogic logic = new GebruikerLogic();
            var gebruiker = logic.ProfielGebruiker(gebruikerId);
            viewModel.Gebruiker = gebruiker;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult VeranderAccount(AccountViewModel model)
        {
            Gebruiker gebruiker = model.Gebruiker;
            GebruikerLogic logic = new GebruikerLogic();
            
            //Checks op ingevoerde velden
            if (logic.LeegVeldCheck(gebruiker) && logic.VeldCheck(gebruiker))
            {
                logic.WijzigAccount(gebruiker);
                return RedirectToAction("Index");
            }

            ViewData["InvalidUpdate"] = "Er is iets fout gegaan bij het wijzigen van het account, pas de velden aan en probeer het opnieuw!";
            return View(model);
        }

        public IActionResult DeleteAccount()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAccount(int GebruikerId)
        {
            GebruikerLogic logic = new GebruikerLogic();
            logic.VerwijderAccount(GebruikerId);

            TempData["DeleteAccountSucces"] = "Account is succesvol verwijderd!";
            return RedirectToAction("LogOut");
        }

        private void PerformLogin(Gebruiker gebruiker)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, gebruiker.Gebruikersnaam),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(gebruiker.GebruikerId)),
                new Claim(ClaimTypes.GivenName, gebruiker.Naam),
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