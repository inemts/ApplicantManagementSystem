using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Student
{
    public class SqliteConnector
    {
        SqliteConnection con = new SqliteConnection("Data Source=LogUsers.db");
        public SqliteDataReader SelectDB()
        {
            string query = $"SELECT Users.id, Users.login, Users.password, (SELECT Posts.name FROM Posts WHERE Posts.idPost = Users.idPost) FROM Users";
            con.Open();
            SqliteCommand command = new SqliteCommand(query, con);
            SqliteDataReader reader = command.ExecuteReader();
            return reader;
        }
    }
}
