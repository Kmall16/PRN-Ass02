using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessData
{
    public class BaseDAL
    {
        public DataProvider dataProvider { get; set; } = null;
        public SqlConnection connection = null;

        public BaseDAL()
        {
            var connectionString = GetConnectionString();
            dataProvider = new DataProvider(connectionString);
        }

        private string GetConnectionString()
        {
            string connectionString;
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            connectionString = config["ConnectionString:MyDB"];
            return connectionString;
        }

        public void CloseConnection() => dataProvider.CloseConnection(connection);
    }
}
