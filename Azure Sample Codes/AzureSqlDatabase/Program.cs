using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AzureSqlDatabase
{
    class Program
    {
        public static string connectionString;
        static void Main(string[] args)
        {
            connectionString = "Server=tcp:azmmwnewdb.database.windows.net,1433;Initial Catalog=azmmwsqldb;Persist Security Info=False;User ID=azmmwadmin;Password=Leader12345!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var conn = new SqlConnection(connectionString);

            var cmd = new SqlCommand("INSERT Employee(FirstName, LastName) VALUES (@FirstName, @LastName)", conn);

            EmployeeEntity employeeEntity = new EmployeeEntity();
            employeeEntity.FirstName = "John";
            employeeEntity.LastName = "Abhram";

            cmd.Parameters.AddWithValue("@FirstName", employeeEntity.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employeeEntity.LastName);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    public class EmployeeEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return base.ToString(); 
        }
    }
}
