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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqliteConnection con = new SqliteConnection("Data Source=LogUsers.db");
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Tag.ToString() == "0")
            {
                textBox.Text = "";
                textBox.Tag = "1";
            }
        }

        private void LoginBtn(object sender, RoutedEventArgs e)
        {
            bool have = false;
            con.Open();
            SqliteCommand command = new SqliteCommand("SELECT Users.id, Users.login, Users.password, Users.idPost FROM Users", con);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        var id = reader.GetInt32(0);
                        var login = reader.GetString(1);
                        var password = reader.GetString(2);
                        var idPost = reader.GetInt32(3);

                        if (loginT.Text == login && passwordT.Text == password)
                        {
                            have = true;
                            if (idPost == 0)
                            {
                                Admin admin = new Admin(idPost);
                                admin.Show();
                            }
                            else if (idPost == 1)
                            {
                                Editor editor = new Editor(idPost);
                                editor.Show();
                            }
                            else if (idPost == 2)
                            {
                                WinStudent student = new WinStudent(idPost);
                                student.Show();
                            }
                            this.Close();
                            break;
                        }
                    }
                    if (have == false)
                        MessageBox.Show("Вы ввели неправильный пароль или логин. Повторите попытку");
                }
            }
            con.Close();
        }

        private void RegBtn(object sender, RoutedEventArgs e)
        {
            Main.Content = new Registration(Main);
            btnReg.Visibility = Visibility.Collapsed;
        }
    }
}
