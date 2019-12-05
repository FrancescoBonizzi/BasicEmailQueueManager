using BasicEmailQueueManager.Exceptions;
using BasicEmailQueueManager.Infrastructure;
using Dapper;
using System;
using System.Data.SqlClient;

namespace BasicEmailQueueManager
{
    public class SQLConfiguration : IConfiguration
    {
        private readonly string _connectionString;

        public bool EnableSsl => ConvertToBool(GetValueOrThrow("EnableSsl"));
        public bool UseDefaultCredentials => ConvertToBool(GetValueOrThrow("UseDefaultCredentials"));
        public string Host => GetValueOrThrow("Host");
        public int Port => ConvertToInt32(GetValueOrThrow("Port"));
        public string UserName => GetValueOrThrow("UserName");
        public string Password => GetValueOrThrow("Password");
        public TimeSpan RunInterval => ConvertToTimeSpan(GetValueOrThrow("RunInterval"));

        public SQLConfiguration(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Missing connection string", connectionString);

            _connectionString = connectionString;
        }

        private string GetValueOrThrow(string key)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var value = connection.QueryFirstOrDefault<string>(
                    "SELECT Value " +
                    "FROM EmailQueueManager.Configuration " +
                    "WHERE Key = @key",
                    key);

                if (value == null)
                    throw new MissingConfigurationException(key);

                return value;
            }
        }

        private bool ConvertToBool(string value)
        {
            if (value == "0" || value == "1")
                return value == "1";
            else if (value.Equals("true", StringComparison.InvariantCultureIgnoreCase) || value.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);

            throw new FormatException($"'{value}' cannot be converted to a boolean");
        }

        private int ConvertToInt32(string value)
        {
            if (!int.TryParse(value, out int convertedValue))
                throw new FormatException($"'{value}' cannot be converted to a 32 bit integer");

            return convertedValue;
        }

        private TimeSpan ConvertToTimeSpan(string value)
        {
            try
            {
                var intConvertedValue = ConvertToInt32(value);
                return TimeSpan.FromSeconds(intConvertedValue);
            }
            catch (FormatException)
            {
                throw new FormatException($"'{value}' cannot be converted to a 32 bit integer to represent 'seconds'");
            }
        }
    }
}
