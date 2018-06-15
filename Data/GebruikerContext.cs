using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Models;

namespace Data
{
    public class GebruikerContext : Connection
    {
        public bool Add(Gebruiker gebruiker)
        {
            ConnectionString.Open();
            string query = @"INSERT INTO [dbo].[Gebruiker] ([Gebruikersnaam],[Wachtwoord],[Admin],[Email],[Naam],[Straat],[Huisnummer],[Postcode],[Woonplaats])
                                VALUES(@Gebruikersnaam, @Wachtwoord, 0, @Email, @Naam, @Straat, @Huisnummer, @Postcode, @Woonplaats)";
            var cmd = new SqlCommand(query, ConnectionString);

            cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruiker.Gebruikersnaam);
            cmd.Parameters.AddWithValue("@Wachtwoord", gebruiker.Wachtwoord);
            cmd.Parameters.AddWithValue("@Email", gebruiker.Email);
            cmd.Parameters.AddWithValue("@Naam", gebruiker.Naam);
            cmd.Parameters.AddWithValue("@Straat", gebruiker.Straat);
            cmd.Parameters.AddWithValue("@Huisnummer", gebruiker.Huisnummer);
            cmd.Parameters.AddWithValue("@Postcode", gebruiker.Postcode);
            cmd.Parameters.AddWithValue("@Woonplaats", gebruiker.Woonplaats);

            try
            {
                int rowsUpdated = cmd.ExecuteNonQuery();

                ConnectionString.Close();

                return rowsUpdated > 0;

            }
            catch
            {
                ConnectionString.Close();
                throw new Exception("Aanpassen database heeft niet gewerkt.");
            }
        }

        public List<Gebruiker> GetAllGebruikers()
        {
            var gebruikers = new List<Gebruiker>();
            ConnectionString.Open();
            using (var command = new SqlCommand("dbo.spAllGebruikers", ConnectionString))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var gebruiker = new Gebruiker()
                            {
                                Gebruikersnaam = (string)reader["Gebruikersnaam"],
                                Wachtwoord = (string)reader["Wachtwoord"],
                                Email = (string)reader["Email"],
                                Naam = (string)reader["Naam"],
                                Straat = (string)reader["Straat"],
                                Huisnummer = (string)reader["Huisnummer"],
                                Postcode = (string)reader["Postcode"],
                                Woonplaats = (string)reader["Woonplaats"],
                                Admin = (bool)reader["Admin"]
                            };

                            gebruikers.Add(gebruiker);
                        }
                    }
                }
            }
            ConnectionString.Close();
            return gebruikers;
        }

        public Gebruiker GetAccountGebruiker(string gebruikersnaam)
        {
            ConnectionString.Open();
            string query = "SELECT * FROM Gebruiker WHERE Gebruikersnaam = @gebruikersnaam";
            using (SqlCommand cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@gebruikersnaam", gebruikersnaam);
                try
                {
                    var resultaat = cmd.ExecuteScalar();
                    if (resultaat != null)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        reader.Read();

                        Gebruiker gebruiker = new Gebruiker
                        {
                            GebruikerId = (int) reader["GebruikerId"],
                            Gebruikersnaam = (string) reader["Gebruikersnaam"],
                            Wachtwoord = (string) reader["Wachtwoord"],
                            Email = (string) reader["Email"],
                            Naam = (string) reader["Naam"],
                            Straat = (string) reader["Straat"],
                            Huisnummer = (string) reader["Huisnummer"],
                            Postcode = (string) reader["Postcode"],
                            Woonplaats = (string) reader["Woonplaats"],
                            Admin = (bool) reader["Admin"]
                        };
                        ConnectionString.Close();
                        return gebruiker;
                    }

                    return null;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public Gebruiker GetProfielGebruiker(int gebruikerId)
        {
            ConnectionString.Open();
            string query = "SELECT * FROM Gebruiker WHERE GebruikerId = @gebruikerId";
            using (SqlCommand cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@gebruikerId", gebruikerId);
                try
                {
                    var resultaat = cmd.ExecuteScalar();
                    if (resultaat != null)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        reader.Read();

                        Gebruiker gebruiker = new Gebruiker
                        {
                            GebruikerId = (int)reader["GebruikerId"],
                            Gebruikersnaam = (string)reader["Gebruikersnaam"],
                            Wachtwoord = (string)reader["Wachtwoord"],
                            Email = (string)reader["Email"],
                            Naam = (string)reader["Naam"],
                            Straat = (string)reader["Straat"],
                            Huisnummer = (string)reader["Huisnummer"],
                            Postcode = (string)reader["Postcode"],
                            Woonplaats = (string)reader["Woonplaats"],
                            Admin = (bool)reader["Admin"]
                        };
                        ConnectionString.Close();
                        return gebruiker;
                    }

                    return null;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        
        public List<Feest> AlleFeestenGebruiker(int gebruikerId)
        {
            var feesten = new List<Feest>();
            ConnectionString.Open();
            string query = "SELECT * FROM [Feest] WHERE GebruikerId = @gebruikerId";
            using (var command = new SqlCommand(query, ConnectionString))
            {
                command.Parameters.AddWithValue("@gebruikerId", gebruikerId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var feest = new Feest()
                            {
                                FeestId = (int)reader["FeestId"],
                                AantalPersonen = (int)reader["AantalPersonen"],
                                Entree = (bool)reader["Entree"],
                                EntreePrijs = (decimal)reader["EntreePrijs"],
                                Consumptie = (Feest.ConsumptieKeuze)reader["Consumptie"],
                                ConsumptieBonPrijs = (decimal)reader["ConsumptiePrijs"],
                                Versierd = (bool)reader["Versiering"],
                                Drank = (bool)reader["Drank"],
                                Eten = (bool)reader["Eten"],
                                EtenWensen = (string)reader["EtenWensen"],
                                DrankWensen = (string)reader["DrankWensen"],
                                BeginDatum = (DateTime)reader["BeginDatum"],
                                EindDatum = (DateTime)reader["EindDatum"],
                                FeestTitel = (string)reader["FeestTitel"],
                                Betaling = (Feest.BetalingKeuze)reader["Betaling"]
                            };

                            feesten.Add(feest);
                        }
                    }
                }
            }
            ConnectionString.Close();
            return feesten;
        }

        public void WijzigAccount(Gebruiker gebruiker)
        {
            ConnectionString.Open();
            using (var cmd = new SqlCommand("spWijzigAccount", ConnectionString))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@GebruikerId", gebruiker.GebruikerId);
                cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruiker.Gebruikersnaam);
                cmd.Parameters.AddWithValue("@Wachtwoord", gebruiker.Wachtwoord);
                cmd.Parameters.AddWithValue("@Email", gebruiker.Email);
                cmd.Parameters.AddWithValue("@Naam", gebruiker.Naam);
                cmd.Parameters.AddWithValue("@Straat", gebruiker.Straat);
                cmd.Parameters.AddWithValue("@Huisnummer", gebruiker.Huisnummer);
                cmd.Parameters.AddWithValue("@Postcode", gebruiker.Postcode);
                cmd.Parameters.AddWithValue("@Woonplaats", gebruiker.Woonplaats);

                cmd.ExecuteReader();
                ConnectionString.Close();
            }
        }

        public void VerwijderAccount(int gebruikerId)
        {
            ConnectionString.Open();
            string query = "DELETE FROM Gebruiker WHERE GebruikerId = @GebruikerId";
            using (var cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@GebruikerId", gebruikerId);

                cmd.ExecuteReader();
                ConnectionString.Close();
            }
        }
    }
}
