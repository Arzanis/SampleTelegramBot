using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace TelegramBot.Models
{
    public static class SqlService
    {
        private static string ConnString => GetConnString();
        private static NpgsqlConnection MainConnection;

        private static string GetConnString ()
        {
            Uri dbUrl = new Uri(Environment.GetEnvironmentVariable("DATABASE_URL"));
            var builder = new NpgsqlConnectionStringBuilder();

            builder.Username = dbUrl.UserInfo.Split(':')[0];
            builder.Password = dbUrl.UserInfo.Split(':')[1];
            builder.Host = dbUrl.Host;
            builder.Port = dbUrl.Port;
            builder.Database = dbUrl.AbsolutePath.Remove(0, 1);
            builder.IntegratedSecurity = false;
            builder.Pooling = true;
            builder.SslMode = SslMode.Require;
            builder.TrustServerCertificate = true;                

            return builder.ConnectionString;
        }

        public static void InitConnection()
        {
            if (MainConnection != null && MainConnection.State == ConnectionState.Open)
            {
                MainConnection.Close();
            }

            MainConnection = new NpgsqlConnection(ConnString);
            MainConnection.Open();
        }

        public static async Task<object> ExecScalarQueryAsync(string sqlQuery, List<NpgsqlParameter> sqlParams)
        {
            using (var cmd = new NpgsqlCommand(sqlQuery, MainConnection))
            {
                cmd.Parameters.AddRange(sqlParams.ToArray());
                return await cmd.ExecuteScalarAsync();
            }
        }
        public static object ExecScalarQuery(string sqlQuery, List<NpgsqlParameter> sqlParams)
        {
            using (var cmd = new NpgsqlCommand(sqlQuery, MainConnection))
            {
                cmd.Parameters.AddRange(sqlParams.ToArray());
                return cmd.ExecuteScalar();
            }
        }

        public static async Task<int> ExecNonQueryAsync(string sqlQuery, List<NpgsqlParameter> sqlParams)
        {
            using (var cmd = new NpgsqlCommand(sqlQuery, MainConnection))
            {
                cmd.Parameters.AddRange(sqlParams.ToArray());
                return await cmd.ExecuteNonQueryAsync();
            }
        }
        public static int ExecNonQuery(string sqlQuery, List<NpgsqlParameter> sqlParams)
        {
            using (var cmd = new NpgsqlCommand(sqlQuery, MainConnection))
            {
                cmd.Parameters.AddRange(sqlParams.ToArray());
                return cmd.ExecuteNonQuery();
            }
        }

        public static async Task<DataTable> GetDataAsync(string sqlQuery, List<NpgsqlParameter> sqlParams)
        {
            IDataReader reader;
            DataTable data = new DataTable();
            
            using (var cmd = new NpgsqlCommand(sqlQuery, MainConnection))
            {
                cmd.Parameters.AddRange(sqlParams.ToArray());
                reader = await cmd.ExecuteReaderAsync();
            }

            data.Load(reader);
            return data;
        }
        public static DataTable GetData(string sqlQuery, List<NpgsqlParameter> sqlParams)
        {
            IDataReader reader;
            DataTable data = new DataTable();

            using (var cmd = new NpgsqlCommand(sqlQuery, MainConnection))
            {
                cmd.Parameters.AddRange(sqlParams.ToArray());
                reader = cmd.ExecuteReader();
            }

            data.Load(reader);
            return data;
        }
    }
}
