using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Models
{
    public class Gebruiker
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

        public Gebruiker() { }
        public Gebruiker(int gebruikerId, string gebruikersnaam, string wachtwoord, string email, string naam, string straat, string huisnummer, string postcode, string woonplaats, bool admin)
        {
            GebruikerId = gebruikerId;
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
            Email = email;
            Naam = naam;
            Straat = straat;
            Huisnummer = huisnummer;
            Woonplaats = woonplaats;
            Admin = admin;
            Postcode = postcode;
        }
    }
}
