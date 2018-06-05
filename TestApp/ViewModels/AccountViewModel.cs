using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Models;

namespace TestApp.ViewModels
{
    public class AccountViewModel
    {
        Data.Data data = new Data.Data();
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
        public Gebruiker Gebruiker { get; set; }

        public FeestViewModel feestViewModel = new FeestViewModel();

        public List<Gebruiker> GetAllGebruikers()
        {
            return data.GetAllGebruikers();
        }

        public bool LoginCheck()
        {
            foreach (var gebruiker in GetAllGebruikers())
            {
                if (gebruiker.Gebruikersnaam == Gebruikersnaam && gebruiker.Wachtwoord == Wachtwoord)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AccountCheck()
        {
            foreach (var gebruiker in GetAllGebruikers())
            {
                if (gebruiker.Gebruikersnaam == Gebruikersnaam ||
                    gebruiker.Email == Email ||
                    PasswordCheck() == false ||
                    EmptyFieldCheck() == false ||
                    StringTest() == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool PasswordCheck()
        {
            try
            {
                if (Regex.IsMatch(Wachtwoord, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"))
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

        public bool StringTest()
        {
            if (Regex.IsMatch(Gebruikersnaam, @"^(?=.*[a-z])(?=\w*[A-Z])(?=\w*\d)\w{8,15}$") && /*Tenminste 1 hoofdletter, kleine letter en nummer*/
                Regex.IsMatch(Wachtwoord, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$") && /*Tenminste 1 hoofdletter, kleine letter, nummer en speciale teken*/
                Regex.IsMatch(Naam, @"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$") /*Geldt nog niet voor d'Haag, de Boer, etc. */  &&
                Regex.IsMatch(Straat, @"^[A-Z][a-z]+(\s[A-Z][a-z]*)*") &&
                Regex.IsMatch(Woonplaats, @"^[A-Z][a-z]+(\s[A-Z][a-z]*)*") &&
                Regex.IsMatch(Postcode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$") /*Nederlandse postcode*/ &&
                Regex.IsMatch(Huisnummer, @"^[1-9]([0-9]+)?([a-zA-Z]){0,2}?$")) /*Huisnummer met 1 of meer getallen en eventueel maximaal 2 letters*/
            {
                return true;
            }

            return false;
        }

        public bool EmptyFieldCheck()
        {
            try
            {
                if (Gebruikersnaam.Length > 0 || Wachtwoord.Length > 0 || Email.Length > 0 || Naam.Length > 0 || Straat.Length > 0 || Huisnummer.Length > 0 || Woonplaats.Length > 0)
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
