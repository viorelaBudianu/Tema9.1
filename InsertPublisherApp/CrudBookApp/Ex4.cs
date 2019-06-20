using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsertPublisherApp;

namespace CrudBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseConnection dbConnection = new DatabaseConnection("Week9");
            dbConnection.ConnectionToDatabase();
            DatabaseCommands commands = new DatabaseCommands(dbConnection.Connection);

           // commands.InsertInBookTable("69", "La Rascruce de vanturi", "2", 1990, 32.4m);
           // Console.WriteLine("Please give us the Primary key of the book you want to update");
          
            //commands.UpdateNameFromTable("Book2", Console.ReadLine(), "BookTest");
            //Console.WriteLine("Please give us the Primary key of the book you want to delete");
            //commands.DeleteFromTable("Book2", Console.ReadLine());

            Console.WriteLine("Please give us the Primary key of the book you want view");
            commands.SelectFromTable("Book2", Console.ReadLine());
        }
    }
}
