using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace TestSQLite
{
    public class SQLite : IDisposable
    {
        private SQLiteConnection _connection;
        private SQLiteTransaction _transaction;
        private string _connectionString;

        public SQLite(string dbPath)
        {
            _connectionString = $"Data Source={dbPath};Version=3;";
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();
        }

        //Open Connection (if it is closed)
        public void Open()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        // close Connection
        public void Close()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        // ExecuteNonQuery (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                if (_transaction != null)
                    cmd.Transaction = _transaction;

                AddParameters(cmd, parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        // ExecuteScalar (Pull a single value)
        public object ExecuteScalar(string sql, Dictionary<string, object> parameters = null)
        {
            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                if (_transaction != null)
                    cmd.Transaction = _transaction;

                AddParameters(cmd, parameters);
                return cmd.ExecuteScalar();
            }
        }

        // ExecuteReader (Pull multiple rows)
        public DataTable ExecuteReader(string sql, Dictionary<string, object> parameters = null)
        {
            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                if (_transaction != null)
                    cmd.Transaction = _transaction;

                AddParameters(cmd, parameters);
                using (var reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        // Transaction
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        // Add Parameter
        private void AddParameters(SQLiteCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }
        }

        // Dispose
        public void Dispose()
        {
            Close();
            _connection.Dispose();
        }
    }
}
