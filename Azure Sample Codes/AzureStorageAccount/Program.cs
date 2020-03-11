using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleApp1
{
    class Program
    {
        static CloudStorageAccount cloudStorageAccount;
        static CloudTableClient cloudTableClient;
        static CloudTable employees;

        static void Main(string[] args)
        {
            const string connectionString = "DefaultEndpointsProtocol=https;AccountName=azmmwnewstorage;AccountKey=Slk4yIlRJG6vuqu1Zkqmv0+iXJCOCZOpT4vBm0LoTkQzOC1ajriDdwWWMSbH5jzJvpoZrqlK0WvMpKQsIDPvRg==;EndpointSuffix=core.windows.net";
            cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            employees = cloudTableClient.GetTableReference("Employees");

            InsertOp("john", "morass");
            InsertOp("tony", "jaa");
            InsertOp("robert", "valkn");
            QueryOp();

            Console.WriteLine("\n\n");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        static void InsertOp(string first, string last)
        {
            EmployeeEntity employee = new EmployeeEntity(first, last);

            TableOperation insertop = TableOperation.Insert(employee);
            employees.Execute(insertop);
        }
        
        static void QueryOp()
        {
            TableQuery<EmployeeEntity> query = new TableQuery<EmployeeEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey",QueryComparisons.Equal, "staff"));
            Console.WriteLine("Name of all of the staff:");
            Console.WriteLine("*************************");
            foreach(EmployeeEntity employee in employees.ExecuteQuery(query))
            {
                Console.WriteLine(employee.RowKey);
            }
        }
    }

    public class EmployeeEntity : TableEntity
    {
        public EmployeeEntity(string firstname, string lastname)
        {
            this.PartitionKey = "staff";
            this.RowKey = firstname + " " + lastname;
        }
        public EmployeeEntity() { }
    }
}
