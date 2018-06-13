using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Logic;
using Models;

namespace Test
{
    public class GebruikerTestContext
    {
        // ####################################
        // # Unit test methods with fake data #
        // ####################################

        public static List<Gebruiker> UnitTestGebruikers = new List<Gebruiker>();

        public bool UnitTestAccountCheck(Gebruiker nieuweGebruiker)
        {
            foreach (var gebruiker in UnitTestGebruikers)
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

        public void VulUnitTestGebLijst()
        {
            UnitTestGebruikers.Add(new Gebruiker(2, "Keesje", "kees1!", "mail2@gmail.com", "Kees Achternaam", "Straat", "11a", "4587LK", "Woonplaats", true));
        }

        public void VoegTestGebruikerToe(Gebruiker nieuweGebruiker)
        {
            UnitTestGebruikers.Add(nieuweGebruiker);
        }

        public bool UnitTestLoginCheck(Gebruiker gebruiker)
        {
            foreach (var unitTestGebruiker in UnitTestGebruikers)
            {
                if (unitTestGebruiker.Gebruikersnaam == gebruiker.Gebruikersnaam && unitTestGebruiker.Wachtwoord == gebruiker.Wachtwoord)
                {
                    return true;
                }
            }
            return false;
        }

        //Checks op alle ingevoerde velden bij registreren van een account
        public bool FieldCheck(Gebruiker gebruiker)
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
