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
    /// Логика взаимодействия для SpecialFrame.xaml
    /// </summary>
    public partial class SpecialFrame : Page
    {
        SqliteConnection con = new SqliteConnection("Data Source=LogUsers.db");
        Frame Frame;
        public void CreateCardSpecialtiesForAdmin(int id,string name) // Создание карточки специальности для админа
        {
            Button button = new()
            {
                BorderThickness = new Thickness(0),
                Background = null,
                Cursor = Cursors.Hand,
                Tag = id,
                Content = name,
                FontSize = 18,
                FontFamily = new FontFamily("Cascadia Code"),
                ToolTip = "Нажмите на специальность, чтобы открыть специальность",
            };
            button.Click += ShowTopLists;
            list.ItemHeight = 40;
            list.Margin = new Thickness(5,5,0,5);
            list.Orientation = Orientation.Vertical;
            list.Children.Add(button);
        }

        public void CreateCardSpecialtiesForStudent(int id, string name, string mark, int slotFree, int slotPay) // Создание карточки специальности для студента
        {
            var bc = new BrushConverter();

            TextBlock Name = new()
            {
                FontSize = 18,
                Text = name,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
            };
            TextBlock Mark = new()
            {
                FontSize = 14,
                Text = "Проходной балл: от " + mark,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBlock SlotFree = new()
            {
                FontSize = 14,
                Text = "Бюджет: " + slotFree.ToString() + " мест",
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBlock SlotPay = new()
            {
                FontSize = 14,
                Text = "Платное: " + slotPay.ToString() + " мест",
                Margin = new Thickness(0, 0, 0, 10)
            };

            WrapPanel wrapPanel = new()
            {
                Orientation = Orientation.Vertical,
            };

            Button button = new()
            {
                BorderThickness = new Thickness(0),
                Background = null,
                Cursor = Cursors.Hand,
                Tag = id,
            };
            button.Click += ShowTopLists;

            Border border = new()
            {
                Background = (Brush)bc.ConvertFrom("#FFC6ECFF"),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(5),
                Width = 170,
                Margin = new Thickness(5),
                ToolTip = "Нажмите на специальность, чтобы открыть специальность",
            };
            wrapPanel.Children.Add(Name);
            wrapPanel.Children.Add(Mark);
            wrapPanel.Children.Add(SlotFree);
            wrapPanel.Children.Add(SlotPay);
            button.Content = wrapPanel;
            border.Child = button;
            list.Children.Add(border);
        }

        private void ShowTopLists(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Frame.Content = new TopListsFrame(Frame, int.Parse(button.Tag.ToString()));
        }

        public SpecialFrame(Frame frame, int idPost)
        {
            InitializeComponent();
            con.Open();
            SqliteCommand command = new SqliteCommand("SELECT Specialties.id, Specialties.name, Specialties.mark, Specialties.slotFree, Specialties.slotPay FROM Specialties", con);
            SqliteDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var mark = reader.GetString(2);
                    var slotFree = reader.GetInt32(3);
                    var slotPay = reader.GetInt32(4);

                    if (idPost == 0)
                    {
                        CreateCardSpecialtiesForAdmin(id, name);
                        AddSpecial.Visibility = Visibility.Visible;
                    }
                    else if (idPost == 2)
                    {
                        CreateCardSpecialtiesForStudent(id, name, mark, slotFree, slotPay);
                    }
                }
            }
            con.Close();
            Frame = frame;
        }
        private void AddSpecialBtn(object sender, RoutedEventArgs e)
        {
            Frame.Content = new AddSpecial(Frame);
        }
    }
}
