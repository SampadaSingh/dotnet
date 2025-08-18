using System;
using System.Data.SqlClient;

class Program
{
    static string connStr = "Data Source=.\\SQLEXPRESS;Initial Catalog=dbname;Integrated Security=True";

    static void Main()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // CREATE
                string insert1 = "INSERT INTO employee (Id, Name) VALUES (1, 'Anita')";
                string insert2 = "INSERT INTO employee (Id, Name) VALUES (2, 'Rohan')";
                new SqlCommand(insert1, conn).ExecuteNonQuery();
                new SqlCommand(insert2, conn).ExecuteNonQuery();
                Console.WriteLine("Inserted 2 employees.");

                //READ
                string select = "SELECT * FROM employee";
                SqlCommand readCmd = new SqlCommand(select, conn);
                SqlDataReader reader = readCmd.ExecuteReader();
                Console.WriteLine("\nEmployees:");
                while (reader.Read())
                    Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}");
                reader.Close();

                // UPDATE
                string update = "UPDATE employee SET Name = 'Anita S.' WHERE Id = 1";
                new SqlCommand(update, conn).ExecuteNonQuery();
                Console.WriteLine("\nUpdated Anita's name.");

                // DELETE
                string delete = "DELETE FROM employee WHERE Id = 2";
                new SqlCommand(delete, conn).ExecuteNonQuery();
                Console.WriteLine("\nDeleted Rohan.");

                // Verify changes
                reader = readCmd.ExecuteReader();
                Console.WriteLine("\nEmployees after updates:");
                while (reader.Read())
                    Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}");
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
