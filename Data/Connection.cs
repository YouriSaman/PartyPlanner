using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data
{
    public abstract class Connection
    {
        public readonly SqlConnection ConnectionString;

        protected Connection()
        {
            ConnectionString =
                new SqlConnection(
                    "Server=mssql.fhict.local;Database=dbi383661;User Id=dbi383661;Password=YouriS21;");
        }
    }
}
