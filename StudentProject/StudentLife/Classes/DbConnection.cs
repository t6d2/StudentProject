using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudentLife.Classes
{
    public class DbConnection
    {

        public SqlConnection SqlConn { get; }
        public SqlCommand Comm { get; }

        public DbConnection()
        {
            SqlConn = new SqlConnection(PrepareStringConnection());
            Comm = SqlConn.CreateCommand();
            SqlConn.Open();
        }

        public string PrepareStringConnection()
        {
            return @"Data Source=.\sqlexpress;Database=StudentLife;Integrated Security=True";
        }

    }
}
