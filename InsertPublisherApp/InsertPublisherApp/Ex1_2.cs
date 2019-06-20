using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InsertPublisherApp
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please give us the name of the database you want to connect");
          
            DatabaseConnection db = new DatabaseConnection(Console.ReadLine());
            db.ConnectionToDatabase();

            DatabaseCommands comanda = new DatabaseCommands(db.Connection);
          //  comanda.InsertNewItemInPublisherTable("27", "PublisherTest");
            comanda.SelectNamePublisher("1");
            db.DisposeConnection();
        }
    }
}
