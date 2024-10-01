using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Student
{
    /// <summary>
    /// Логика взаимодействия для ShowUsersFrame.xaml
    /// </summary>
    public partial class ShowUsersFrame : Page
    {
        SqliteConnection con = new SqliteConnection("Data Source=LogUsers.db");
        List<User> users = new();
        public ShowUsersFrame()
        {
            InitializeComponent();
            con.Open();
            SqliteCommand command = new SqliteCommand("SELECT Users.id, Users.login, Users.password, (SELECT Posts.name FROM Posts WHERE Posts.idPost = Users.idPost) FROM Users", con);
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные
            {
                int i = 0;
                while (reader.Read())   // построчно считываем данные
                {
                    var id = reader.GetInt32(0);
                    var login = reader.GetString(1);
                    var pass = reader.GetString(2);
                    var post = reader.GetString(3);
                    User user = new User(login, pass, post);
                    users.Add(user);
                }
            }
            con.Close();
            TableForUsers.ItemsSource = users;
        }
        private void TableForUsers_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            SaveEdit.Visibility = Visibility.Visible;
        }

        private void SaveEdit_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqliteCommand command;
            for (int i= 0; i < TableForUsers.Items.Count; i++)
            {
                User user = (User)TableForUsers.Items.GetItemAt(i);
                command = new SqliteCommand($"UPDATE Users SET login = '{user.Login}', password = '{user.Password}', idPost = (Select Posts.idPost FROM Posts WHERE Posts.name = '{user.Post}') WHERE Users.id = {i+1}", con);
                command.ExecuteNonQuery();
            }
            MessageBox.Show("Изменения успешно занесены в базу данных!");
            con.Close();
            SaveEdit.Visibility = Visibility.Collapsed;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
