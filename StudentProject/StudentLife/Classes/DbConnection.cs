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
        public string PrepareStringConnection()
        {
            return @"Data Source=.\sqlexpress;Database=StudentLife;Integrated Security=True";
        }
    }
}
