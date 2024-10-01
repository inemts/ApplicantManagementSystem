using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace Student
{
    /// <summary>
    /// Логика взаимодействия для MainAdminFrame.xaml
    /// </summary>
    public partial class MainAdminFrame : Page
    {
        SqliteConnection con = new SqliteConnection("Data Source=LogUsers.db");
        List<User> users = new();
        public MainAdminFrame()
        {
            InitializeComponent();
            con.Open();
            SqliteCommand command = new SqliteCommand("SELECT Users.id, Users.login, Specialties.name, Users.ex FROM Users JOIN Specialties ON Specialties.id = Users.idSpecial WHERE Users.ex IS NOT NULL", con);
            SqliteDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                int i = 0;
                while (reader.Read())   // построчно считываем данные
                {
                    var id = reader.GetInt32(0);
                    var login = reader.GetString(1);
                    var special = reader.GetString(2);
                    var ex = reader.GetString(3);
                    User exam = new User(login);
                    exam.AddSpecial(special, ex);
                    users.Add(exam);
                }
            }

            con.Close();
            TableForExams.ItemsSource = users;
        }

    }
}
