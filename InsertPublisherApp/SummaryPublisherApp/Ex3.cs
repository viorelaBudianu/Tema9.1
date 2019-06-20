using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsertPublisherApp;
 



namespace SummaryPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection("Week9");
            databaseConnection.ConnectionToDatabase();

            DatabaseCommands command = new DatabaseCommands(databaseConnection.Connection);
            command.NumberOfRowsFromTable("Publisher2");

            command.Top10FromTable("Publisher2", "PublisherId", "Name");

            command.CountofBooksPerPublisher();

            command.SumofBooksPerPublisher();

            databaseConnection.DisposeConnection();
        }
    }
}
