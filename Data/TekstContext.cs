using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class TekstContext : Connection
    {
        public List<Bericht> GetAllBerichten()
        {
            List<Bericht> berichten = new List<Bericht>();
            string query = "SELECT * FROM Bericht";
            ConnectionString.Open();
            using (var cmd = new SqlCommand(query, ConnectionString))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var bericht = new Bericht()
                            {
                                BerichtId = (int)reader["BerichtId"],
                                Gebruikersnaam = (string)reader["Gebruikersnaam"],
                                GebruikerId = (int)reader["GebruikerId"],
                                BerichtTitel = (string)reader["BerichtTitel"],
                                Tekstbericht = (string)reader["Tekst"]
                            };
                            berichten.Add(bericht);
                        }
                    }
                }
            }
            ConnectionString.Close();
            return berichten;
        }

        public Bericht GetBerichtMetId(int id)
        {
            ConnectionString.Open();
            string query = "SELECT * FROM Bericht WHERE BerichtId = @Id";
            using (SqlCommand cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    var bericht = new Bericht()
                    {
                        BerichtId = (int)reader["BerichtId"],
                        Gebruikersnaam = (string)reader["Gebruikersnaam"],
                        GebruikerId = (int)reader["GebruikerId"],
                        BerichtTitel = (string)reader["BerichtTitel"],
                        Tekstbericht = (string)reader["Tekst"]
                    };
                    return bericht;
                }
            }
        }

        public bool AddBericht(Bericht bericht)
        {
            string query = "INSERT INTO [dbo].[Bericht] ([GebruikerId],[Gebruikersnaam],[Tekst],[BerichtTitel])" +
                           "VALUES (@GebruikerId, @Gebruikersnaam, @Tekst, @Titel)";

            using (SqlCommand cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@GebruikerId", bericht.GebruikerId);
                cmd.Parameters.AddWithValue("@Gebruikersnaam", bericht.Gebruikersnaam);
                cmd.Parameters.AddWithValue("@Tekst", bericht.Tekstbericht);
                cmd.Parameters.AddWithValue("@Titel", bericht.BerichtTitel);

                try
                {
                    ConnectionString.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result == 0)
                    {
                        ConnectionString.Close();

                        return false;
                    }

                    ConnectionString.Close();
                    return true;
                }
                catch (Exception errorException)
                {
                    throw errorException;
                }
            }


        }

        public List<Reactie> ReactiesVanBericht(int berichtId)
        {
            List<Reactie> reacties = new List<Reactie>();
            string query = "SELECT * FROM Reactie WHERE BerichtId = @BerichtId";
            using (var cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@BerichtId", berichtId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Reactie reactie = new Reactie()
                            {
                                Gebruikersnaam = (string)reader["Gebruikersnaam"],
                                Tekstbericht = (string)reader["Tekst"]
                            };
                            reacties.Add(reactie);
                        }
                    }
                }

                return reacties;
            }
        }

        public bool AddReactie(Reactie reactie)
        {
            string query = "INSERT INTO [dbo].[Reactie] ([GebruikerId],[Gebruikersnaam],[Tekst],[BerichtId])" +
                           "VALUES (@GebruikerId, @Gebruikersnaam, @Tekst, @BerichtId)";

            using (SqlCommand cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@GebruikerId", reactie.GebruikerId);
                cmd.Parameters.AddWithValue("@Gebruikersnaam", reactie.Gebruikersnaam);
                cmd.Parameters.AddWithValue("@Tekst", reactie.Tekstbericht);
                cmd.Parameters.AddWithValue("@BerichtId", reactie.BerichtId);

                try
                {
                    ConnectionString.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result == 0)
                    {
                        ConnectionString.Close();

                        return false;
                    }

                    ConnectionString.Close();
                    return true;
                }
                catch (Exception errorException)
                {
                    throw errorException;
                }
            }


        }
    }
}
