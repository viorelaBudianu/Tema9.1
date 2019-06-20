using System;
using System.Data.SqlClient;

namespace InsertPublisherApp
{
    partial class Program
    {
        public class DatabaseCommands
        {
            private SqlCommand comanda = new SqlCommand();

            public DatabaseCommands(SqlConnection connection)
            {
                try
                {
                    comanda.Connection = connection;
                }
                catch (SqlException s)
                {
                    Console.WriteLine(s.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            public void InsertNewItemInPublisherTable(string PrimaryKey, string Name)
            {
                try
                {
                    comanda.CommandText = "Insert into Publisher2 values (@PublisherId,@Name);SELECT CAST(scope_identity() AS int);";
                    SqlParameter parameterPK = new SqlParameter("@PublisherId", System.Data.DbType.Int32);
                    parameterPK.Value = PrimaryKey;

                    SqlParameter parameterName = new SqlParameter("@Name", System.Data.DbType.String);
                    parameterName.Value = Name;

                    comanda.Parameters.Add(parameterPK);
                    comanda.Parameters.Add(parameterName);
                    var returned = comanda.ExecuteScalar();
                    Console.WriteLine($"The Publisher with PK {returned} has been added");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void SelectNamePublisher(string PrimaryKey)
            {
                try
                {
                    comanda.CommandText = "Select Name From Publisher2 where PublisherId=@Id;";
                    SqlParameter parameterPK = new SqlParameter("@Id", System.Data.DbType.Int32);
                    parameterPK.Value = PrimaryKey;


                    comanda.Parameters.Add(parameterPK);
                    var returned = comanda.ExecuteScalar();
                    if (String.IsNullOrWhiteSpace(Convert.ToString(comanda.ExecuteScalar())))
                    {
                        Console.WriteLine($"Primary Key {PrimaryKey} doesn't exist in database, therefor no result has been returned");
                    }
                    else
                    {
                        Console.WriteLine($"The Publisher with PK {PrimaryKey} is {Convert.ToString(comanda.ExecuteScalar())}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
