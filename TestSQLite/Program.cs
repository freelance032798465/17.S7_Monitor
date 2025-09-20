using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SQLite("test.db"))
            {
                // Create a table
                db.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS Users(Id INTEGER PRIMARY KEY, Name TEXT, Age INTEGER)");

                // Insert
                db.ExecuteNonQuery("INSERT INTO Users(Name, Age) VALUES(@name, @age)",
                    new Dictionary<string, object> { { "@name", "Test" }, { "@age", 30 } });

                // Query
                var dt = db.ExecuteReader("SELECT * FROM Users");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["Id"]} - {row["Name"]} - {row["Age"]}");
                }

                // Scalar
                var count = db.ExecuteScalar("SELECT COUNT(*) FROM Users");
                Console.WriteLine("Total Users: " + count);
            }
        }
    }

}
