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
        public int Add(Feest entity, int id)
        {
            string query = "INSERT INTO [dbo].[Feest] ([FeestTitel],[AantalPersonen],[Drank],[Eten],[DrankWensen],[EtenWensen],[Betaling],[Entree],[EntreePrijs],[Consumptie], [ConsumptiePrijs],[Versiering],[BeginDatum],[EindDatum], [ArtiestId], [GebruikerId], [Muziek], [ZaalId]) " +
                           "VALUES(@FeestTitel, @AantalPersonen, @Drank, @Eten, @DrankWensen, @EtenWensen, @Betaling, @Entree, @EntreePrijs, @Consumptie, @ConsumptiePrijs, @Versiering, @BeginDatum, @EindDatum, @ArtiestId, @GebruikerId, @Muziek, @ZaalId); " +
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
                command.Parameters.AddWithValue("@ArtiestId", 0);
                command.Parameters.AddWithValue("@GebruikerId", id);
                command.Parameters.AddWithValue("@Muziek", 0);
                command.Parameters.AddWithValue("ZaalId", 0);

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

        public bool AddDatumLoca(DateTime beginDatum, DateTime eindDatum, int zaalId, int feestId)
        {
            string query = "UPDATE Feest SET BeginDatum = @BeginDatum, EindDatum = @EindDatum, ZaalId = @ZaalId WHERE FeestId = @FeestId;";

            using (SqlCommand command = new SqlCommand(query, ConnectionString))
            {
                command.Parameters.AddWithValue("@BeginDatum", beginDatum);
                command.Parameters.AddWithValue("@EindDatum", eindDatum);
                command.Parameters.AddWithValue("@FeestId", feestId);
                command.Parameters.AddWithValue("@ZaalId", zaalId);

                try
                {
                    ConnectionString.Open();
                    int result = command.ExecuteNonQuery();

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

        //public bool AddArtiest(int feestId, int artiestId, Feest.MuziekKeuze keuze)
        //{
        //    string query = "UPDATE Feest SET ArtiestId = @ArtiestId, Muziek = @Muziek WHERE FeestId = @FeestId;";

        //    using (SqlCommand command = new SqlCommand(query, ConnectionString))
        //    {
        //        command.Parameters.AddWithValue("@ArtiestId", artiestId);
        //        command.Parameters.AddWithValue("@FeestId", feestId);
        //        command.Parameters.AddWithValue("@Muziek", keuze);


        //        try
        //        {
        //            ConnectionString.Open();
        //            int result = command.ExecuteNonQuery();

        //            if (result == 0)
        //            {
        //                ConnectionString.Close();

        //                return false;
        //            }

        //            ConnectionString.Close();
        //            return true;
        //        }
        //        catch (Exception errorException)
        //        {
        //            throw errorException;
        //        }
        //    }

        //}

        public bool AddArtiest(int feestId, int artiestId, Feest.MuziekKeuze keuze)
        {
            using (SqlCommand command = new SqlCommand("dbo.spAddArtiest", ConnectionString))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ArtiestId", artiestId);
                command.Parameters.AddWithValue("@FeestId", feestId);
                command.Parameters.AddWithValue("@MuziekKeuze", keuze);


                try
                {
                    ConnectionString.Open();
                    int result = command.ExecuteNonQuery();

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
                                FeestId = (int)reader["FeestId"],
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
                                Betaling = (Feest.BetalingKeuze)reader["Betaling"],
                                ArtiestId = (int)reader["ArtiestId"],
                                GebruikerId = (int)reader["GebruikerId"],
                                Muziek = (Feest.MuziekKeuze)reader["Muziek"],
                                ZaalId = (int)reader["ZaalId"]
                            };

                            feesten.Add(feest);
                        }
                    }
                }
            }
            ConnectionString.Close();
            return feesten;
        }

        public Feest FeestMetId(int feestId)
        {
            string query = "SELECT * FROM Feest WHERE FeestId = @FeestId";
            using (var cmd = new SqlCommand(query, ConnectionString))
            {
                cmd.Parameters.AddWithValue("@FeestId", feestId);
                try
                {
                    ConnectionString.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Feest feest = new Feest();
                            while (reader.Read())
                            {
                                feest.FeestId = (int) reader["FeestId"];
                                feest.AantalPersonen = (int) reader["AantalPersonen"];
                                feest.Entree = (bool) reader["Entree"];
                                feest.EntreePrijs = (decimal) reader["EntreePrijs"];
                                feest.Consumptie = (Feest.ConsumptieKeuze) reader["Consumptie"];
                                feest.ConsumptieBonPrijs = (decimal) reader["ConsumptiePrijs"];
                                feest.Versierd = (bool) reader["Versiering"];
                                feest.Drank = (bool) reader["Drank"];
                                feest.Eten = (bool) reader["Eten"];
                                feest.DrankWensen = (string) reader["DrankWensen"];
                                feest.EtenWensen = (string) reader["EtenWensen"];
                                feest.BeginDatum = (DateTime) reader["BeginDatum"];
                                feest.EindDatum = (DateTime) reader["EindDatum"];
                                feest.FeestTitel = (string) reader["FeestTitel"];
                                feest.Betaling = (Feest.BetalingKeuze) reader["Betaling"];
                                feest.ArtiestId = (int) reader["ArtiestId"];
                                feest.GebruikerId = (int) reader["GebruikerId"];
                                feest.Muziek = (Feest.MuziekKeuze) reader["Muziek"];
                                feest.ZaalId = (int) reader["ZaalId"];
                            }

                            ConnectionString.Close();
                            return feest;
                        }
                        else
                        {
                            ConnectionString.Close();
                            return null;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
