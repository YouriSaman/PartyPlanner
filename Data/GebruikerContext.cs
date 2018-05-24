using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class GebruikerContext : Connection
    {
        public bool Add(Gebruiker entity)
        {
            ConnectionString.Open();
            string query = @"INSERT INTO [dbo].[Gebruiker] ([Gebruikersnaam],[Wachtwoord],[Admin],[Email],[Naam],[Straat],[Huisnummer],[Postcode],[Woonplaats])
                                VALUES(@Gebruikersnaam, @Wachtwoord, 0, @Email, @Naam, @Straat, @Huisnummer, @Postcode, @Woonplaats)";
            var cmd = new SqlCommand(query, ConnectionString);

            cmd.Parameters.AddWithValue("@Gebruikersnaam", entity.Gebruikersnaam);
            cmd.Parameters.AddWithValue("@Wachtwoord", entity.Wachtwoord);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            cmd.Parameters.AddWithValue("@Naam", entity.Naam);
            cmd.Parameters.AddWithValue("@Straat", entity.Straat);
            cmd.Parameters.AddWithValue("@Huisnummer", entity.Huisnummer);
            cmd.Parameters.AddWithValue("@Postcode", entity.Postcode);
            cmd.Parameters.AddWithValue("@Woonplaats", entity.Woonplaats);

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

        public List<Gebruiker> LoginAccGebruikers()
        {
            var gebruikers = new List<Gebruiker>();
            ConnectionString.Open();
            using (var command = new SqlCommand("dbo.spLoginGebruikers", ConnectionString))
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
                                Admin = (bool)reader["Admin"]
                            };

                            gebruikers.Add(gebruiker);
                        }
                    }
                }
            }

            return gebruikers;
        }
    }
}
