using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Agrodit_angular.Utility
{
    public class MSSqlDatabase : IDisposable
    {
        public SqlConnection Connection;

        public MSSqlDatabase(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}

