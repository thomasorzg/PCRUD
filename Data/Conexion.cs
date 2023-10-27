using System.Data.SqlClient;
using System.Data;

namespace MVCPracticaArqdSoft.Data {

    public class Conexion {
        private string connectionString = string.Empty;

        public Conexion() {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            connectionString = config.GetSection("ConnectionStrings:SQLConnectionString").Value;
        }

        public string GetConnectionString () => connectionString;
    }

}
