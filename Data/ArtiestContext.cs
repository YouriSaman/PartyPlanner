using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class ArtiestContext : Connection
    {
        public List<Artiest> AlleArtiesten()
        {
            List<Artiest> artiesten = new List<Artiest>();
            ConnectionString.Open();
            string query = @"SELECT * FROM [dbo].[Artiest]";
            using (var cmd = new SqlCommand(query, ConnectionString))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Artiest artiest = new Artiest
                            {
                                ArtiestId = (int)reader["ArtiestId"],
                                Naam = (string)reader["Naam"],
                                Beschikbaar = (bool)reader["Beschikbaar"],
                                Prijs = (decimal)reader["Prijs"]
                            };
                            artiesten.Add(artiest);
                        }
                    }
                }
            }
            return artiesten;
        }

        public Artiest ArtiestMetId(int artiestId)
        {
            ConnectionString.Open();
            Artiest artiest = null;
            string query = "SELECT * FROM Artiest WHERE ArtiestId = @ArtiestId";
            using (var cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@ArtiestId", artiestId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            artiest = new Artiest()
                            {
                                ArtiestId = (int)reader["ArtiestId"],
                                Naam = (string)reader["Naam"],
                                Beschikbaar = (bool)reader["Beschikbaar"],
                                Prijs = (decimal)reader["Prijs"]
                            };

                        }
                    }
                }
            }

            return artiest;
        }
    }
}
