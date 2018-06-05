using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Data;
using Models;

namespace Logic
{
    public class GebruikerLogic
    {
        public int GebruikerId { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public bool Admin { get; set; }
        private GebruikerContext _gebruikerContext;

        public GebruikerLogic()
        {
            _gebruikerContext = new GebruikerContext();
        }

        public void AddGebruiker(Gebruiker gebruiker)
        {
            _gebruikerContext.Add(gebruiker);
        }

        public List<Gebruiker> GetAllGebruikers()
        {
            return _gebruikerContext.GetAllGebruikers();
        }

        public List<Gebruiker> GetAccGebruikers()
        {
            return _gebruikerContext.LoginAccGebruikers();
        }

        public bool LogCheck(string gebruikersnaam, string wachtwoord)
        {
            return _gebruikerContext.Checklogin(gebruikersnaam, wachtwoord);
        }

        public Gebruiker AccountGebruiker(string gebruikersnaam)
        {
            return _gebruikerContext.GetAccountGebruiker(gebruikersnaam);
        }

        public Gebruiker ProfielGebruiker(int gebruikerId)
        {
            return _gebruikerContext.GetProfielGebruiker(gebruikerId);
        }

        public List<Feest> FeestenGebruiker(int gebruikerId)
        {
            return _gebruikerContext.AlleFeestenGebruiker(gebruikerId);
        }

        public void WijzigAccount(Gebruiker gebruiker)
        {
            _gebruikerContext.WijzigAccount(gebruiker);
        }

        public bool LoginCheck(string gebruikersnaam, string wachtwoord)
        {
            foreach (var gebruiker in GetAllGebruikers())
            {
                if (gebruiker.Gebruikersnaam == gebruikersnaam && gebruiker.Wachtwoord == wachtwoord)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AccountCheck(Gebruiker nieuweGebruiker)
        {
            foreach (var gebruiker in GetAllGebruikers())
            {
                if (gebruiker.Gebruikersnaam == nieuweGebruiker.Gebruikersnaam ||
                    gebruiker.Email == nieuweGebruiker.Email ||
                    EmptyFieldCheck(nieuweGebruiker) == false ||
                    FieldCheck(nieuweGebruiker) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool FieldCheck(Gebruiker gebruiker)
        {
            try
            {
                if (Regex.IsMatch(gebruiker.Gebruikersnaam, @"^(?=.*[a-z])(?=\w*[A-Z])(?=\w*\d)\w{8,15}$") && /*Tenminste 1 hoofdletter, kleine letter en nummer*/
                    Regex.IsMatch(gebruiker.Wachtwoord, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$") && /*Tenminste 1 hoofdletter, kleine letter, nummer en speciale teken*/
                    Regex.IsMatch(gebruiker.Naam, @"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$") /*Geldt nog niet voor d'Haag, de Boer, etc. */  &&
                    Regex.IsMatch(gebruiker.Straat, @"^[A-Z][a-z]+(\s[A-Z][a-z]*)*$") &&
                    Regex.IsMatch(gebruiker.Woonplaats, @"^[A-Z][a-z]+(\s[A-Z][a-z]*)*$") &&
                    Regex.IsMatch(gebruiker.Postcode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$") /*Nederlandse postcode*/ &&
                    Regex.IsMatch(gebruiker.Huisnummer, @"^[1-9]([0-9]+)?([a-zA-Z]){0,2}?$")) /*Huisnummer met 1 of meer getallen en eventueel maximaal 2 letters*/
                {
                    return true;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }

            return false;
        }

        public bool EmptyFieldCheck(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker.Gebruikersnaam.Length > 0 && gebruiker.Wachtwoord.Length > 0 && gebruiker.Email.Length > 0 && gebruiker.Naam.Length > 0 && gebruiker.Straat.Length > 0 && gebruiker.Huisnummer.Length > 0 && gebruiker.Woonplaats.Length > 0)
                {
                    return true;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }

            return false;
        }
    }
}
