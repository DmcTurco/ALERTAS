using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace ALERT
{
    public class DatabaseHelper
    {

        private readonly string connectionString;
        private readonly string dbPath;

        public DatabaseHelper(string databaseName = "alerts.db")
        {
            dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseName);
            connectionString = $"Data Source={dbPath};Version=3";
        }

        public void Initialize()
        {
            Console.WriteLine("Inicializando base de datos...");

            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                Console.WriteLine($"✓ Base de datos creada en: {dbPath}");
            }
            else
            {
                Console.WriteLine($"✓ Base de datos creada en: {dbPath}");
            }

            CrearTablas();
        }

        private void CrearTablas()
        {
            using var con = new SQLiteConnection(connectionString);
            con.Open();

            string createTableSQL = @"
            CREATE TABLE IF NOT EXISTS ALERTS (
                cd INTEGER PRIMARY KEY AUTOINCREMENT,
                type INTEGER NOT NULL,
                button_flg INTEGER NOT NULL,
                message TEXT,
                record_date DATETIME DEFAULT CURRENT_TIMESTAMP,
                flg INTEGER NOT NULL,
                markas_read INTEGER NOT NULL
            )";

            using var command = new SQLiteCommand(createTableSQL, con);
            command.ExecuteNonQuery();

            Console.WriteLine("✓ Tabla 'ALERTS' lista");
        }
        public void InsertAlert(Alert alert)
        {
            using var con = new SQLiteConnection(connectionString);
            con.Open();

            string sql = @"INSERT INTO ALERTS (type, button_flg, message, flg, markas_read) VALUES (@type, @buttonFlg, @message, @flg, @markasRead)";

            using var command = new SQLiteCommand(sql, con);
            command.Parameters.AddWithValue("@type", alert.type);
            command.Parameters.AddWithValue("@buttonFlg", alert.buttonFlg);
            command.Parameters.AddWithValue("@message", alert.message);
            command.Parameters.AddWithValue("@flg", alert.flg);
            command.Parameters.AddWithValue("@markasRead", alert.markasRead);

            command.ExecuteNonQuery();
        }

        public List<Alert> GetActiveAlerts()
        {
            var alerts = new List<Alert>();

            using var con = new SQLiteConnection(connectionString);
            con.Open();

            string sql = @"
                SELECT
                    cd,
                    type,
                    button_flg,
                    message,
                    record_date,
                    flg,
                    markas_read
                FROM ALERTS
                WHERE flg = 1 AND markas_read = 1";

            using var command = new SQLiteCommand(sql, con);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                alerts.Add(new Alert
                {
                    cd = reader.GetInt32(reader.GetOrdinal("cd")),
                    type = reader.GetInt32(reader.GetOrdinal("type")),
                    buttonFlg = reader.GetInt32(reader.GetOrdinal("button_flg")),
                    message = reader["message"]?.ToString(),
                    recordDate = reader.GetDateTime(reader.GetOrdinal("record_date")),
                    flg = reader.GetInt32(reader.GetOrdinal("flg")),
                    markasRead = reader.GetInt32(reader.GetOrdinal("markas_read"))
                });
            }

            return alerts;
        }


        public void MarkAsRead(int cd)
        {
            using var con = new SQLiteConnection(connectionString);
            con.Open();

            string sql = @"
                UPDATE ALERTS
                SET markas_read = 0, flg = 0
                WHERE cd = @cd";

            using var command = new SQLiteCommand(sql, con);
            command.Parameters.AddWithValue("@cd", cd);
            command.ExecuteNonQuery();
        }


        public List<Alert> GetAllAlerts()
        {
            var alerts = new List<Alert>();

            using var con = new SQLiteConnection(connectionString);
            con.Open();

            string sql = @"
                    SELECT
                        cd,
                        type,
                        button_flg,
                        message,
                        record_date,
                        flg,
                        markas_read
                    FROM ALERTS
                    ORDER BY cd ASC";

            using var command = new SQLiteCommand(sql, con);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                alerts.Add(new Alert
                {
                    cd = reader.GetInt32(reader.GetOrdinal("cd")),
                    type = reader.GetInt32(reader.GetOrdinal("type")),
                    buttonFlg = reader.GetInt32(reader.GetOrdinal("button_flg")),
                    message = reader["message"]?.ToString(),
                    recordDate = reader.GetDateTime(reader.GetOrdinal("record_date")),
                    flg = reader.GetInt32(reader.GetOrdinal("flg")),
                    markasRead = reader.GetInt32(reader.GetOrdinal("markas_read"))
                });
            }

            return alerts;
        }



    }
}
