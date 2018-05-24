using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace TestApp.ViewModels
{
    public class EditGebruikerViewModel
    {
        public Gebruiker Gebruiker { get; set; }
        public int SelectedGebruikerId { get; set; }
        public List<SelectListItem> GebruikersList { get; private set; } = new List<SelectListItem>();

        public EditGebruikerViewModel() { }

        public EditGebruikerViewModel(Gebruiker gebruiker, List<Gebruiker> gebruikers)
        {
            Gebruiker = gebruiker;
            SelectedGebruikerId = gebruiker.GebruikerId;

            foreach (var Gbruiker in gebruikers)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Text = Gbruiker.Naam;
                listItem.Value = Gbruiker.GebruikerId.ToString();

            }
        }
    }
}
