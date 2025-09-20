using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TestMySQL
{
    public class MySQL
    {
        private string connectionString;

        public MySQL(string server, string database, string user, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};";
        }

        private string tableName = "table_log";

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // Create
        public void InsertLog(TableLog log)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $@"INSERT INTO {tableName} (Date, Time, ProductName, ProductCode, Description) 
                       VALUES (@Date, @Time, @ProductName, @ProductCode, @Description)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", log.Date);
                    cmd.Parameters.AddWithValue("@Time", log.Time);
                    cmd.Parameters.AddWithValue("@ProductName", log.ProductName);
                    cmd.Parameters.AddWithValue("@ProductCode", log.ProductCode);
                    cmd.Parameters.AddWithValue("@Description", log.Description);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Read All
        public List<TableLog> GetAllLogs()
        {
            var logs = new List<TableLog>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName}";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(MapReaderToLog(reader));
                    }
                }
            }
            return logs;
        }

        // Read by ID
        public TableLog GetLogById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName} WHERE ID=@ID";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToLog(reader);
                        }
                    }
                }
            }
            return null;
        }

        // Update
        public void UpdateLog(TableLog log)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $@"UPDATE {tableName} 
                               SET Date=@Date, Time=@Time, ProductName=@ProductName, 
                                   ProductCode=@ProductCode, Description=@Description
                               WHERE ID=@ID";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", log.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Time", log.Time);
                    cmd.Parameters.AddWithValue("@ProductName", log.ProductName);
                    cmd.Parameters.AddWithValue("@ProductCode", log.ProductCode);
                    cmd.Parameters.AddWithValue("@Description", log.Description);
                    cmd.Parameters.AddWithValue("@ID", log.ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete
        public void DeleteLog(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"DELETE FROM {tableName} WHERE ID=@ID";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Filter by Date
        public List<TableLog> GetLogsByDate(DateTime date)
        {
            var logs = new List<TableLog>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName} WHERE Date = @Date";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(MapReaderToLog(reader));
                        }
                    }
                }
            }
            return logs;
        }

        // Filter by Date Range
        public List<TableLog> GetLogsByDateRange(DateTime startDate, DateTime endDate)
        {
            var logs = new List<TableLog>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName} WHERE Date BETWEEN @Start AND @End";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Start", startDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@End", endDate.ToString("yyyy-MM-dd"));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(MapReaderToLog(reader));
                        }
                    }
                }
            }
            return logs;
        }

        // Filter by Time Range
        public List<TableLog> GetLogsByTimeRange(TimeSpan startTime, TimeSpan endTime)
        {
            var logs = new List<TableLog>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName} WHERE Time BETWEEN @Start AND @End";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Start", startTime);
                    cmd.Parameters.AddWithValue("@End", endTime);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(MapReaderToLog(reader));
                        }
                    }
                }
            }
            return logs;
        }

        // Filter by Date + Time Range
        public List<TableLog> GetLogsByDateTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var logs = new List<TableLog>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName} WHERE Date = @Date AND Time BETWEEN @Start AND @End";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Start", startTime);
                    cmd.Parameters.AddWithValue("@End", endTime);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(MapReaderToLog(reader));
                        }
                    }
                }
            }
            return logs;
        }

        // 🔹 Helper Function แปลง Reader → TableLog
        private TableLog MapReaderToLog(MySqlDataReader reader)
        {
            return new TableLog
            {
                ID = reader.GetInt32("ID"),
                Date = reader.GetDateTime("Date"),
                Time = reader.GetTimeSpan("Time"),
                ProductName = reader.GetString("ProductName"),
                ProductCode = reader.GetString("ProductCode"),
                Description = reader.GetString("Description")
            };
        }

        public List<TableLog> GetLogsByProductName(string text)
        {
            var logs = new List<TableLog>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName} WHERE ProductName = @ProductName";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", text);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(MapReaderToLog(reader));
                        }
                    }
                }
            }
            return logs;
        }
        public List<TableLog> GetLogsByProductCode(string text)
        {
            var logs = new List<TableLog>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName} WHERE ProductCode = @ProductCode";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductCode", text);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(MapReaderToLog(reader));
                        }
                    }
                }
            }
            return logs;
        }
        public List<TableLog> GetLogsByDescription(string text)
        {
            var logs = new List<TableLog>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName} WHERE Description = @Description";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Description", text);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(MapReaderToLog(reader));
                        }
                    }
                }
            }
            return logs;
        }
    }

    //header of database
    public class TableLog
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
    }

}
