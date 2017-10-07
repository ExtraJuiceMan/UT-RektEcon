using System;
using System.Data.SQLite;

namespace ExtraConcentratedJuice.RektEcon
{
    public class DBHandler
    {

        SQLiteConnection dbConnection = new SQLiteConnection("Data Source=database/rekt_data.db;Version=3;");

        public decimal GetBalance(string playerId)
        {
            string sql = String.Format("SELECT bal FROM users WHERE id = {0};", playerId);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            dbConnection.Open();
            decimal balance = Convert.ToDecimal(command.ExecuteScalar());
            dbConnection.Close();
            return balance;
        }

        public void AddBalance(string playerId, decimal balAdd)
        {
            string sql = String.Format("UPDATE users SET bal = bal + {0} WHERE id = {1};", balAdd, playerId);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public void SetBalance(string playerId, decimal newBal)
        {
            string sql = String.Format("UPDATE users SET bal = {0} WHERE id = {1};", newBal, playerId);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public decimal GetItemPrice(ushort itemId)
        {
            string sql = String.Format("SELECT price FROM items WHERE id = {0};", itemId);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            dbConnection.Open();
            object result = command.ExecuteScalar();
            dbConnection.Close();
            decimal price = 0;
            if (result != null)
            {
                price = Convert.ToDecimal(result);
            }
            return price;
        }

        public ushort RandomItem(string type)
        {
            string sql = String.Format("SELECT id FROM items WHERE type = '{0}' AND price != 0 ORDER BY RANDOM();", type);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            ushort randItem = 0;
            dbConnection.Open();
            randItem = Convert.ToUInt16(command.ExecuteScalar());
            dbConnection.Close();
            return randItem;
        }

        public string GetItemType(ushort itemId)
        {
            string sql = String.Format("SELECT type FROM items WHERE id = {0};", itemId);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            dbConnection.Open();
            string type = Convert.ToString(command.ExecuteScalar());
            dbConnection.Close();
            return type;
        }

        public void InitializePlayer(string playerId)
        {
            string sql = String.Format("SELECT count(*) FROM users WHERE id = {0};", playerId);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            dbConnection.Open();
            int exists = Convert.ToInt32(command.ExecuteScalar());
            if (exists == 0)
            {
                sql = String.Format("INSERT INTO users VALUES ({0}, 0);", playerId);
                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
            dbConnection.Close();
        }
        public void InitializeDatabase()
        {
            string sql = "CREATE TABLE IF NOT EXISTS users(id INT PRIMARY KEY, bal REAL);";
            string sql2 = "CREATE TABLE IF NOT EXISTS items(id INT PRIMARY KEY, price REAL, name TEXT, type TEXT);";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            command = new SQLiteCommand(sql2, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
    }
}
