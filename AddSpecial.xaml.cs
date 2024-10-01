using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для AddSpecial.xaml
    /// </summary>
    public partial class AddSpecial : Page
    {
        Frame Frame;
        SqliteConnection con = new SqliteConnection("Data Source=LogUsers.db");
        public AddSpecial(Frame frame)
        {
            InitializeComponent();
            Frame = frame;
        }
        private void BackBtn(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void AddBtn(object sender, RoutedEventArgs e)
        {
            if (NameT.Text != "" && MarkT.Text !="" && FreeT.Text != "" && PayT.Text != "")
            {
                NameT.Text.Trim();
                MarkT.Text.Trim();
                FreeT.Text.Trim();
                PayT.Text.Trim();
                con.Open();
                SqliteCommand command = new SqliteCommand($"INSERT INTO Specialties (name, slotFree, slotPay, mark) VALUES ('{NameT.Text}',{int.Parse(FreeT.Text)},{int.Parse(PayT.Text)}, {int.Parse(MarkT.Text)})", con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Специальность добавлена!");
                Frame.GoBack();
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
    }
}
