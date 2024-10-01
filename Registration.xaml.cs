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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        SqliteConnection con = new SqliteConnection("Data Source=LogUsers.db");
        Frame rootFrame;
        public Registration(Frame frame)
        {
            InitializeComponent();
            rootFrame = frame;
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

        private void Registr(object sender, RoutedEventArgs e)
        {
            if (loginT.Text != "" && loginT.Text != "Логин" && passwordT.Text != "" && passwordT.Text != "Пароль") {
                loginT.Text.Trim();
                passwordT.Text.Trim();
                con.Open();
                SqliteCommand command = new SqliteCommand($"INSERT INTO Users (login, password, idPost) VALUES ('{loginT.Text}','{passwordT.Text}',2)", con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Регистрация прошла успешно! Теперь вы можете выполнить вход в приложение.");
                rootFrame.Content = null;
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
