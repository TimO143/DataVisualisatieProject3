using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace Prototype_project_3
{
    class DataGetter
    {
        private NpgsqlConnection connection;

        public DataGetter()
        {
            connection = new NpgsqlConnection("Server = 127.0.0.1; port = 5432; User Id = postgres; Password = postgres; Database = Retake project 3; ");
        }
        public DataTable GetData(string statement)
        {
            DataTable dataStore = new DataTable();
            NpgsqlCommand sqlstatement = new NpgsqlCommand(statement, connection);
            NpgsqlDataAdapter dataGrabber = new NpgsqlDataAdapter(sqlstatement);

            connection.Open();
            dataGrabber.Fill(dataStore);
            connection.Close();

            return dataStore;
        }
    }
}
