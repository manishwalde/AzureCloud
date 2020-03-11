using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static string DatabaseName = "maindb";
        static string CollectionName = "employee";
        static DocumentClient dc;

        static string endpoint = "https://azmmwcosmosdb.documents.azure.com:443/";
        static string key = "KU4P1DiHJwaL8qzqr1ss3TZ0z9X2Tj7gox6Rig0xxbkY1Ni28iWCLSO77Do1XXfqWhpOovqkRfEmCkvVWQWwNg==";

        static void Main(string[] args)
        {
            dc = new DocumentClient(new Uri(endpoint), key);

            //InsertOp("john", "morass");
            //InsertOp("tony", "jaa");
            //InsertOp("richard", "smith");
            QueryOp();

            Console.WriteLine("\n\n");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        static void InsertOp(string first, string last)
        {
            EmployeeEntity employee = new EmployeeEntity();
            employee.FirstName = first;
            employee.LastName = last;

            var result = dc.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName),
                employee).GetAwaiter().GetResult();
        }

        static void QueryOp()
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };

            IQueryable<EmployeeEntity> query = dc.CreateDocumentQuery<EmployeeEntity>(
                UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), queryOptions)
                .Where(e => e.LastName == "jaa");

            Console.WriteLine("Name of all of the staff:");
            Console.WriteLine("*************************");
            foreach (EmployeeEntity employee in query)
            {
                Console.WriteLine(employee);
            }
        }
    }

    public class EmployeeEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
