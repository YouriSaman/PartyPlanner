using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class FeestContext : Connection
    {
        public int Add(Feest entity)
        {
            string query = "INSERT INTO [dbo].[Feest] ([FeestTitel],[AantalPersonen],[Drank],[Eten],[DrankWensen],[EtenWensen],[Betaling],[Entree],[EntreePrijs],[Consumptie], [ConsumptiePrijs],[Versiering],[BeginDatum],[EindDatum]) " +
                           "VALUES(@FeestTitel, @AantalPersonen, @Drank, @Eten, @DrankWensen, @EtenWensen, @Betaling, @Entree, @EntreePrijs, @Consumptie, @ConsumptiePrijs, @Versiering, @BeginDatum, @EindDatum); " +
                           "SELECT @@IDENTITY AS NewID;";

            using (SqlCommand command = new SqlCommand(query, ConnectionString))
            {
                command.Parameters.AddWithValue("@FeestTitel", entity.FeestTitel);
                command.Parameters.AddWithValue("@AantalPersonen", entity.AantalPersonen);
                command.Parameters.AddWithValue("@Drank", entity.Drank);
                command.Parameters.AddWithValue("@Eten", entity.Eten);
                command.Parameters.AddWithValue("@DrankWensen", entity.DrankWensen);
                command.Parameters.AddWithValue("@EtenWensen", entity.EtenWensen);
                command.Parameters.AddWithValue("@Betaling", entity.Betaling);
                command.Parameters.AddWithValue("@Entree", entity.Entree);
                command.Parameters.AddWithValue("@EntreePrijs", entity.EntreePrijs);
                command.Parameters.AddWithValue("@Consumptie", entity.Consumptie);
                command.Parameters.AddWithValue("@ConsumptiePrijs", entity.ConsumptieBonPrijs);
                command.Parameters.AddWithValue("@Versiering", entity.Versierd);
                command.Parameters.AddWithValue("@BeginDatum", DateTime.Now);
                command.Parameters.AddWithValue("@EindDatum", DateTime.Now.AddDays(1));

                try
                {
                    ConnectionString.Open();

                    SqlDataReader rowsAffected = command.ExecuteReader();

                    int feestid = 0;

                    while (rowsAffected.Read())
                    {
                        var decimalId = (decimal) rowsAffected["NewID"];

                        feestid = Convert.ToInt32(decimalId);
                    }

                    ConnectionString.Close();

                    return feestid;
                }
                catch (Exception errorException)
                {
                    throw errorException;
                }
            }

        }

        public int AddDatum(Feest entity)
        {
            string query = "UPDATE Feest SET BeginDatum = @BeginDatum, EindDatum = 145 WHERE id = @FeestId;";

            using (SqlCommand command = new SqlCommand(query, ConnectionString))
            {
                command.Parameters.AddWithValue("@BeginDatum", entity.BeginDatum);
                command.Parameters.AddWithValue("@EindDatum", entity.EindDatum);
                command.Parameters.AddWithValue("@FeestId", entity.FeestId);
                

                try
                {
                    ConnectionString.Open();

                    SqlDataReader rowsAffected = command.ExecuteReader();

                    int feestid = 0;

                    while (rowsAffected.Read())
                    {
                        var decimalId = (decimal)rowsAffected["NewID"];

                        feestid = Convert.ToInt32(decimalId);
                    }

                    ConnectionString.Close();

                    return feestid;
                }
                catch (Exception errorException)
                {
                    throw errorException;
                }
            }

        }

        public List<Feest> DatumsFeesten()
        {
            var feesten = new List<Feest>();
            ConnectionString.Open();
            using (var command = new SqlCommand("dbo.spDatumsFeesten", ConnectionString))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var feest = new Feest()
                            {
                                BeginDatum = (DateTime)reader["BeginDatum"],
                                EindDatum = (DateTime)reader["EindDatum"]
                            };

                            feesten.Add(feest);
                        }
                    }
                }
            }

            return feesten;
        }

        public List<Feest> GetAllFeesten()
        {
            var feesten = new List<Feest>();
            ConnectionString.Open();
            using (var command = new SqlCommand("dbo.spAllFeesten", ConnectionString))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var feest = new Feest()
                            {
                                AantalPersonen = (int)reader["AantalPersonen"],
                                Entree = (bool)reader["Entree"],
                                EntreePrijs = (decimal)reader["EntreePrijs"],
                                Consumptie = (Feest.ConsumptieKeuze) reader["Consumptie"],
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
    }
}
