using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class ZaalContext : Connection
    {
        public List<Zaal> GetAllZalen()
        {
            ConnectionString.Open();
            List<Zaal> zalen = new List<Zaal>();
            string query = "SELECT * FROM Zaal";
            using (var cmd = new SqlCommand(query, ConnectionString))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Zaal zaal = new Zaal()
                            {
                                ZaalId = (int)reader["ZaalId"],
                                Naam = (string)reader["Naam"],
                                Plaats = (string)reader["Plaats"],
                                Capaciteit = (int)reader["Capaciteit"],
                                Prijs = (decimal)reader["Prijs"]
                            };
                            zalen.Add(zaal);
                        }
                    }   
                }

                ConnectionString.Close();
                return zalen;
            }
        }
    }
}
