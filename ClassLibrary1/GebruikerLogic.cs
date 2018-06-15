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

        public GebruikerLogic(string test)
        {
            Console.WriteLine(test);
            //Lege gebruikerLogic voor tests --> WriteLine voor het tonen van de string die nergens word gebruikt..
        }

        public void AddGebruiker(Gebruiker gebruiker)
        {
            _gebruikerContext.Add(gebruiker);
        }

        public List<Gebruiker> GetAllGebruikers()
        {
            return _gebruikerContext.GetAllGebruikers();
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

        public void VerwijderAccount(int gebruikerId)
        {
            _gebruikerContext.VerwijderAccount(gebruikerId);
        }

        public bool AccountCheck(Gebruiker nieuweGebruiker)
        {
            foreach (var gebruiker in GetAllGebruikers())
            {
                if (gebruiker.Gebruikersnaam == nieuweGebruiker.Gebruikersnaam ||
                    gebruiker.Email == nieuweGebruiker.Email ||
                    LeegVeldCheck(nieuweGebruiker) == false ||
                    VeldCheck(nieuweGebruiker) == false)
                {
                    return false;
                }
            }
            return true;
        }

        //Checks op alle ingevoerde velden bij registreren van een account
        public bool VeldCheck(Gebruiker gebruiker)
        {
            try
            {
                if (GebruikersnaamCheck(gebruiker.Gebruikersnaam) &&
                    WachtwoordCheck(gebruiker.Wachtwoord) &&
                    NaamCheck(gebruiker.Naam) &&
                    StraatCheck(gebruiker.Straat) &&
                    WoonplaatsCheck(gebruiker.Woonplaats) &&
                    PostcodeCheck(gebruiker.Postcode) &&
                    HuisnummerCheck(gebruiker.Huisnummer))
                {
                    return true;
                }
                //if (Regex.IsMatch(gebruiker.Gebruikersnaam, @"^(?=.*[a-z])(?=\w*[A-Z])(?=\w*\d)\w{8,15}$") && /*Tenminste 1 hoofdletter, kleine letter en nummer. Tussen 8 en 15 lang*/
                //    Regex.IsMatch(gebruiker.Wachtwoord, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$") && /*Tenminste 1 hoofdletter, kleine letter, nummer en speciale teken. Tussen 8 en 15 lang*/
                //    Regex.IsMatch(gebruiker.Naam, @"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$") /*Geldt nog niet voor d'Haag, de Boer, etc. */  &&
                //    Regex.IsMatch(gebruiker.Straat, @"^[A-Z][a-z]+(\s[A-Z][a-z]*)*$") &&
                //    Regex.IsMatch(gebruiker.Woonplaats, @"^[A-Z][a-z]+(\s[A-Z][a-z]*)*$") &&
                //    Regex.IsMatch(gebruiker.Postcode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$") /*Nederlandse postcode*/ &&
                //    Regex.IsMatch(gebruiker.Huisnummer, @"^[1-9]([0-9]+)?([a-zA-Z]){0,2}?$")) /*Huisnummer met 1 of meer getallen en eventueel maximaal 2 letters*/
                //{
                //    return true;
                //}
            }
            catch (NullReferenceException)
            {
                return false;
            }

            return false;
        }

        //Aparte checks per field voor fieldcheck
        public static bool GebruikersnaamCheck(string gebruikersnaam)
        {
            try
            {
                /*Tenminste 1 hoofdletter, kleine letter en nummer. Tussen 8 en 15 lang*/
                return Regex.IsMatch(gebruikersnaam, @"^(?=.*[a-z])(?=\w*[A-Z])(?=\w*\d)\w{8,15}$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool WachtwoordCheck(string wachtwoord)
        {
            try
            {
                /*Tenminste 1 hoofdletter, kleine letter, nummer en speciale teken. Tussen 8 en 15 lang*/
                return Regex.IsMatch(wachtwoord, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool NaamCheck(string naam)
        {
            try
            {
                /*Geldt nog niet voor d'Haag, de Boer, etc. */
                return Regex.IsMatch(naam, @"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool StraatCheck(string straat)
        {
            try
            {
                return Regex.IsMatch(straat, @"^[A-Z][a-z]+(\s[A-Z][a-z]*)*$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool WoonplaatsCheck(string woonplaats)
        {
            try
            {
                return Regex.IsMatch(woonplaats, @"^[A-Z][a-z]+(\s[A-Z][a-z]*)*$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool PostcodeCheck(string postcode)
        {
            try
            {
                /*Nederlandse postcode*/
                return Regex.IsMatch(postcode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool HuisnummerCheck(string huisnummer)
        {
            try
            {
                /*Huisnummer met 1 of meer getallen en eventueel maximaal 2 letters*/
                return Regex.IsMatch(huisnummer, @"^[1-9]([0-9]+)?([a-zA-Z]){0,2}?$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        //Check of er een leeg veld is
        public bool LeegVeldCheck(Gebruiker gebruiker)
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
