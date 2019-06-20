using System;
using System.Data.SqlClient;
using System.Text;

namespace InsertPublisherApp
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
        public void NumberOfRowsFromTable(string TableName)
        {
            try
            {
                StringBuilder sb = new StringBuilder("Select Count(*) from ");
                comanda.CommandText = Convert.ToString(sb.Append(TableName));

                if (String.IsNullOrWhiteSpace(Convert.ToString(comanda.ExecuteScalar())))
                {
                    Console.WriteLine("Please check again the table's name");
                }
                else
                    Console.WriteLine($"Number of rows in table {TableName} is {comanda.ExecuteScalar()}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Top10FromTable(string TableName, string FirstColumn, string SecondColumn)
        {
            try
            {
                StringBuilder sb = new StringBuilder("select top (10) * from ");
                comanda.CommandText = Convert.ToString(sb.Append(TableName));

                if (String.IsNullOrWhiteSpace(Convert.ToString(comanda.ExecuteScalar())))
                {
                    Console.WriteLine("Please check again the table's name");
                }
                else
                {
                    var reader = comanda.ExecuteReader();

                    // write each record
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}, {1}",
                            reader[FirstColumn],
                            reader[SecondColumn]);
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void CountofBooksPerPublisher()
        {
            try
            {
                comanda.CommandText = "Select p.Name,Count(BookID) as NumberOfBooks From Book2 inner join Publisher2 p on Book2.PublisherId = p.PublisherId group by p.Name";

                var reader = comanda.ExecuteReader();
                Console.WriteLine("Number of books per publisher");
                // write each record
                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}",
                        reader["Name"],
                        reader["NumberOfBooks"]);
                }
                reader.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SumofBooksPerPublisher()
        {
            try
            {
                comanda.CommandText = "Select p.Name,Sum(Price) as NoBooks From Book2 inner join Publisher2 p on Book2.PublisherId = p.PublisherId group by p.Name";

                var reader = comanda.ExecuteReader();
                Console.WriteLine("Number of books per publisher");
                // write each record
                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}",
                        reader["Name"],
                        reader["NumberOfBooks"]);
                }
                reader.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void InsertInBookTable(string PK, string BookName, string PublisherId, int Year, decimal Price)
        {
            try
            {
                comanda.CommandText = "Insert into Book2 values (@BookId,@Name,@PublisherId,@BookYear,@BookPrice);SELECT CAST(scope_identity() AS int);";
                SqlParameter parameterPK = new SqlParameter("@BookId", System.Data.DbType.Int32);
                parameterPK.Value = PK;

                SqlParameter parameterName = new SqlParameter("@Name", System.Data.DbType.String);
                parameterName.Value = BookName;

                SqlParameter parameterPublisher = new SqlParameter("@PublisherId", System.Data.DbType.Int32);
                parameterPublisher.Value = PublisherId;

                SqlParameter parameterYear = new SqlParameter("@BookYear", System.Data.DbType.Int32);
                parameterYear.Value = PublisherId;

                SqlParameter parameterPrice = new SqlParameter("@BookPrice", System.Data.SqlDbType.Decimal);
                parameterPrice.Value = Price;

                comanda.Parameters.Add(parameterPK);
                comanda.Parameters.Add(parameterName);
                comanda.Parameters.Add(parameterPublisher);
                comanda.Parameters.Add(parameterYear);
                comanda.Parameters.Add(parameterPrice);
                var returned = comanda.ExecuteScalar();
                Console.WriteLine($"The Book with PK {returned} has been added");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void UpdateNameFromTable(string TableName,string PK, string Name)
        {
            try
            {
                StringBuilder sb = new StringBuilder("Update ");
                if (TableName == "Publisher2")
                {
                    sb.Append(TableName + " set Name=@Name where PublisherId=@PK;");
                }
                else if (TableName == "Book2")
                {
                    sb.Append(TableName + " set Title=@Name where BookId=@PK;");
                }
                else
                    return;

                comanda.CommandText = Convert.ToString(sb);

                SqlParameter parameterPK = new SqlParameter("@PK", System.Data.DbType.Int32);
                parameterPK.Value = PK;

                SqlParameter parameterName = new SqlParameter("@Name", System.Data.DbType.String);
                parameterName.Value = Name;
                comanda.Parameters.Add(parameterName);
                comanda.Parameters.Add(parameterPK);

                if (String.IsNullOrWhiteSpace(Convert.ToString(comanda.ExecuteScalar())))
                {
                    Console.WriteLine("Please check again the table's name");
                }
                else
                    comanda.ExecuteScalar();
                Console.WriteLine($"Field with Primary key {PK} from {TableName} table has been updated.");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteFromTable (string TableName, string ID)
        {
            try
            {
                StringBuilder sb = new StringBuilder("Delete From ");
                if (TableName == "Book2")
                    comanda.CommandText = Convert.ToString(sb.Append(" where AuthorId=@ID;"));
                else if (TableName == "Publisher2")
                    comanda.CommandText = Convert.ToString(sb.Append("where PublisherId = ID;"));
                SqlParameter parameterPK = new SqlParameter("@ID", System.Data.DbType.Int32);
                parameterPK.Value = ID;
                comanda.Parameters.Add(parameterPK);

                if (String.IsNullOrWhiteSpace(Convert.ToString(comanda.ExecuteScalar())))
                {
                    Console.WriteLine("Please check again the table's name");
                }
                else
                    comanda.ExecuteScalar();
                Console.WriteLine($"Field with Primary key {ID} from {TableName} table has been deleted.");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SelectFromTable (string TableName, string ID)
        {
            try
            {
                if(TableName=="Publisher2")
                    comanda.CommandText = "Select * From Publisher2 where PublisherId=@Id;";
                else if (TableName=="Book2")
                    comanda.CommandText = "Select * From Book2 where BookId=@Id;";
                SqlParameter parameterPK = new SqlParameter("@Id", System.Data.DbType.Int32);
                parameterPK.Value = ID;


                comanda.Parameters.Add(parameterPK);
                var returned = comanda.ExecuteScalar();
                if (String.IsNullOrWhiteSpace(Convert.ToString(comanda.ExecuteScalar())))
                {
                    Console.WriteLine($"Primary Key {ID} doesn't exist in database, therefore no result has been returned");
                }
                else
                {
                    // get data stream
                    var reader = comanda.ExecuteReader();
                    if (TableName == "Publisher2")
                    { // write each record
                        Console.WriteLine("PublisherId|Name");
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}, {1}",
                                reader["PublisherId"],
                                reader["Name"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("BookId|Title|PublisherId|Year|Price");
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4}",
                                reader["BookId"],
                                reader["Title"],
                                reader["PublisherId"],
                                reader["Year"],
                                reader["Price"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    }
