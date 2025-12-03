using Newtonsoft.Json;
using Npgsql;

namespace bizlabcoreapi.Data
{
    public class SecureUserData
    {
        private readonly IConfiguration _configuration;

        public SecureUserData()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            _configuration = configuration;
        }

        public bool IsSecure(string browserId)
        {
            var connString = _configuration.GetConnectionString("bizlabcoreapiContext");
            string results = string.Empty;
            using (var connection = new NpgsqlConnection(connString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("SELECT count(1) FROM user_activity where browser_id='" + browserId + "'", connection))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    results = cmd.ExecuteScalar().ToString();
                }
            }
            return results == "1" ? true : false;
        }

        public void InsertAuthBrowser(string browserId, string userId)
        {
            var connString = _configuration.GetConnectionString("bizlabcoreapiContext");
            string results = string.Empty;
            using (var connection = new NpgsqlConnection(connString))
            {
                connection.Open();
                const string sql = @"
                    INSERT INTO user_activity (browser_id,user_id, logged_in, last_transaction)
                    VALUES (@browser_id,@user_id, @logged_in, @last_transaction);
                ";

                using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("browser_id", browserId);
                cmd.Parameters.AddWithValue("user_id", userId);
                cmd.Parameters.AddWithValue("logged_in", DateTime.Now);
                cmd.Parameters.AddWithValue("last_transaction", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAuthBrowser(string browserId)
        {
            var connString = _configuration.GetConnectionString("bizlabcoreapiContext");
            string results = string.Empty;
            using (var connection = new NpgsqlConnection(connString))
            {
                connection.Open();
                const string sql = @"
                    UPDATE user_activity
                    SET last_transaction = @last_transaction
                    WHERE browser_id = @browser_id;
                ";
                using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("browser_id", browserId);
                cmd.Parameters.AddWithValue("last_transaction", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAuthBrowser(string browserId)
        {
            var connString = _configuration.GetConnectionString("bizlabcoreapiContext");
            string results = string.Empty;
            using (var connection = new NpgsqlConnection(connString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM user_activity where browser_id='" + browserId + "'", connection))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    int recs = cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
